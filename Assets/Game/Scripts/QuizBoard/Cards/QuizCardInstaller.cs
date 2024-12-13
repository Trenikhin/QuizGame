namespace Game.QuizBoard
{
	using Scripts.Configs;
	using Zenject;

	public class QuizCardInstaller : MonoInstaller
	{
		[Inject] CardConfig _cfg;
		
		public override void InstallBindings()
		{
			Container
				.BindInterfacesAndSelfTo<QuizCardPresenter>()
				.AsSingle();
			
			Container
				.BindInterfacesAndSelfTo<QuizCardView>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			Container.BindInstance(_cfg);
		}
	}
}