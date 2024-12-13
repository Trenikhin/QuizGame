namespace Game.QuizBoard
{
	using UnityEngine;

	public interface IGridPlacer
	{
		void Place(Transform[,] cells);
	}
	
	public class GridPlacer : MonoBehaviour, IGridPlacer
	{
		[SerializeField] SpriteRenderer _cell;
		[SerializeField] float _spacing = 0f;

		public void Place( Transform[,] cells )
		{
			int rows = cells.GetLength(0);
			int columns = cells.GetLength(1);
			float spriteWidth = _cell.bounds.size.x;
			float spriteHeight = _cell.bounds.size.y;
			
			Vector3 startPosition = new Vector3(-((columns - 1) * (spriteWidth + _spacing)) / 2, ((rows - 1) * (spriteHeight + _spacing)) / 2, 0);

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < columns; col++)
				{
					float xPosition = startPosition.x + col * (spriteWidth + _spacing);
					float yPosition = startPosition.y - row * (spriteHeight + _spacing);
					
					cells[row, col].transform.position = new Vector3(xPosition, yPosition, 0);
					cells[row, col].transform.SetParent(transform);
				}
			}
		}
	}
}