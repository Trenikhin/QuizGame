namespace Game.QuizBoard
{
	using System;
	using System.Collections.Generic;
	using Core;
	using Configs;
	using UnityEngine;
	using Zenject;

	public class QuizBoardPresenter : IInitializable, IDisposable
	{
		[Inject] IQuizBoardView _view;
		[Inject] IQuizBrain _brain;
		
		[Inject] ICardsManager _cardsManager;
		[Inject] ILevel _level;
		[Inject] LevelsConfig _levels;
		
		public void Initialize()
		{
			DrawLevel(_level.Value);
			_level.OnChanged += DrawLevel;
			_level.LastLevelCompleted += ClearLevel;
		}
		
		public void Dispose()
		{
			_level.OnChanged -= DrawLevel;
			_level.LastLevelCompleted -= ClearLevel;
		}
		
		void DrawLevel(LevelConfig lvlCfg)
		{
			// Clear Board
			_cardsManager.Despawn();
			
			// Set Text
			bool withAnimation = _levels.FirstLevel.Identifier == lvlCfg.Identifier;
			_view.SetGoal( $"Find {_brain.GetGoal(lvlCfg).Identifier}", withAnimation );

			// Create Cards
			_cardsManager.Spawn( lvlCfg );
		}

		void ClearLevel()
		{
			_view.SetGoal("", false);
			_cardsManager.Despawn();;
		}
	}
}