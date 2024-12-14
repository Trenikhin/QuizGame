namespace Game.Installers
{
	using Core;
	using QuizBoard;
	using Scripts.Configs;
	using Animations;
	using Ui;
	using UnityEngine;
	using Zenject;

	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] QuizCardView _cardTemplate;
		[SerializeField] LevelsConfig _levels;
		[SerializeField] Transform _cardHolder;
		[SerializeField] SpriteRenderer _spriteRenderer;
		
		/// <summary>
		/// Можно разделить на несколько инсталлеров, но решил не усложнять
		/// </summary>
		public override void InstallBindings()
		{
			Container
				.BindInterfacesTo<FadeAnimator>()
				.AsSingle();
			
			Container.BindInstance(_spriteRenderer);
			
			Container
				.BindInterfacesTo<Coords>()
				.AsSingle();
			
			Container
				.BindInterfacesTo<LoadScreen>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			Container
				.BindInterfacesTo<RestartView>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			Container
				.BindInterfacesTo<RestartPresenter>()
				.AsSingle();
			
			Container
				.BindInterfacesTo<StarParticle>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			Container.BindInstance(_cardTemplate);
			
			Container.BindInstance(_levels).AsSingle();
			
			Container
				.BindInterfacesTo<GameLauncher>()
				.AsSingle();
			
			Container
				.BindInterfacesTo<PickHelper>()
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
			
			Container.BindFactory<CardConfig, Vector3, QuizCardPresenter, QuizCardPresenter.Factory>()
				.FromPoolableMemoryPool( pool => pool
					.WithInitialSize(10)
					.FromSubContainerResolve()
					.ByNewContextPrefab( _cardTemplate )
					.WithGameObjectName( "Card" )
					.UnderTransform( _cardHolder )
				);
			
			Container
				.BindInterfacesTo<Level>()
				.AsSingle();
		}
	}
}