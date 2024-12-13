namespace Game.Ui
{
	using System;
	using Core;
	using Zenject;

	public class RestartPresenter : IInitializable, IDisposable
	{
		[Inject] IRestartView _view;
		[Inject] ILevelManager _levelManager;
		[Inject] ILoadScreen _loadScreen;
		
		public void Initialize()
		{
			_view.OnRestartClicked += OnRestartClicked;
			_levelManager.GameFinished += OnGameFinished;
		}

		public void Dispose()
		{
			_view.OnRestartClicked -= OnRestartClicked;
			_levelManager.GameFinished -= OnGameFinished;
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