namespace Game.QuizBoard
{
	using UnityEngine;
	using Zenject;

	public interface ICoords
	{
		Vector3 GridToWorld(Vector2Int pos, int width, int height);
	}
	
	public class Coords : ICoords
	{
		[Inject] SpriteRenderer _cell;

		float CellWidth => _cell.bounds.size.x;
		float CellHeight => _cell.bounds.size.x;
		
		public Vector3 GridToWorld(Vector2Int pos, int width, int height)
		{
			width -= 1;
			height -= 1;
			
			float xOffset = (CellWidth * width) / 2;
			float xPos = CellWidth * pos.x;
			float yOffset = (CellHeight * height) / 2;
			float yPos = CellHeight * pos.y;
			
			return new Vector3( xPos - xOffset, yPos - yOffset, 0f );
		}
	}
}