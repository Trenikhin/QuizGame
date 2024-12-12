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
		[Inject] ICardEvents _cardEvents;
		[Inject] CardConfig _cfg;
		
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
			_cardEvents.PickCard( _cfg );
		}
	}
}