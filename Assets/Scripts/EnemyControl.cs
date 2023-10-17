using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject scoreUITextGo;
    public float speed;
    public GameObject ExplosionGO;

    public GameObject BossController;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        scoreUITextGo = GameObject.FindGameObjectWithTag("TextScoreTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 postition = transform.position;
        postition =  new Vector2(postition.x, postition.y - speed * Time.deltaTime);
        transform.position = postition;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
    
     void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();
            scoreUITextGo.GetComponent<GameScore>().Score += 100;
            Destroy(gameObject);
        }
    }
     
     void PlayExplosion()
     {
         GameObject explosion = (GameObject)Instantiate(ExplosionGO);
         explosion.transform.position = transform.position;
     }
}
