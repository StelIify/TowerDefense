using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawns = 3f;

    [SerializeField] GameObject Enemy;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Instantiate(Enemy, transform.position, Enemy.transform.rotation);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
