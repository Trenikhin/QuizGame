namespace Game.QuizBoard
{
	using System;
	using Zenject;
	using Game.Core;
	using Scripts.Configs;
	using UnityEngine;

	public class QuizCardPresenter : IInitializable, IDisposable
	{
		[Inject] IQuizCardView _view;
		[Inject] CardConfig _cfg;
		[Inject] IPickHelper _pickHelper;
		
		public void Initialize()
		{
			_view.OnCardClicked += OnCardClicked;
		}

		public void Dispose()
		{
			_view.OnCardClicked -= OnCardClicked;
		}

		void OnCardClicked()
		{
			if (_pickHelper.IsGoal( _cfg ))
				_view.ShowParticles(() => _pickHelper.TryPick( _cfg ));
			else
				_view.Shake();
		}
	}
}