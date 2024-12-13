namespace Game.QuizBoard
{
	using System;
	using Zenject;
	using Game.Core;
	using Scripts.Configs;
	using UnityEngine;

	public class QuizCardPresenter : IInitializable, IDisposable, IPoolable<CardConfig, Vector3, IMemoryPool>
	{
		[Inject] IQuizCardView _view;
		[Inject] IPickHelper _pickHelper;

		[Inject] ILevel _level;
		[Inject] LevelsConfig _levels;
		
		CardConfig _cfg;
		IMemoryPool _pool;
		bool _spawned;
		
		public void Initialize()
		{
			if (!_spawned)
				_view.SetActive( false );
		}
		
		public void Dispose()
		{
			_pool?.Despawn( this );
		}
		
		public void OnSpawned(CardConfig card, Vector3 pos, IMemoryPool pool)
		{
			_spawned = true;
			
			if (_levels.FirstLevel.Identifier == _level.Value.Identifier)
				_view.Animate();
			
			_view.SetActive( true );
			_pool = pool;
			_cfg = card;
			_view.Transform.position = pos;
			_view.OnCardClicked += OnCardClicked;
			_view.SetIcon( _cfg.Icon );
		}
		
		public void OnDespawned()
		{
			_view.SetActive( false );
			_view.OnCardClicked -= OnCardClicked;
			_pool = null;
			_spawned = false;
		}

		void OnCardClicked()
		{
			if (_pickHelper.IsGoal( _cfg ))
				_view.ShowParticles(() => _pickHelper.TryPick( _cfg ));
			else
				_view.Shake();
		}
		
		public class Factory : PlaceholderFactory<CardConfig, Vector3, QuizCardPresenter> {}
	}
}