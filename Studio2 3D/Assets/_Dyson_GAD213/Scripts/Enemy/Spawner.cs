using System;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    #region Variables
    public GameObject enemy;
    public Transform firstSpawner;
    public Transform secondSpawner;
    public Transform thirdSpawner;
    #endregion Variables
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2.0f, 5.0f);
    }

    void SpawnEnemy()
    {
        var enemyPosition = UnityEngine.Random.Range(1, 3);

        if (enemyPosition == 1)
            Instantiate(enemy, firstSpawner.transform);
        if (enemyPosition == 2)
            Instantiate(enemy, secondSpawner.transform);
        if (enemyPosition == 3)
            Instantiate(enemy, thirdSpawner.transform);
    }
}
