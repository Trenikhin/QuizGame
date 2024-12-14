namespace Game.QuizBoard
{
	using DG.Tweening;
	using TMPro;
	using Animations;
	using UnityEngine;
	using Zenject;

	public interface IQuizBoardView
	{
		void SetGoal(string goal, bool hasAnimation);
	}
	
	public class QuizBoardView : MonoBehaviour, IQuizBoardView
	{
		[SerializeField] TextMeshProUGUI _goalTxt;
		
		[Inject] IFadeAnimator _fadeAnimator;
		
		Tween _showTextTween;
		
		void OnDestroy()
		{
			_showTextTween?.Kill();
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