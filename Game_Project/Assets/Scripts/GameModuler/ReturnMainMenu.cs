using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnMainMenu : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void trigger()
    {
        ServiceLocator.Get<LevelManager>().GoMainMenuManual();
    }
}
