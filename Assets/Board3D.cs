using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board3D : MonoBehaviour
{
    public GameObject cellPrefab; // 3D棋盘格子预制件
    public int rows = 15;
    public int columns = 15;

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < columns; z++)
            {
                Vector3 position = new Vector3(x, 0, z);
                Instantiate(cellPrefab, position, Quaternion.Euler(0, 0, 90), transform);
            }
        }
    }
}