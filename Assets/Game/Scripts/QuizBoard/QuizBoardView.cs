namespace Game.QuizBoard
{
	using DG.Tweening;
	using Scripts.Configs;
	using TMPro;
	using Tweener;
	using UnityEngine;
	using Zenject;

	public interface IQuizBoardView
	{
		IQuizCardView CreateCard( CardConfig cfg, Vector3 pos, bool hasAnimation );

		void SetGoal(string goal, bool hasAnimation);
	}
	
	public class QuizBoardView : MonoBehaviour, IQuizBoardView
	{
		[SerializeField] TextMeshProUGUI _goalTxt;
		
		[Inject] IFadeAnimator _fadeAnimator;
		[Inject] QuizCardView.Factory _factory;

		Tween _showCardTween;
		Tween _showTextTween;
		
		void OnDestroy()
		{
			_showCardTween?.Kill();
			_showTextTween?.Kill();
		}
		
		public IQuizCardView CreateCard( CardConfig cfg, Vector3 pos, bool hasAnimation )
		{
			var card = _factory.Create(cfg, pos);
			
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

			_showTextTween = _fadeAnimator.DoFade( _goalTxt, 0, 1, 0.5f );
		}
	}
}