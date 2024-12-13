namespace Game.QuizBoard
{
	using Zenject;

	public class QuizCardInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesAndSelfTo<QuizCardPresenter>()
				.AsSingle();
			
			Container
				.BindInterfacesAndSelfTo<QuizCardView>()
				.FromComponentInHierarchy()
				.AsSingle();
		}
	}
}