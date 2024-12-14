namespace Game.Ui
{
	using System;
	using Core;
	using Zenject;

	public class RestartPresenter : IInitializable, IDisposable
	{
		[Inject] IRestartView _view;
		[Inject] ILoadScreen _loadScreen;
		[Inject] ILevel _level;
		
		public void Initialize()
		{
			_level.LastLevelCompleted += OnGameFinished;
			_view.OnRestartClicked += OnRestartClicked;
		}

		public void Dispose()
		{
			_level.LastLevelCompleted -= OnGameFinished;
			_view.OnRestartClicked -= OnRestartClicked;
		}
		
		void OnGameFinished()
		{
			_view.ShowHide( true );
			_view.ShowBlackout();
		}
		
		void OnRestartClicked()
		{
			_view.ShowHide( false );
			_loadScreen.Load();
		}
	}
}