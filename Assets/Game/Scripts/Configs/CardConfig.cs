namespace Game.Configs
{
	using System;
	using UnityEngine;
	
	[Serializable]
	public class CardConfig
	{
		[field: SerializeField] public string Identifier { get; private set; }
		[field: SerializeField] public Sprite Icon  { get; private set; }
	}
}