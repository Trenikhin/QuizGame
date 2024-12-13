namespace Game.QuizBoard
{
	using System;
	using System.Collections;
	using DG.Tweening;
	using UnityEngine;
	using UnityEngine.UI;

	public interface IStarParticle
	{
		void Show( Vector2 position, Action callback );
	}
	
	public class StarParticle : MonoBehaviour, IStarParticle
	{
		[SerializeField] Image _particle;
		[SerializeField] Transform _moveTarget;
		
		public void Show(Vector2 position, Action callback)
		{
			var pos = Camera.main.WorldToScreenPoint( position );
			
			StartCoroutine( ShowParticlesRoutine( pos, callback ) );
		}
		
		IEnumerator ShowParticlesRoutine( Vector2 position, Action callback )
		{
			_particle.transform.position = position;
			_particle.gameObject.SetActive(true);
			
			yield return _particle.transform
				.DOScale( 1, 0.3f )
				.From( 0 )
				.SetEase( Ease.OutBounce )
				.WaitForCompletion();

			// Fly
			_particle.transform.DOScale(0, 0.3f).From(0);
			
			yield return _particle.transform
				.DOMove( _moveTarget.position, 0.3f )
				.WaitForCompletion();
			
			_particle.gameObject.SetActive(false);
			
			callback?.Invoke();
		}
	}
}