namespace Game.QuizBoard
{
	using Scripts.Configs;
	using UnityEngine;
	using Zenject;

	public class CardFactory : IFactory<CardConfig, Vector3, IQuizCardView>
	{
		[Inject] QuizCardView _cardTemplate;
		[Inject] DiContainer _container;
		[Inject] Transform _cardHolder;
		
		public IQuizCardView Create(CardConfig param, Vector3 pos)
		{
			DiContainer subContainer = _container.CreateSubContainer();
			subContainer.BindInstance( param );
			IQuizCardView view = subContainer.InstantiatePrefabForComponent< IQuizCardView >( _cardTemplate, _cardHolder );
			view.Transform.position = pos;
			
			return view;
		}
	}
}