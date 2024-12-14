namespace Game.Configs
{
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "Levels", menuName = "Configs/ Levels", order = 0)]
	public class LevelsConfig : ScriptableObject
	{
		[field: SerializeField] public List<LevelConfig> Levels { get; private set; }
		
		public LevelConfig FirstLevel => Levels[0];
		public LevelConfig LastLevel => Levels[Levels.Count - 1];
	}
}