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
    private int currentLevel = 2;

    public int Level
    {
        get { return currentLevel; }
    }

    public LevelManager Initialize()
    {
        currentState = GameState.Initialize;
        currentLevel = 2;
        SetUp();
        return this;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex < 1) return;
        if (playerGO == null || spawnManagerGO == null)
        {
            SetUp();
            return;
        }
        if ((currentState != GameState.Initialize) && (currentState != GameState.Processing))
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
                    StartCoroutine(ServiceLocator.Get<UIManager>().DisplayVictoryText());
                    StartCoroutine(GoNextLevel());
                }
                break;
            case GameState.Lose:
                {
                    currentState = GameState.Processing;
                    StartCoroutine(ServiceLocator.Get<UIManager>().DisplayDefeatText());
                    StartCoroutine(GoMainMenu());
                }
                break;
        }

    }

    public void SetUp()
    {
        spawnManagerGO = GameObject.FindGameObjectWithTag("SpawnManager")?.gameObject;
        playerGO = GameObject.FindGameObjectWithTag("Player")?.gameObject;
        ServiceLocator.Get<UIManager>().SetUp();
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                {
                    ServiceLocator.Get<AudioManager>().musicSource.Stop();
                    ServiceLocator.Get<AudioManager>().musicSource.clip = ServiceLocator.Get<AudioManager>().mainMenu;
                    ServiceLocator.Get<AudioManager>().musicSource.Play();
                }
                break;
            case 2:
                {
                    ServiceLocator.Get<AudioManager>().musicSource.Stop();
                    ServiceLocator.Get<AudioManager>().musicSource.clip = ServiceLocator.Get<AudioManager>().rain;
                    ServiceLocator.Get<AudioManager>().musicSource.Play();
                }
                break;
            case 3:
                {
                    ServiceLocator.Get<AudioManager>().musicSource.Stop();
                }
                break;
            default:
                break;
        }
        currentState = GameState.Playing;
    }

    private IEnumerator GoNextLevel()
    {
        yield return new WaitForSeconds(5.0f);
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

    public void GoMainMenuManual()
    {
        ServiceLocator.Get<AudioManager>().musicSource.Stop();
        ServiceLocator.Get<AudioManager>().musicSource.clip = ServiceLocator.Get<AudioManager>().mainMenu;
        ServiceLocator.Get<AudioManager>().musicSource.Play();
        StartCoroutine(GoMainMenu());
    }
    
    private IEnumerator GoMainMenu()
    {
        yield return new WaitForSeconds(5.0f);
        foreach (var obj in spawnManagerGO.GetComponent<EnemySpawnManager>().EnemyList)
        {
            obj.GetComponent<Enemy>().TakeDamage(1000);
        }
        foreach(var obj in GameObject.FindGameObjectsWithTag("Weapon"))
        {
            obj.GetComponent<Weapon>().DestroyGun();
        }
        ServiceLocator.Get<AudioManager>().musicSource.Stop();
        ServiceLocator.Get<AudioManager>().musicSource.clip = ServiceLocator.Get<AudioManager>().mainMenu;
        ServiceLocator.Get<AudioManager>().musicSource.Play();
        currentLevel = 2;
        SceneManager.LoadScene(1);
        ServiceLocator.Get<UIManager>().UnDisplayText();
        currentState = GameState.Initialize;
        Cursor.lockState = CursorLockMode.Confined;

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
