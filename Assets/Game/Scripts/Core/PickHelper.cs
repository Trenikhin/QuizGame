namespace Game.Core
{
	using QuizBoard;
	using Scripts.Configs;
	using Zenject;

	public interface IPickHelper
	{
		bool IsGoal(CardConfig card);
		bool TryPick(CardConfig card);
	}
	
	public class PickHelper : IPickHelper
	{
		[Inject] IQuizBrain _brain;
		[Inject] ILevel _level;

		public bool IsGoal(CardConfig card) => card.Identifier == _brain.GetGoal(_level.Value).Identifier;
		 
		public bool TryPick( CardConfig card )
		{
			if (!IsGoal(card))
				return false;
			
			_level.NextLevel();
			
			return true;
		}
	}
}