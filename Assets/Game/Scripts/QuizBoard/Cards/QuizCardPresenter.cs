namespace Game.QuizBoard
{
	using System;
	using Zenject;
	using Game.Core;
	using Scripts.Configs;
	using UnityEngine;

	public class QuizCardPresenter : IInitializable, IDisposable
	{
		[Inject] IQuizCardView _view;
		[Inject] ICardEvents _cardEvents;
		[Inject] CardConfig _cfg;
		[Inject] IQuizBrain _brain;
		[Inject] ILevel _level;
		
		public void Initialize()
		{
			_view.OnCardClicked += OnCardClicked;
		}

		public void Dispose()
		{
			_view.OnCardClicked -= OnCardClicked;
		}

		void OnCardClicked()
		{
			if (_cfg.Identifier == _brain.GetGoal(_level.Value).Identifier)
			{
				_view.ShowParticles( () => _cardEvents.PickCard( _cfg ) );
			}
			else
			{
				_view.Shake();
			}
		}
	}
}