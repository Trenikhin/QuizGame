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
		
		List<IQuizCardView> _cards = new List<IQuizCardView>();
		
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
			bool withAnimation = _levels.FirstLevel.Identifier == lvlCfg.Identifier;
			
			ClearLevel();
			_view.SetGoal( $"Find {_brain.GetGoal(lvlCfg).Identifier}", withAnimation );

			var board = _brain.GetBoard(lvlCfg);
			var width = board.GetLength(0);
			var height = board.GetLength(1);
			
			for (var i = 0; i < width; i++)
			{
				for (var j = 0; j < height; j++)
				{
					var pos = _coords.GridToWorld(new Vector2Int(j, i), height, width);
					var cardView = _view.CreateCard(board[i, j], pos, withAnimation);
					_cards.Add( cardView );
				}
			}
		}

		void ClearLevel()
		{
			_view.SetGoal("", false);
			foreach (var c in _cards)
				Object.Destroy( c.Transform.gameObject );
			_cards.Clear();
		}
	}
}