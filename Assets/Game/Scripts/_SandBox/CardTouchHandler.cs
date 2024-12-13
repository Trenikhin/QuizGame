namespace Game.Scripts._SandBox
{
	using UnityEngine;
	using UnityEngine.EventSystems;

	public class CardTouchHandler : MonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData) => Debug.Log("Click");
	}
}