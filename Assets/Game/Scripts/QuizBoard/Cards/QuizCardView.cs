namespace Game.QuizBoard
{
	using System;
	using DG.Tweening;
	using Scripts.Configs;
	using UnityEngine;
	using UnityEngine.EventSystems;
	using Zenject;

	public interface IQuizCardView
	{
		event Action OnCardClicked;

		Transform Transform { get; }
		
		void SetIcon(Sprite sprite);
		void Shake();
		void ShowParticles(Action callback);
	}
	
	public class QuizCardView : MonoBehaviour, IQuizCardView, IPointerClickHandler
	{
		[SerializeField] SpriteRenderer _icon;
		
		[Inject] IStarParticle _particles;

		Tween _shakeTween;
		int _baseOrder;
		
		public event Action OnCardClicked;
		
		public Transform Transform => transform;

		public void OnPointerClick(PointerEventData eventData) => OnCardClicked?.Invoke();

		void Start() => _baseOrder = _icon.sortingOrder;
		
		public void SetIcon( Sprite sprite )
		{
			_icon.sprite = sprite;
		}

		public void Shake()
		{
			Vector3 strength = transform.localScale.x * 0.2f * Vector3.right;
			
			_shakeTween?.Kill();
			_shakeTween = transform
				.DOShakePosition( 0.1f, strength, 30 )
				.SetLink( transform.gameObject );
		}

		public void ShowParticles( Action callback )
		{
			_icon.sortingOrder += 1;
			
			// Bounce
			transform.localScale = Vector3.one;
			Sequence bounceSequence = DOTween.Sequence();
			
			for (int i = 0; i < 1; i++)
			{
				bounceSequence.Append(transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutQuad));
				bounceSequence.Append(transform.DOScale(1f, 0.2f).SetEase(Ease.Linear));
			}
			
			bounceSequence.Play();

			_particles.Show( transform.position, () =>
			{
				_icon.sortingOrder = _baseOrder;	 
				callback?.Invoke();
			});
		}

		public class Factory : PlaceholderFactory<CardConfig ,IQuizCardView> {}
	}
}