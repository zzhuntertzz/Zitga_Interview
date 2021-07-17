using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] private Transform spawnItemPos;
    [SerializeField] private Player player;
    [SerializeField] private Exit exit;
    [SerializeField] private Cell[] _cells;
    [SerializeField] private LineRenderer hint;
    
    [ContextMenu("GetCells")]
    public void GetCells()
    {
        _cells = GetComponentsInChildren<Cell>();
    }
    
    private int width = 10, height = 13;
    
    // Start is called before the first frame update
    void Start()
    {
        hint.gameObject.SetActive(false);
        foreach (var cell in _cells)
        {
            cell.btn.onClick.AddListener(() => player.Move(cell));
        }
        StartCoroutine(Draw(MazeGenerator.Generate(width, height, Data.CurrentLevel)));
    }

    IEnumerator Draw(WallState[,] wallStates)
    {
        for (int h = 0; h < height; ++h)
        {
            for (int w = 0; w < width; ++w)
            {
                var state = wallStates[w, h];
                var cell = _cells[w + h * width];
                cell.Init(state, new Vector2Int(w, h));
            }
        }

        var startCell = _cells[MazeGenerator.startPos.X + MazeGenerator.startPos.Y * width];
        var endCell = _cells[MazeGenerator.endPos.X + MazeGenerator.endPos.Y * width];

        yield return new WaitForEndOfFrame();
        
        exit = Instantiate(exit, endCell.transform);
        exit.Init(endCell);
        exit.transform.SetParent(spawnItemPos);
        
        player = Instantiate(player, startCell.transform);
        player.Init(startCell);
        player.transform.SetParent(spawnItemPos);
        
        // hint.positionCount = pathCount;
        // hint.SetPositions(MazeGenerator.Paths.Select(
        //     p =>
        //     {
        //         int index = p.X + p.Y * width;
        //         return _cells[index].transform.position;
        //     }).ToArray());
    }

    public void ShowHint()
    {
        hint.gameObject.SetActive(true);
        var hintPath = PathFinding.PathFinder.FindPath_GreedyBestFirstSearch(this,
            player.currentCell, exit.currentCell);
        hint.positionCount = hintPath.Count;
        // hint.SetPositions(MazeGenerator.Paths.Select(
        //     p =>
        //     {
        //         int index = p.X + p.Y * width;
        //         return _cells[index].transform.position;
        //     }).ToArray());
        var paths = hintPath.Select(p => p.transform.position).ToArray();
        hint.SetPositions(paths);
        AutoMove(paths);
    }

    void AutoMove(Vector3[] paths)
    {
        DOTween.Kill(player.transform);
        player.transform.DOPath(paths, player.moveSpeed * paths.Length)
            .SetEase(Ease.Linear).OnWaypointChange(delegate(int value)
            {
                // Debug.Log($"done path {value}");
                LookAt2D(player.transform, Vector2.up, paths[value + 1] - player.transform.position);
            }).OnComplete(delegate
            {
                MazeGame.I.Win();
            });
    }

    void LookAt2D(Transform trans, Vector2 worldUp, Vector2 dir)
    {
        trans.eulerAngles = Vector3.forward * Vector2.SignedAngle(worldUp, dir);
    }

    //Path Finder
    public IEnumerable<Cell> GetNeighbors(Cell tile)
    {
        Cell right = GetTile(tile.pos.y, tile.pos.x + 1);
        if (right != null && !right.state.HasFlag(WallState.LEFT))
        {
            yield return right;
        }

        Cell up = GetTile(tile.pos.y + 1, tile.pos.x);
        if (up != null && !up.state.HasFlag(WallState.DOWN))
        {
            yield return up;
        }

        Cell left = GetTile(tile.pos.y, tile.pos.x - 1);
        if (left != null && !left.state.HasFlag(WallState.RIGHT))
        {
            yield return left;
        }

        Cell down = GetTile(tile.pos.y - 1, tile.pos.x);
        if (down != null && !down.state.HasFlag(WallState.UP))
        {
            yield return down;
        }
    }

    public Cell GetTile(int row, int col)
    {
        if (!IsInBounds(row, col))
        {
            return null;
        }

        return _cells[GetTileIndex(row, col)];
    }

    private bool IsInBounds(int row, int col)
    {
        bool rowInRange = row >= 0 && row < height;
        bool colInRange = col >= 0 && col < width;
        return rowInRange && colInRange;
    }

    private int GetTileIndex(int row, int col)
    {
        return row * width + col;
    }
}
