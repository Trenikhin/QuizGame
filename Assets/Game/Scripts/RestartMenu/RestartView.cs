namespace Game.Ui
{
	using System;
	using DG.Tweening;
	using UnityEngine;
	using UnityEngine.UI;

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
			DOVirtual
				.Float(0, 0.66f, 0.3f, (v) => _blackout.color = new Color(_blackout.color.r, _blackout.color.g, _blackout.color.b, v))
				.SetEase(Ease.Linear);
		}
		
		void OnClicked() => OnRestartClicked?.Invoke();
	}
}