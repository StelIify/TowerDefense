using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToMove;

    [SerializeField] GameObject bullet;

    [SerializeField] float fireDistance = 20f;

    Transform Enemy;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        FireCalculation();
        SetTargetEnemy();
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if(sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach(var testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        Enemy = closestEnemy;

    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if(distToA < distToB)
        {
            return transformA;
        }
        return transformB;


    }

    private void FireCalculation()
    {
        if (Enemy == null) return;

        var distanceFromTowerToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);

        if(distanceFromTowerToEnemy <= fireDistance)
        {
            objectToMove.LookAt(Enemy);
            bullet.SetActive(true);
        }
        else
        {
            bullet.SetActive(false);
        }

    }

}
