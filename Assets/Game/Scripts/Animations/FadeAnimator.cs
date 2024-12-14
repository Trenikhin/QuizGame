namespace Game.Animations
{
	using DG.Tweening;
	using TMPro;
	using UnityEngine;
	using UnityEngine.UI;

	public interface IFadeAnimator
	{
		Tween DoFade(Graphic obj, float from, float to, float duration);
	}
	
	public class FadeAnimator : IFadeAnimator
	{
		public Tween DoFade( Graphic obj, float from, float to, float duration )
		{
			return DOVirtual
				.Float(from, to, duration, (v) =>
				{
					obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, v);
				})
				.SetEase(Ease.Linear);
		}
	}
}