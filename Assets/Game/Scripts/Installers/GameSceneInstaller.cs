namespace Game.Installers
{
	using Core;
	using QuizBoard;
	using Configs;
	using Animations;
	using Ui;
	using UnityEngine;
	using Zenject;

	public class GameSceneInstaller : MonoInstaller
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
			BindCore();
			BindQuizBoard();
			BindTweens();
			BindUi();
		}

		void BindTweens()
		{
			Container
				.BindInterfacesTo<FadeAnimator>()
				.AsSingle();

			Container
				.BindInterfacesTo<StarParticle>()
				.FromComponentInHierarchy()
				.AsSingle();
		}

		void BindQuizBoard()
		{
			Container.BindInstance(_cardTemplate);
			Container.BindInstance(_spriteRenderer);
			
			Container.BindFactory<CardConfig, Vector3, QuizCardPresenter, QuizCardPresenter.Factory>()
				.FromPoolableMemoryPool( pool => pool
					.WithInitialSize(10)
					.FromSubContainerResolve()
					.ByNewContextPrefab( _cardTemplate )
					.WithGameObjectName( "Card" )
					.UnderTransform( _cardHolder )
				);
			
			Container
				.BindInterfacesTo<QuizBoardPresenter>()
				.AsSingle();
			
			Container
				.BindInterfacesTo<QuizBoardView>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			Container
				.BindInterfacesTo<Coords>()
				.AsSingle();
		}

		void BindCore()
		{
			Container
				.BindInterfacesTo<QuizBrain>()
				.AsSingle();
			
			Container.BindInstance(_levels).AsSingle();
			
			Container
				.BindInterfacesTo<GameLauncher>()
				.AsSingle();
			
			Container
				.BindInterfacesTo<PickHelper>()
				.AsSingle();
			
			Container
				.BindInterfacesTo<Level>()
				.AsSingle();
		}

		void BindUi()
		{
			Container
				.BindInterfacesTo<RestartPresenter>()
				.AsSingle();
			
			Container
				.BindInterfacesTo<RestartView>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			Container
				.BindInterfacesTo<LoadScreen>()
				.FromComponentInHierarchy()
				.AsSingle();
		}
	}
}