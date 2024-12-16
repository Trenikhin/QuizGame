namespace Game.QuizBoard
{
	using System.Collections.Generic;
	using Configs;
	using Core;
	using UnityEngine;
	using Zenject;

	public interface ICardsManager
	{
		void Spawn(LevelConfig lvlCfg);
		void Despawn();
	}
	
	public class CardsManager : ICardsManager
	{
		[Inject] ICoords  _coords;
		[Inject] IQuizBrain _brain;
		[Inject] QuizCardPresenter.Factory _factory;
		
		readonly List<QuizCardPresenter> _cards = new List<QuizCardPresenter>();
		
		public void Spawn( LevelConfig lvlCfg )
		{
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

		public void Despawn()
		{
			_cards.ForEach( c => c.Dispose() );
			_cards.Clear();
		}
	}
}