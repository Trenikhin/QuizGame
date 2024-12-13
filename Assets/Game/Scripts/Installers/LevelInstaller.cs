﻿namespace Game.Installers
{
	using Core;
	using QuizBoard;
	using Scripts.Configs;
	using Ui;
	using UnityEngine;
	using Zenject;

	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] QuizCardView _cardTemplate;
		[SerializeField] LevelsConfig _levels;
		
		public override void InstallBindings()
		{
			Container
				.BindInterfacesTo<GridPlacer>()
				.FromComponentInHierarchy()
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