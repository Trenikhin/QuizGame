namespace Game.QuizBoard
{
	using System;

	public interface IGameRestarter
	{
		event Action OnRestarted;

		void Restart();
	}
	
	public class GameRestarter
	{
		public event Action OnRestarted;
		
		public void Restart()
		{
			OnRestarted?.Invoke();
		}
	}
}