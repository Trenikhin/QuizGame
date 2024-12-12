namespace Game.QuizBoard
{
	using Scripts.Configs;
	using TMPro;
	using UnityEngine;
	using UnityEngine.UI;
	using Zenject;

	public interface IQuizBoardView
	{
		QuizCardBundleView GetBundle();
		IQuizCardView GetCard( CardConfig cfg );

		void SetGoal(string goal);
	}
	
	public class QuizBoardView : MonoBehaviour, IQuizBoardView
	{
		[SerializeField] TextMeshProUGUI _goalTxt;
		[SerializeField] Transform _cardContainer;
		[SerializeField] QuizCardBundleView _cardBundleTemplate;
		
		[Inject] QuizCardView.Factory _factory;

		public QuizCardBundleView GetBundle() => Instantiate(_cardBundleTemplate, _cardContainer);
		public IQuizCardView GetCard( CardConfig cfg ) => _factory.Create( cfg );
		
		public void SetGoal( string goal ) => _goalTxt.text = goal;
	}
}