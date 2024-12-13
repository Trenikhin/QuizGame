namespace Game.Core
{ 
	using System;
	using Scripts.Configs;
	using UnityEngine;
	using Zenject;

	public interface ILevel
	{
		LevelConfig Value { get; }
		
		event Action<LevelConfig> OnChanged;

		void Reset();
		bool NextLevel();
	}
	
	public class Level : ILevel
	{
		[Inject] LevelConfig[] _levels;

		public event Action<LevelConfig> OnChanged;
		
		public LevelConfig Value { get; private set; }
		
		int _cur;

		public void Reset()
		{
			_cur = 0;
			NextLevel();
		}

		public bool NextLevel()
		{
			if (_cur >= _levels.Length )
				return false;
			
			Value = _levels[_cur];
			OnChanged?.Invoke(Value);
			_cur++;
			
			return true;
		}
	}
}