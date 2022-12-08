using DolphinMemoryEngine.MemoryWatch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ComboCounter : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textMesh;
    private BrawlPlayer player;
    public VideoPlayer videoPlayer;
    private uint prevComboCounter;
    private float coolDown = 0;
    private float maxCoolDown = 5;

    private void Start()
    {
        textMesh = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        player = GetComponentInParent<BrawlPlayer>();
        videoPlayer.loopPointReached += killVideo;
    }

    void killVideo(VideoPlayer vp)
    {
        videoPlayer.GetComponent<MeshRenderer>().enabled = false;
        videoPlayer.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        uint comboCounter = player.ComboCounter;/*
        if (comboCounter == 8 && prevComboCounter != comboCounter)
        {
            videoPlayer.Play();
            videoPlayer.GetComponent<MeshRenderer>().enabled = true;
        }*/
        if (comboCounter != prevComboCounter)
        {
            coolDown = maxCoolDown;
        }
        else
        {
            if (coolDown > 0) coolDown--;
        }
        transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(1.5f, 2f, 1), coolDown/maxCoolDown);
        prevComboCounter = comboCounter;
        if (comboCounter > 2 && comboCounter < 30000)
        {
            textMesh.text = comboCounter.ToString() + " HIT COMBO";
            float hue = Mathf.Clamp(comboCounter * 10, 0, 240);
            textMesh.color = Color.HSVToRGB(hue / 255, 1, 1);
        }
        else
        {
            textMesh.text = "";
        }
    }
}
