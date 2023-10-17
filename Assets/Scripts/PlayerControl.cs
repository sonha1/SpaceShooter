using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public GameObject GameManageGO;
    
    public GameObject PlayerBulletGO;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject ExplosionGO;

    public Text LivesUIText;
    private const int MaxLives = 3;
    private int lives;
    public float speed;
    private GameObject scoreUITextGo;
    private float minSpeedFire = 0.05f;
    public float speedFire = 0.3f;
    public float upSpeedFireRate = 0.05f;
    public float fireRate = 1f;
    private int oldScore = 0;
    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        // transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
        InvokeRepeating("MakeFire",0f, fireRate * speedFire);
    }
        // Start is called before the first frame update
        void Start()
        {
            scoreUITextGo = GameObject.FindGameObjectWithTag("TextScoreTag");
        }
    
        // Update is called once per frame
        void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
    
            Vector2 direction = new Vector2(x, y).normalized;
            Move(direction);
        }
    
        void Move(Vector2 direction)
        {
            UpdateRateMakeFire();
            Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
            
            max.x = max.x - 0.225f;
            min.x = min.x + 0.225f;
            max.y = max.y - 0.285f;
            min.y = min.y + 0.285f;
    
            Vector2 pos = transform.position;        
            
            pos += direction * speed * Time.deltaTime;
            pos.x = Mathf.Clamp (pos.x, min.x, max.x);
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
            
            transform.position = pos;
    
        }

        void MakeFire()
        {
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position;
                
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;
        }

         void OnTriggerEnter2D(Collider2D col)
        {
            if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || (col.tag == "HitBoss") || (col.tag == "AsteroidTag"))
            {
               
                lives--;
                lives = lives < 0 ? 0 : lives;
                LivesUIText.text = lives.ToString();
                if (lives == 0)
                { 
                    PlayExplosion();
                    GameManageGO.GetComponent<GameManage>().SetGameManageState(GameManage.GameManageState.GameOver);
                    gameObject.SetActive(false);
                    CancelInvoke("MakeFire");
                    speedFire = 0.3f;
                }
                // Destroy(gameObject);
            }
        }

         void PlayExplosion()
         {
             GameObject explosion = (GameObject)Instantiate(ExplosionGO);
             explosion.transform.position = transform.position;
         }

         void UpdateRateMakeFire()
         {
             int scoreNow = scoreUITextGo.GetComponent<GameScore>().Score;
             if (scoreNow > 0 && scoreNow % 100 == 0 && scoreNow != oldScore)
             {
                 oldScore = scoreNow;
                 speedFire -= upSpeedFireRate;
                 SetRateMakeFire();
             }
         }

          void SetRateMakeFire()
         {
             if (speedFire > minSpeedFire)
             {
                 Trace.WriteLine(speedFire);
                 CancelInvoke("MakeFire");
                InvokeRepeating("MakeFire",0f, fireRate * speedFire);
             }
         }
}

//
// - win game 
//     - time
//     - move animation