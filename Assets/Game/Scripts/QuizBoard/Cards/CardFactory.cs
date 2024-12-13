namespace Game.QuizBoard
{
	using Scripts.Configs;
	using Zenject;

	public class CardFactory : IFactory<CardConfig, IQuizCardView>
	{
		[Inject] QuizCardView _cardTemplate;
		[Inject] DiContainer _container;
		
		public IQuizCardView Create(CardConfig param)
		{
			DiContainer subContainer = _container.CreateSubContainer();
			subContainer.BindInstance( param );
			IQuizCardView view = subContainer.InstantiatePrefabForComponent< IQuizCardView >( _cardTemplate );

			return view;
		}
	}
}