using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;

public class HexComponent : MonoBehaviour
{
    public void Init(Hex hex)
    {
        this.Hex = hex;
        transform.localPosition = hex.Position;
        name = string.Format("Hex: {0},{1}", hex.gridX, hex.gridZ);
        InitializeObjectGrid();
    }
    public Hex Hex { get; protected set; }
    public SpaceGridObject[,] ObjectGrid;
    
    public Transform MapObjectHolder;
    public Transform UnitComponentHolder;
    public TextMesh DebugText;

    void InitializeObjectGrid()
    {
        ObjectGrid = new SpaceGridObject[10, 10];
        for (int i = 0; i < ObjectGrid.GetLength(0); i++)
        {
            for (int j = 0; j < ObjectGrid.GetLength(1); j++)
            {
                float xPos = (float)i - (float)(ObjectGrid.GetLength(0) - 1) / 2f;
                float zPos = (float)j - (float)(ObjectGrid.GetLength(1) - 1) / 2f;

                Vector3 position = new Vector3(xPos, 0, zPos);
                ObjectGrid[i, j] = new SpaceGridObject(position);

            }
        }
    }
}
public struct SpaceGridObject
{
    public SpaceGridObject(Vector3 position)
    {
        Position = position;
        GO = null;
    }
    public Vector3 Position;
    public GameObject GO;
}