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
		
		List<IQuizCardView> _cards = new List<IQuizCardView>();
		List<QuizCardBundleView> _bundles = new List<QuizCardBundleView>();
		
		public void Initialize()
		{
			OnLevelChanged(_level.Value);
			_level.OnChanged += OnLevelChanged;
		}
		
		public void Dispose()
		{
			_level.OnChanged -= OnLevelChanged;
		}
		
		void OnLevelChanged(LevelConfig lvlCfg)
		{
			foreach (var c in _cards)
				GameObject.Destroy( c.Transform.gameObject );
			_cards.Clear();
			foreach (var b in _bundles)
				GameObject.Destroy( b.gameObject );
			_bundles.Clear();
			
			_view.SetGoal( $"Find {_brain.GetGoal(lvlCfg).Identifier}" );
			
			foreach (var bundleCfg in lvlCfg.Bundles)
			{
				var bundleView = _view.GetBundle();
				_bundles.Add( bundleView );
				
				foreach (var cardCfg in bundleCfg.Cards)
				{
					CreateCard(cardCfg, bundleView);
				}
			}
		}
		
		void CreateCard(CardConfig cardCfg, QuizCardBundleView bundleView)
		{
			var cardView = _view.GetCard(cardCfg);
			cardView.SetIcon( cardCfg.Icon );
			cardView.Transform.SetParent(bundleView.transform);
			_cards.Add( cardView );
		}
	}
}