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

    public IEnumerator DisplayVictoryText()
    {
        text.gameObject.SetActive(true);
        text.transform.GetComponent<Text>().text = "Mission Completed!";
        yield return new WaitForSeconds(5.0f);
        UnDisplayText();
    }

    public IEnumerator DisplayDefeatText()
    {
        text.gameObject.SetActive(true);
        text.transform.GetComponent<Text>().text = "Mission Failed...";
        yield return new WaitForSeconds(5.0f);
        UnDisplayText();
    }

    public void UnDisplayText()
    {
        text.gameObject.SetActive(false);
    }
}
