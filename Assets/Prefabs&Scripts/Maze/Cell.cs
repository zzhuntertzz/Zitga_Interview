using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Cell PrevTile { get; set; } //For pathFinder
    public bool hasExit = false;
    
    public Button btn;
    [SerializeField] private GameObject R, L, U, D;
    public WallState state;
    public Vector2Int pos;

    public void Init(WallState state, Vector2Int pos)
    {
        this.pos = pos;
        this.state = state;
        R.SetActive(state.HasFlag(WallState.RIGHT));
        L.SetActive(state.HasFlag(WallState.LEFT));
        U.SetActive(state.HasFlag(WallState.UP));
        D.SetActive(state.HasFlag(WallState.DOWN));
    }
}
