using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public static class PathFinder
    {
        private static float GetEuclideanHeuristicCost(Cell current, Cell end)
        {
            float heuristicCost = (current.pos - end.pos).magnitude;
            return heuristicCost;
        }
        

        private static List<Cell> BacktrackToPath(Cell end)
        {
            Cell current = end;
            List<Cell> path = new List<Cell>();

            while (current != null)
            {
                path.Add(current);
                current = current.PrevTile;
            }

            path.Reverse();

            return path;
        }

        public static List<Cell> FindPath_GreedyBestFirstSearch(
            MazeRenderer grid, Cell start, Cell end)
        {
            Comparison<Cell> heuristicComparison = (lhs, rhs) =>
            {
                float lhsCost = GetEuclideanHeuristicCost(lhs, end);
                float rhsCost = GetEuclideanHeuristicCost(rhs, end);

                return lhsCost.CompareTo(rhsCost);
            };

            MinHeap<Cell> frontier = new MinHeap<Cell>(heuristicComparison);
            frontier.Add(start);

            HashSet<Cell> visited = new HashSet<Cell>();
            visited.Add(start);

            start.PrevTile = null;

            while (frontier.Count > 0)
            {
                Cell current = frontier.Remove();

                if (current == end)
                {
                    break;
                }

                foreach (var neighbor in grid.GetNeighbors(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        frontier.Add(neighbor);
                        visited.Add(neighbor);
                        neighbor.PrevTile = current;
                    }
                }
            }

            List<Cell> path = BacktrackToPath(end);

            return path;
        }
    }
}