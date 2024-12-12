namespace Game.QuizBoard
{
	using System;
	using Scripts.Configs;
	using UnityEngine;
	using UnityEngine.UI;
	using Zenject;

	public interface IQuizCardView
	{
		event Action OnCardClicked;

		Transform Transform { get; }
		
		void SetIcon(Sprite sprite);
	}
	
	public class QuizCardView : MonoBehaviour, IQuizCardView
	{
		[SerializeField] Image _icon;
		[SerializeField] Button _cardButton;
		
		public event Action OnCardClicked;
		
		public Transform Transform => transform;
		
		public void SetIcon( Sprite sprite ) => _icon.sprite = sprite;

		public void Start()
		{
			_cardButton.onClick.AddListener(OnClicked);
		}

		void OnDestroy()
		{
			_cardButton.onClick.RemoveListener(OnClicked);
		}
		
		void OnClicked() => OnCardClicked?.Invoke();
		
		public class Factory : PlaceholderFactory<CardConfig ,IQuizCardView> {}
	}
}