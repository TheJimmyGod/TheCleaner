using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class Bleeding : MonoBehaviour
{
    public PostProcessVolume volume;
    [SerializeField]
    private float mBleedTime = 3.0f;
    [SerializeField]
    private float mMaxIntensity = 0.15f;
    private Vignette bleed;

    private bool isBleed = false;

    private Player player = null;
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out bleed);
        player = transform.parent.gameObject.GetComponent<Player>();
        bleed.intensity.value = 0.0f;
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>();
            return;
        }
        if(isBleed)
            bleed.intensity.value = Mathf.Lerp(bleed.intensity.value, mMaxIntensity, Time.deltaTime * 2.0f);
        else
            if(bleed.intensity.value > 0.0f)
                bleed.intensity.value -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartBleeding());
        }
    }

    private IEnumerator StartBleeding()
    {
        isBleed = true;
        yield return new WaitForSeconds(mBleedTime);
        isBleed = false;
    }

    private IEnumerator StartFatalBleeding()
    {
        isBleed = true;
        yield return new WaitUntil(() => player.Health >= 2.0f);
        isBleed = false;
    }
}
