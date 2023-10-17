using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Boss1;
    public GameObject Boss2;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnBoss1()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        GameObject anEnemy = (GameObject)Instantiate((Boss1));
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }


    void SpawnBoss2()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        GameObject anEnemy = (GameObject)Instantiate((Boss2));
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }

    public void ScheduleActiveBoss(int i)
    {
        if (i == 1)
        {
            Invoke("SpawnBoss1", 3f);
        } else if (i == 2)
        {
            Invoke("SpawnBoss2", 3f);
        }
    }
}