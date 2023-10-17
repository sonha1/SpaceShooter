using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    
    public enum GameManageState
    {
        Opening,
        GamePlay,
        GameOver
    }

    private GameManageState GMState;
    void Start()
    {
        GMState = GameManageState.Opening;
    }

    // Update is called once per frame
    void UpdateGameManageState()
    {
        switch (GMState)
        {
            case GameManageState.Opening:
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                enemySpawner.GetComponent<EnemySpawnerControl>().UnScheduleNextEnemySpawn();
              
                break;
            case GameManageState.GamePlay:
                enemySpawner.GetComponent<EnemySpawnerControl>().unActiveBoss();
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                GameOverGO.SetActive(false);
                playerShip.GetComponent<PlayerControl>().Init();
                enemySpawner.GetComponent<EnemySpawnerControl>().ScheduleNextEnemySpawn();
                break;
            case GameManageState.GameOver:
                enemySpawner.GetComponent<EnemySpawnerControl>().UnScheduleNextEnemySpawn();
                GameOverGO.SetActive(true);
                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }

    public void SetGameManageState(GameManageState state)
    {
        GMState  = state;
        UpdateGameManageState();
    }

    public void StartGamePlay()
    {
        GMState = GameManageState.GamePlay;
        UpdateGameManageState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManageState(GameManageState.Opening);
    }
}
