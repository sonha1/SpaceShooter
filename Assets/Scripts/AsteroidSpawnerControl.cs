using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerControl : MonoBehaviour
{

    public GameObject AsteroidGO;
    public float maxSpawnRateInSeconds = 5f;
   // public GameObject anEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnAsteroid", maxSpawnRateInSeconds);
        //
        InvokeRepeating("IncreaseSpawnRate", 0f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAsteroid()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anAsteroid = (GameObject)Instantiate((AsteroidGO));
        anAsteroid.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextAsteroidSpawn();
    }

    public void ScheduleNextAsteroidSpawn()
    {

        maxSpawnRateInSeconds = 5f;

        float spawnInNSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
            maxSpawnRateInSeconds--;
        }
        else
        {
            spawnInNSeconds = 1f;
        }
        Invoke("SpawnAsteroid", spawnInNSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
        }

        if (maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke(("IncreaseSpawnRate"));
        }

    }


    public void UnScheduleNextAsteroidSpawn()
    {
        CancelInvoke("SpawnAsteroid");
        CancelInvoke("IncreaseSpawnRate");
    }
}
