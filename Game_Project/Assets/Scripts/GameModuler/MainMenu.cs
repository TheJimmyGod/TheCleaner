using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int levelToLoad = 0;

    public void OnLevelButtonClick()
    {
        StartCoroutine(LoadLevelRoutine());
    }

    private void Awake()
    {
        levelToLoad = ServiceLocator.Get<LevelManager>().Level;
        ServiceLocator.Get<AudioManager>().musicSource.Stop();
        ServiceLocator.Get<AudioManager>().musicSource.clip = ServiceLocator.Get<AudioManager>().mainMenu;
        ServiceLocator.Get<AudioManager>().musicSource.Play();
    }

    private IEnumerator LoadLevelRoutine()
    {
        yield return SceneManager.LoadSceneAsync(levelToLoad);
    }
}
