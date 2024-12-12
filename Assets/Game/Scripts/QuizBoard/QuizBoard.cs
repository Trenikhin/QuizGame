namespace Game.QuizBoard
{
	using Scripts.Configs;

	public class QuizBoard
	{
		readonly CardsBundleConfig[] _bundles;
		
		public QuizBoard( CardsBundleConfig[] bundles )
		{
			_bundles = bundles;
		}
	}
}