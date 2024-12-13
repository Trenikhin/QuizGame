namespace Game.QuizBoard
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Core;
	using Scripts.Configs;
	using UnityEngine;
	using Zenject;

	public class QuizBoardPresenter : IInitializable, IDisposable
	{
		[Inject] ILevel _level;
		[Inject] IQuizBoardView _view;
		[Inject] IQuizBrain _brain;
		[Inject] ILevelManager _levelManager;
		
		List<IQuizCardView> _cards = new List<IQuizCardView>();
		List<QuizCardBundleView> _bundles = new List<QuizCardBundleView>();
		
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
			
			foreach (var bundleCfg in lvlCfg.Bundles)
			{
				var bundleView = _view.GetBundle();
				_bundles.Add( bundleView );
				
				foreach (var cardCfg in bundleCfg.Cards)
				{
					CreateCard(cardCfg, bundleView, withAnimation );
				}
			}
		}
		
		void CreateCard(CardConfig cardCfg, QuizCardBundleView bundleView, bool animate)
		{
			var cardView = _view.CreateCard(cardCfg, animate);
			cardView.SetIcon( cardCfg.Icon );
			cardView.Transform.SetParent(bundleView.transform);
			_cards.Add( cardView );
		}

		void ClearBoard()
		{
			foreach (var c in _cards)
				GameObject.Destroy( c.Transform.gameObject );
			_cards.Clear();
			foreach (var b in _bundles)
				GameObject.Destroy( b.gameObject );
			_bundles.Clear();
		}
	}
}