using UnityEngine;

public class Exit : MonoBehaviour
{
    public Cell currentCell;

    public void Init(Cell cell)
    {
        currentCell = cell;
        cell.hasExit = true;
    }
}