namespace Game.Scripts.Configs
{
	using UnityEngine;

	[CreateAssetMenu(fileName = "CardsBundleConfig", menuName = "Configs/ CardsBundleConfig", order = 0)]
	public class CardsBundleConfig : ScriptableObject
	{
		[field: SerializeField] public CardConfig[] Cards{ get; private set; }
	}
}