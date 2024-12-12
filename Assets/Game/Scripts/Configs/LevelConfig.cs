namespace Game.Scripts.Configs
{
	using UnityEngine;

	[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/ LevelConfig", order = 0)]
	public class LevelConfig : ScriptableObject
	{
		[field: SerializeField] public string Identifier { get; private set; }
		
		[field: SerializeField] public CardsBundleConfig[] Bundles { get; private set; }
	}
}