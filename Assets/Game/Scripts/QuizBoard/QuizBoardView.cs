namespace Game.QuizBoard
{
	using DG.Tweening;
	using Scripts.Configs;
	using TMPro;
	using UnityEngine;
	using Zenject;

	public interface IQuizBoardView
	{
		IQuizCardView CreateCard( CardConfig cfg, bool hasAnimation );

		void SetGoal(string goal, bool hasAnimation);
	}
	
	public class QuizBoardView : MonoBehaviour, IQuizBoardView
	{
		[SerializeField] TextMeshProUGUI _goalTxt;
		
		[Inject] QuizCardView.Factory _factory;

		Tween _showCardTween;
		Tween _showTextTween;
		
		void OnDestroy()
		{
			_showCardTween?.Kill();
			_showTextTween?.Kill();
		}
		
		public IQuizCardView CreateCard( CardConfig cfg, bool hasAnimation )
		{
			var card = _factory.Create(cfg);
			
			if (hasAnimation)
			{
				_showCardTween = card.Transform
					.DOScale( 1, 0.22f )
					.SetEase(Ease.OutBounce)
					.From(0);
			}
			
			return card;
		}

		public void SetGoal( string goal, bool hasAnimation )
		{
			_goalTxt.text = goal;

			if (!hasAnimation)
				return;
			
			_showTextTween = DOVirtual.Float(0, 1, 0.5f, (v) =>
				{
					_goalTxt.color = new Color(_goalTxt.color.r, _goalTxt.color.g, _goalTxt.color.b, v);
				})
				.SetEase(Ease.Linear);
		}
	}
}