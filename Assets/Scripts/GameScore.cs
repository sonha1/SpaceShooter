using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public Text scoreTextUI;
    public int score;

    public int limitPointHaveBoss = 6000;

    // public float defaultRateMakeFire = 0.5f;
    // Start is called before the first frame update
    private GameObject enemySpawnerControl;
    // private GameObject playerControl;
    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdateSoreTextUI();
        }
    }
    
    void Start()
    {
        scoreTextUI = GetComponent<Text>();
        string text = "";
        enemySpawnerControl = GameObject.FindGameObjectWithTag("EnemySpawnerTag");
    }

    // Update is called once per frame
    void UpdateSoreTextUI()
    {
        string scoreStr = string.Format("{0:0000000}", score);
        scoreTextUI.text = scoreStr;
        if(score == limitPointHaveBoss)
        {
            enemySpawnerControl.GetComponent<EnemySpawnerControl>().ScheduleActiveBoss();
        }
    }
    void Update()
    {
        
    }
}
