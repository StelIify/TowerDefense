using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    void Start()
    {
        PathFinder pathinder = FindObjectOfType<PathFinder>();
        var path = pathinder.GetPath();
        StartCoroutine(MoveThroughPath(path));
    }

    IEnumerator MoveThroughPath(List<Waypoint> path)
    {
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(2f);

        }
    }
    void Update()
    {
        
    }
}
