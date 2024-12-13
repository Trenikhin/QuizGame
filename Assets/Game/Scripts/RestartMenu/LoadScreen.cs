namespace Game.Ui
{
	using System.Collections;
	using Core;
	using DG.Tweening;
	using UnityEngine;
	using UnityEngine.UI;
	using Zenject;

	public interface ILoadScreen
	{
		void Load();
	}
	
	public class LoadScreen : MonoBehaviour, ILoadScreen
	{
		[SerializeField] Image _image;
		[SerializeField] float _fadeDuration = 0.5f;	
		
		[Inject] ILevelManager _levelManager;
		
		public void Load()
		{
			gameObject.SetActive( true );
			StartCoroutine(nameof(LoadLevelRoutine));
		}

		IEnumerator LoadLevelRoutine()
		{
			const float fadeDuration = .5f;
			
			_levelManager.Reset();
			DoFade( 0, 1, _fadeDuration);
			
			yield return new WaitForSeconds(0.5f);
			DoFade(1, 0, _fadeDuration);
			
			yield return new WaitForSeconds(_fadeDuration);
			gameObject.SetActive( false );
		}

		void DoFade( float from, float to, float duration )
		{
			DOVirtual
				.Float(from, to, duration, (v) => _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, v))
				.SetEase(Ease.Linear);
		}
	}
}