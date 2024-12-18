﻿namespace Game.QuizBoard
{
	using System;
	using DG.Tweening;
	using Animations;
	using UnityEngine;
	using UnityEngine.EventSystems;
	using Zenject;

	public interface IQuizCardView
	{
		event Action OnCardClicked;
		
		void SetActive(bool active);
		void SetIcon(Sprite sprite);
		void SetPos(Vector3 pos);
		
		// Animations
		void Appear();
		void Shake();
		void ShowParticles(Action callback);
	}
	
	public class QuizCardView : MonoBehaviour, IQuizCardView, IPointerClickHandler
	{
		[SerializeField] SpriteRenderer _icon;
		[SerializeField] Collider2D _collider;
		
		[Inject] IStarParticle _particles;
		
		Tween _shakeTween;
		int _baseOrder;
		Tween _showCardTween;

		public event Action OnCardClicked;

		public void OnPointerClick(PointerEventData eventData)
		{
			OnCardClicked?.Invoke();
		}

		void Start() => _baseOrder = _icon.sortingOrder;
		
		public void SetActive(bool active) => gameObject.SetActive(active);

		public void SetIcon( Sprite sprite )
		{
			_icon.sprite = sprite;
		}

		public void SetPos(Vector3 pos)
		{
			transform.position = pos;
		}

		public void Appear()
		{
			_showCardTween = transform
				.DOScale( 1, 0.35f )
				.SetEase(Ease.OutBounce)
				.From(0);
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
			_collider.enabled = false;
			
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
				_collider.enabled = true;
				_icon.sortingOrder = _baseOrder;	 
				callback?.Invoke();
			});
		}
	}
}