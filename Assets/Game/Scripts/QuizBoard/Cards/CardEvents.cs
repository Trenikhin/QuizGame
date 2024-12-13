namespace Game.Core
{
	using System;
	using Scripts.Configs;

	public interface ICardEvents
	{
		event Action<CardConfig> OnPick;
		
		void PickCard(CardConfig identifier);
	}
	
	public class CardEvents: ICardEvents
	{
		public event Action<CardConfig> OnPick;
		
		public void PickCard(CardConfig cfg)
		{
			OnPick?.Invoke( cfg );
		}
	}
}