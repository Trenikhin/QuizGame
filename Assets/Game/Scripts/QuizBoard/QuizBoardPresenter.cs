namespace Game.QuizBoard
{
	using System;
	using System.Collections.Generic;
	using Core;
	using Scripts._SandBox;
	using Scripts.Configs;
	using UnityEngine;
	using Zenject;
	using Object = UnityEngine.Object;

	public class QuizBoardPresenter : IInitializable, IDisposable
	{
		[Inject] ILevel _level;
		[Inject] IQuizBoardView _view;
		[Inject] IGridPlacer _gridPlacer;
		[Inject] IQuizBrain _brain;
		[Inject] ILevelManager _levelManager;
		
		List<IQuizCardView> _cards = new List<IQuizCardView>();
		
		public void Initialize()
		{
			DrawBoard(_level.Value);
			_level.OnChanged += DrawBoard;
		}
		
		public void Dispose()
		{
			_level.OnChanged -= DrawBoard;
		}
		
		void DrawBoard(LevelConfig lvlCfg)
		{
			bool withAnimation = _levelManager.FirstLevel.Identifier == lvlCfg.Identifier;
			
			ClearBoard();
			_view.SetGoal( $"Find {_brain.GetGoal(lvlCfg).Identifier}", withAnimation );

			Transform[,] cards = new Transform[lvlCfg.Bundles.Length, lvlCfg.Bundles[0].Cards.Length];
			
			for (int i = 0; i < lvlCfg.Bundles.Length; i++)
			{
				for (int j = 0; j < lvlCfg.Bundles[i].Cards.Length; j++)
				{
					var cardCfg = lvlCfg.Bundles[i].Cards[j];
					var card = CreateCard(cardCfg, withAnimation );
					
					cards[i, j] = card.Transform;
				}
			}
			
			_gridPlacer.Place( cards );
		}
		
		IQuizCardView CreateCard(CardConfig cardCfg, bool animate)
		{
			var cardView = _view.CreateCard(cardCfg, animate);
			cardView.SetIcon( cardCfg.Icon );
			_cards.Add( cardView );

			return cardView;
		}

		void ClearBoard()
		{
			foreach (var c in _cards)
				Object.Destroy( c.Transform.gameObject );
			_cards.Clear();
		}
	}
}