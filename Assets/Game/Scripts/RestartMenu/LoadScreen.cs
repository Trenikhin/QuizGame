namespace Game.Ui
{
	using System.Collections;
	using Core;
	using DG.Tweening;
	using Tweener;
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
		
		[Inject] IFadeAnimator _fadeAnimator;
		[Inject] IGameLauncher _gameLauncher;
		
		public void Load()
		{
			gameObject.SetActive( true );
			StartCoroutine(nameof(LoadLevelRoutine));
		}

		IEnumerator LoadLevelRoutine()
		{
			_fadeAnimator.DoFade( _image, 0, 1, _fadeDuration );
			
			yield return new WaitForSeconds(1f);
			_fadeAnimator.DoFade( _image, 1, 0, _fadeDuration );
			
			yield return new WaitForSeconds(_fadeDuration);
			gameObject.SetActive( false );
			_gameLauncher.Launch();
		}
	}
}