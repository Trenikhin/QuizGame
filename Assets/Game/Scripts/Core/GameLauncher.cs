namespace Game.Core
{
	using Configs;
	using Zenject;

	public interface IGameLauncher
	{
		void Launch();
	}
	
	public class GameLauncher : IInitializable, IGameLauncher
	{
		[Inject] ILevel _level;
		[Inject] IQuizBrain _brain;
		[Inject] LevelsConfig _levels;
		
		public void Initialize() => Launch();
		
		public void Launch()
		{
			_brain.GenerateGoals( _levels.Levels.ToArray() );
			_level.Reset();
		}
	}
}