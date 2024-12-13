namespace Game.QuizBoard
{
	using System;
	using System.Collections;
	using DG.Tweening;
	using Scripts.Configs;
	using UnityEngine;
	using UnityEngine.UI;
	using Zenject;

	public interface IQuizCardView
	{
		event Action OnCardClicked;

		Transform Transform { get; }
		
		void SetIcon(Sprite sprite);

		void Shake();
		void ShowParticles(Action callback);
	}
	
	public class QuizCardView : MonoBehaviour, IQuizCardView
	{
		[SerializeField] Image _icon;
		[SerializeField] Button _cardButton;

		[Inject] IStarParticle _particles;
		
		public event Action OnCardClicked;
		
		public Transform Transform => transform;
		
		public void SetIcon( Sprite sprite )
		{
			_icon.sprite = sprite;
			_icon.SetNativeSize();
			_icon.rectTransform.sizeDelta *= 0.25f;
		}

		public void Start()
		{
			_cardButton.onClick.AddListener(OnClicked);
		}

		void OnDestroy()
		{
			_cardButton.onClick.RemoveListener(OnClicked);
		}

		public void Shake()
		{
			Vector3 strength = transform.localScale.x * 15 * Vector3.right;
			
			transform
				.DOShakePosition( 0.1f, strength, 30 )
				.SetLink( transform.gameObject );
		}

		public void ShowParticles( Action callback )
		{
			// Bounce
			transform.localScale = Vector3.one;
			Sequence bounceSequence = DOTween.Sequence();
			
			for (int i = 0; i < 1; i++)
			{
				bounceSequence.Append(transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutQuad));
				bounceSequence.Append(transform.DOScale(1f, 0.2f).SetEase(Ease.Linear));
			}
			
			bounceSequence.Play();

			_particles.Show( transform.position, callback );
		}
		
		void OnClicked() => OnCardClicked?.Invoke();
		
		public class Factory : PlaceholderFactory<CardConfig ,IQuizCardView> {}
	}
}