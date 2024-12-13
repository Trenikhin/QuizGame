namespace Game.QuizBoard
{
	using Scripts.Configs;
	using UnityEngine;
	using Zenject;

	public class CardFactory : IFactory<CardConfig, IQuizCardView>
	{
		[Inject] QuizCardView _cardTemplate;
		[Inject] DiContainer _container;
		[Inject] Transform _cardHolder;
		
		public IQuizCardView Create(CardConfig param)
		{
			DiContainer subContainer = _container.CreateSubContainer();
			subContainer.BindInstance( param );
			IQuizCardView view = subContainer.InstantiatePrefabForComponent< IQuizCardView >( _cardTemplate, _cardHolder );

			return view;
		}
	}
}