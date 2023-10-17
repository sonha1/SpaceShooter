using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartGenerator : MonoBehaviour
{

    public GameObject StartGO;
    // Start is called before the first frame update
    public int MaxStarts;
    private Color[] startColors =
    {
        new Color(0.5f, 0.5f, 1f),
        new Color(0, 1f, 1f),
        new Color(1f, 1f, 0f),
        new Color(1f, 0f, 0f),
    };
    void Start()
    {
        
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        for (int i = 0; i < MaxStarts; i++)
        {
            GameObject star = (GameObject)Instantiate(StartGO);

            star.GetComponent<SpriteRenderer>().color = startColors[i % startColors.Length];

            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);
            star.transform.parent = transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
