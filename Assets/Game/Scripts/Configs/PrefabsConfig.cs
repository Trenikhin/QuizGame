namespace Game.Scripts.Configs
{
	using QuizBoard;
	using UnityEngine;
	
	[CreateAssetMenu(fileName = "PrefabsConfig", menuName = "Configs/ PrefabsConfig", order = 0)]
	public class PrefabsConfig : ScriptableObject
	{
		[field: SerializeField] public QuizCardView CardTemplate { get; private set; }
	}
}