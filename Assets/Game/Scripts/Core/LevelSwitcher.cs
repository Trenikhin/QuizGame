namespace Game.Core
{
	using System;
	using QuizBoard;
	using Scripts.Configs;
	using UnityEngine;
	using Zenject;

	public class LevelSwitcher : IInitializable, IDisposable
	{
		[Inject] ICardEvents _cardEvents;
		[Inject] ILevel _level;
		[Inject] IQuizBrain _brain;
		[Inject] LevelConfig[] _levels;
		
		public void Initialize()
		{
			Reset();
			_cardEvents.OnPick += OnCardPicked;
		}

		public void Dispose()
		{
			_cardEvents.OnPick -= OnCardPicked;
		}

		void OnCardPicked( CardConfig cfg )
		{
			if (_brain.GetGoal(_level.Value).Identifier == cfg.Identifier)
			{
				if (!_level.NextLevel())
				{
					Reset();
				}
			}
		}

		void Reset()
		{
			_brain.GenerateGoals( _levels );
			_level.Reset();
		}
	}
}