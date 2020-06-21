using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startPoint, endPoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    Waypoint currentSearchCenter;
    bool isRunning = true;

    List<Waypoint> path = new List<Waypoint>();

    public List<Waypoint> GetPath()
    {
        if(path.Count == 0)
        {
            LoadBlocks();
            BreadthFirstSearch();
            CreatePath();
        }
        return path;
        
    }
    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    void Start()
    {
      
    }

    private void CreatePath()
    {
        path.Add(endPoint);
        var previus = endPoint.exploredFrom;
        while(previus != startPoint)
        {
            path.Add(previus);
            previus = previus.exploredFrom;
        }
        path.Add(startPoint);
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
       queue.Enqueue(startPoint);

        while(queue.Count > 0 && isRunning)
        {
            currentSearchCenter = queue.Dequeue();
            currentSearchCenter.isExplored = true;
           // print("Searching from " + currentSearchCenter); // todo remove
            BreakIfEndWaypointFound();
            ExploreNeighbours();
        }

       // print("Finished pathfinding?");
    }

    private void BreakIfEndWaypointFound()
    {
        if(currentSearchCenter == endPoint)
        {
            //print("Searching from endPoint therefore stopping");
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) return;
        foreach(var direction in directions)
        {
            var neighbourCoordinates = currentSearchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbour(neighbourCoordinates);
            }
           
        }
    }

    private void QueueNewNeighbour(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];

        if (neighbour.isExplored || queue.Contains(neighbour))
        {

        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = currentSearchCenter;
           // print("Queueing " + neighbour);
        }
       
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach(var waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            bool isOverlapping = grid.ContainsKey(gridPos);
            if (isOverlapping)
            {
                Debug.Log("Overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
               
            }
           
        }
       // print(grid.Count);
    }

    private void ColorStartAndEnd()
    {
        endPoint.SetTopColor(Color.red);
    }
   
}
