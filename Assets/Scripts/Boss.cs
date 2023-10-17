using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject scoreUITextGo;

    public float minX = -5f;  // Giới hạn bên trái
    public float maxX = 5f;  // Giới hạn bên phải
    public float minY = -2f;  // Giới hạn dưới
    public float maxY = 2f;  // Giới hạn trên
    public float moveSpeed = 5f;  // Tốc độ di chuyển của boss

    private Vector2 targetPosition;  // Vị trí đích di chuyển của boss

    public GameObject ExplosionGO;

    public GameObject BossBulletGO;

    public GameObject hitEffect;
    
    public Transform firePoint;  // Vị trí xuất phát của đạn
    public float fireRate = 0.8f;  // Tốc độ bắn đạn (số lần bắn đạn mỗi giây)
    private float fireTimer = 0.2f;  // Đếm thời gian giữa các lần bắn đạn
    public int bulletCount = 10;  // Số lượng đạn trong chùm
    public float spreadAngle = 30f;  // Góc phân tán của chùm đạn
    public int health = 12;
    public int damage = 500;
    public int score = 1000;
    private GameObject enemySpawnerControl;
    private void Start()
    {
        scoreUITextGo = GameObject.FindGameObjectWithTag("TextScoreTag");
        enemySpawnerControl = GameObject.FindGameObjectWithTag("EnemySpawnerTag");
    }

    private void Update()
    {
        // Di chuyển boss đến vị trí đích
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Kiểm tra nếu boss đã đến vị trí đích, chọn một vị trí ngẫu nhiên mới
        if ((Vector2)transform.position == targetPosition)
        {
            SetRandomTargetPosition();
        }


        // Tính toán thời gian giữa các lần bắn đạn
        fireTimer += Time.deltaTime;

        // Kiểm tra nếu đến lượt bắn đạn
        if (fireTimer >= fireRate)
        {
            // Bắn đạn
            // FireBossBullet();
            ShootBurst();
            // Đặt lại đếm thời gian
            fireTimer = 0f;
        }
    }

    private void SetRandomTargetPosition()
    {
        // Chọn một vị trí ngẫu nhiên trong khoảng xác định
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        // Gán vị trí đích di chuyển mới cho boss
        targetPosition = new Vector2(randomX, randomY);
    }



    // Bắn đạn chùm
    private void ShootBurst()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");
        if (playerShip != null)
        {
            // Tính toán góc giữa các viên đạn trong chùm
            float angleStep = spreadAngle / (bulletCount - 1);
            float startAngle = -spreadAngle / 2f;

            // Bắn các viên đạn trong chùm
            for (int i = 0; i < bulletCount; i++)
            {
                // Tính toán góc của đạn hiện tại
                float bulletAngle = startAngle + (angleStep * i);

                // Tạo đạn từ prefab và thiết lập hướng di chuyển
                GameObject bullet = Instantiate(BossBulletGO, firePoint.position, Quaternion.identity);
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, bulletAngle);

                Vector2 direction = playerShip.transform.position - bullet.transform.position;
                bullet.GetComponent<EnemyBullet>().SetDirection(direction);
            }
        }
    }
    // Bắn đạn đơn
    void FireBossBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");
        if (playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate((BossBulletGO));

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
     void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("PlayerShipTag") || (col.gameObject.CompareTag("PlayerBulletTag")))
        {
            // Khi đối tượng bị bắn trúng, chạy animation bị bắn trúng
            HitAnimBoss hitAnimBoss = col.gameObject.GetComponent<HitAnimBoss>();
            if (hitAnimBoss != null)
            {
                hitAnimBoss.PlayHitAnimation();
            }

            // Hiển thị hiệu ứng khi bắn trúng
            Instantiate(hitEffect, col.contacts[0].point, Quaternion.identity);

            // Hủy đạn
            Destroy(gameObject);

           
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            health -= damage;
            if (health == 0)
            {
                PlayExplosion();
                scoreUITextGo.GetComponent<GameScore>().Score += score;
                enemySpawnerControl.GetComponent<EnemySpawnerControl>().ScheduleEnemySpawner();
                Destroy(gameObject);
            }

        }
    }



    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}