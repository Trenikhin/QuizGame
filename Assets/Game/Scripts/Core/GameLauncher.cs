namespace Game.Core
{
	using QuizBoard;
	using Scripts.Configs;
	using Zenject;

	public interface IGameLauncher
	{
		void Reset();
	}
	
	public class GameLauncher : IInitializable, IGameLauncher
	{
		[Inject] ILevel _level;
		[Inject] IQuizBrain _brain;
		[Inject] LevelsConfig _levels;
		
		public void Initialize() => Reset();
		
		public void Reset()
		{
			_brain.GenerateGoals( _levels.Levels.ToArray() );
			_level.Reset();
		}
	}
}