using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = .3f;
    public Cell currentCell;

    public void Init(Cell startCell)
    {
        currentCell = startCell;
    }

    public bool Move(Cell toCell)
    {
        if (DOTween.IsTweening(transform)) return false;
        bool canMove = false;
        
        if (currentCell.pos.y == toCell.pos.y)
        {
            if (currentCell.pos.x + 1 == toCell.pos.x) //Right
            {
                canMove = !toCell.state.HasFlag(WallState.LEFT);
            }
            else
            if (currentCell.pos.x - 1 == toCell.pos.x) //Left
            {
                canMove = !toCell.state.HasFlag(WallState.RIGHT);
            }
        }
        else
        if (currentCell.pos.x == toCell.pos.x)
        {
            if (currentCell.pos.y + 1 == toCell.pos.y) //Up
            {
                canMove = !toCell.state.HasFlag(WallState.DOWN);
            }
            else
            if (currentCell.pos.y - 1 == toCell.pos.y) //Down
            {
                canMove = !toCell.state.HasFlag(WallState.UP);
            }
        }
        
        if (canMove)
        {
            Debug.Log($"Move to {toCell.pos}");
            transform.DOMove(toCell.transform.position, moveSpeed).OnComplete(delegate
            {
                currentCell = toCell;
                if (currentCell.hasExit)
                {
                    MazeGame.I.Win();
                }
            });
            return true;
        }

        return false;
    }
}
