namespace Game.Core
{
	using System;
	using QuizBoard;
	using Scripts.Configs;
	using UnityEngine;
	using Zenject;

	public interface ILevelManager
	{
		event Action GameFinished;
		
		LevelConfig FirstLevel { get; }
		
		void Reset();
	}
	
	public class LevelManager : IInitializable, IDisposable, ILevelManager
	{
		[Inject] ICardEvents _cardEvents;
		[Inject] ILevel _level;
		[Inject] IQuizBrain _brain;
		[Inject] LevelConfig[] _levels;
		
		public event Action GameFinished;
		
		public void Initialize()
		{
			Reset();
			_cardEvents.OnPick += OnCardPicked;
		}

		public void Dispose()
		{
			_cardEvents.OnPick -= OnCardPicked;
		}
		
		public LevelConfig FirstLevel => _levels[0];
		public LevelConfig LastLevel => _levels[_levels.Length - 1];

		void OnCardPicked( CardConfig cfg )
		{
			if (_brain.GetGoal(_level.Value).Identifier == cfg.Identifier)
			{
				if(!_level.NextLevel())
					GameFinished?.Invoke();
			}
		}

		public void Reset()
		{
			_brain.GenerateGoals( _levels );
			_level.Reset();
		}
	}
}