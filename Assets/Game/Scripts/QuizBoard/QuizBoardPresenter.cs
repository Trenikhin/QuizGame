namespace Game.QuizBoard
{
	using System;
	using System.Collections.Generic;
	using Core;
	using Scripts.Configs;
	using UnityEngine;
	using Zenject;
	using Object = UnityEngine.Object;

	public class QuizBoardPresenter : IInitializable, IDisposable
	{
		[Inject] IQuizBoardView _view;
		
		[Inject] ILevel _level;
		[Inject] ICoords  _coords;
		[Inject] IQuizBrain _brain;
		[Inject] LevelsConfig _levels;
		[Inject] QuizCardPresenter.Factory _factory;
		
		List<QuizCardPresenter> _cards = new List<QuizCardPresenter>();
		
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
			ClearLevel();
			
			// Set Text
			bool withAnimation = _levels.FirstLevel.Identifier == lvlCfg.Identifier;
			_view.SetGoal( $"Find {_brain.GetGoal(lvlCfg).Identifier}", withAnimation );

			// Create Cards
			
			var board = _brain.GetBoard(lvlCfg);
			var width = board.GetLength(0);
			var height = board.GetLength(1);
			
			for (var i = 0; i < width; i++)
			{
				for (var j = 0; j < height; j++)
				{
					var pos = _coords.GridToWorld(new Vector2Int(j, i), height, width);
					var card = _factory.Create(board[i, j], pos);
					_cards.Add( card );
				}
			}
		}

		void ClearLevel()
		{
			// Clear Text
			_view.SetGoal("", false);
			
			// Clear Cards
			foreach (var c in _cards)
				c.Dispose();
			_cards.Clear();
		}
	}
}