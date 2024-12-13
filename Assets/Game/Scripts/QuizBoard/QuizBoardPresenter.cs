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
		[Inject] ILevel _level;
		[Inject] IQuizBoardView _view;
		[Inject] ICoords  _coords;
		[Inject] IQuizBrain _brain;
		[Inject] LevelsConfig _levels;
		
		List<IQuizCardView> _cards = new List<IQuizCardView>();
		
		public void Initialize()
		{
			DrawBoard(_level.Value);
			_level.OnChanged += DrawBoard;
			_level.LastLevelCompleted += ClearBoard;
		}
		
		public void Dispose()
		{
			_level.OnChanged -= DrawBoard;
			_level.LastLevelCompleted -= ClearBoard;
		}
		
		void DrawBoard(LevelConfig lvlCfg)
		{
			bool withAnimation = _levels.FirstLevel.Identifier == lvlCfg.Identifier;
			
			ClearBoard();
			_view.SetGoal( $"Find {_brain.GetGoal(lvlCfg).Identifier}", withAnimation );

			var board = _brain.GetBoard(lvlCfg);
			var width = board.GetLength(0);
			var height = board.GetLength(1);
			
			for (var i = 0; i < width; i++)
			{
				for (var j = 0; j < height; j++)
				{
					var cardView = _view.CreateCard(board[i, j], withAnimation);
					cardView.Transform.position = _coords.GridToWorld(new Vector2Int(j, i), height, width);
					_cards.Add( cardView );
				}
			}
		}

		void ClearBoard()
		{
			_view.SetGoal("", false);
			foreach (var c in _cards)
				Object.Destroy( c.Transform.gameObject );
			_cards.Clear();
		}
	}
}