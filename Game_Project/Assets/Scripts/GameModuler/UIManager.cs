using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject crossHair;
    public GameObject text;

    public UIManager Initialize()
    {
        return this;
    }

    private void Start()
    {
        text.gameObject.SetActive(false);
        crossHair.gameObject.SetActive(false);
    }

    public void SetUp()
    {
        crossHair.gameObject.SetActive(true);
    }

    public void DisplayVictoryText()
    {
        text.gameObject.SetActive(true);
        text.transform.GetComponent<Text>().text = "Mission Completed!";
    }

    public void DisplayDefeatText()
    {
        text.gameObject.SetActive(true);
        text.transform.GetComponent<Text>().text = "Mission Failed...";
    }

    public void UnDisplayText()
    {
        text.gameObject.SetActive(false);
    }
}
