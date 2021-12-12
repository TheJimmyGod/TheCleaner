using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum GameState
    {
        Initialize,
        Playing,
        Processing,
        Win,
        Lose
    }

    private GameObject spawnManagerGO;
    private GameObject playerGO;

    private GameState currentState = GameState.Initialize;
    private int currentLevel = 1;

    public LevelManager Initialize()
    {
        currentState = GameState.Initialize;
        currentLevel = 1;
        SetUp();
        return this;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
        if (playerGO == null || spawnManagerGO == null)
        {
            SetUp();
            return;
        }
        if((currentState != GameState.Initialize) && (currentState != GameState.Processing)) 
            currentState = Condition();
        switch (currentState)
        {
            case GameState.Initialize:
            case GameState.Playing:
            case GameState.Processing:
                break;
            case GameState.Win:
                {
                    currentState = GameState.Processing;
                    ServiceLocator.Get<UIManager>().DisplayVictoryText();
                    StartCoroutine(GoNextLevel());
                }
                break;
            case GameState.Lose:
                {
                    currentState = GameState.Processing;
                    // TODO: Lose text for UI manager
                }
                break;
        }
        
    }

    public void SetUp()
    {
        spawnManagerGO = GameObject.FindGameObjectWithTag("SpawnManager")?.gameObject;
        playerGO = GameObject.FindGameObjectWithTag("Player")?.gameObject;
        ServiceLocator.Get<UIManager>().SetUp();
        currentState = GameState.Playing;
    }

    private IEnumerator GoNextLevel()
    {
        // TODO: Win text for UI Manager
        yield return new WaitForSeconds(3.0f);
        if (currentLevel < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Come On");
            currentLevel++;
            SceneManager.LoadScene(currentLevel);
            ServiceLocator.Get<UIManager>().UnDisplayText();
            currentState = GameState.Initialize;
            SetUp();
        }
    }

    private GameState Condition()
    {
        bool allAlive = true;
        if(playerGO.GetComponent<Player>().IsDead) return GameState.Lose;
        foreach(var obj in spawnManagerGO.GetComponent<EnemySpawnManager>().EnemyList)
        {
            if (obj.GetComponent<Enemy>().isDead == false) return GameState.Playing;
            else allAlive = false;
        }
        if (allAlive) return GameState.Playing;
        else return GameState.Win;
    }
}
