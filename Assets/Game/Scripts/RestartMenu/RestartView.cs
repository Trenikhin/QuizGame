namespace Game.Ui
{
	using System;
	using Animations;
	using UnityEngine;
	using UnityEngine.UI;
	using Zenject;

	public interface IRestartView
	{
		public event Action OnRestartClicked;
		
		void ShowHide(bool isActive);
		void ShowBlackout();
	}
	
	public class RestartView : MonoBehaviour, IRestartView
	{
		[SerializeField] Button _restartButton;
		[SerializeField] Image _blackout;

		[Inject] IFadeAnimator _fadeAnimator;
		
		public event Action OnRestartClicked;
		
		public void Start()
		{
			_restartButton.onClick.AddListener(OnClicked);
		}

		void OnDestroy()
		{
			_restartButton.onClick.RemoveListener(OnClicked);
		}

		public void ShowHide( bool isActive )
		{
			gameObject.SetActive( isActive );
		}
		
		public void ShowBlackout()
		{
			_fadeAnimator.DoFade( _blackout, 0, 0.66f, 0.3f );
		}
		
		void OnClicked() => OnRestartClicked?.Invoke();
	}
}