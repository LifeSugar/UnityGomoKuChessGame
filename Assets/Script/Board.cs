using UnityEngine;

public class Board : MonoBehaviour
{
    public int rows = 15;
    public int columns = 15;
    public GameObject gridPrefab;

    private GameObject[,] gridArray;

    void Start()
    {
        gridArray = new GameObject[rows, columns];
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject grid = Instantiate(gridPrefab, new Vector2(i, j), Quaternion.identity);
                grid.name = $"Grid_{i}_{j}";
                grid.transform.parent = this.transform;
            }
        }
    }
}