namespace Game.Installers
{
	using Core;
	using QuizBoard;
	using Scripts.Configs;
	using UnityEngine;
	using Zenject;

	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] LevelConfig[] _levelsConfig;
		[SerializeField] PrefabsConfig _prefabsConfig;
		
		public override void InstallBindings()
		{
			Container.BindInstance(_prefabsConfig);
			
			Container.BindInstance(_levelsConfig).AsSingle();
			
			Container
				.BindInterfacesTo<LevelSwitcher>()
				.AsSingle();
			
			Container
				.BindInterfacesTo<CardEvents>()
				.AsSingle();
			
			Container
				.BindInterfacesTo<QuizBoardPresenter>()
				.AsSingle();
			
			Container
				.BindInterfacesTo<QuizBoardView>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			Container
				.BindInterfacesTo<QuizBrain>()
				.AsSingle();
			
			// Bind card factory
			Container
				.BindFactory< CardConfig, IQuizCardView, QuizCardView.Factory>()
				.FromFactory< CardFactory >();
			
			Container
				.BindInterfacesTo<Level>()
				.AsSingle();
		}
	}
}