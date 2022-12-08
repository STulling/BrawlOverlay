using Assets.BrawlStructs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class THCombos : MonoBehaviour
{
    AudioSource audioData;
    private TMPro.TextMeshProUGUI textMesh;
    private TMPro.TextMeshProUGUI comboMesh;
    private Animator textAnimation;
    private Animator comboAnimation;
    public BrawlPlayer player;
    private float coolDown = 0;
    private float maxCoolDown = 180;
    private List<int> currentCombo;
    private int prevQueueHash = 0;
    private Tuple<string, int, int> comboInfo;
    private bool inCombo = false;

    private void Start()
    {
        textMesh = GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0];
        textAnimation = textMesh.GetComponent<Animator>();
        comboMesh = GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1];
        comboAnimation = comboMesh.GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();
        comboInfo = new Tuple<string, int, int>("", 0, 0);
        currentCombo = new List<int>();
    }
    // Update is called once per frame
    void Update()
    {
        textMesh.fontSizeMax = 26 * (Screen.height / 600);
        // Update Combo String
        if (DMEex.HasStaleQueueChanged(player.id))
        {
            (List<int>, int) queueData = DMEex.GetStaleQueue(player.id);
            List<int> queue = queueData.Item1;
            int hash = queueData.Item2;
            if (prevQueueHash == hash)
            {
                currentCombo.Clear();
            }
            else
            {
                inCombo = true;
                int id = queue[queue.Count - 1];
                currentCombo.Add(id);
                coolDown = 0;
                textAnimation.Play("Shake");
                comboAnimation.Play("Idle");
            }
            prevQueueHash = hash;
            comboInfo = Data.GetComboString((int)player.CharID, currentCombo, audioData);
        }

        // effect stuff
        if ((player.FramesUntilHitstunEnds > 0 || player.FramesUntilGrabRelease > 0) && inCombo)
        {
            inCombo = false;
            textAnimation.Play("Fail");
            comboAnimation.Play("Fail");
        }
        coolDown++;
        if (coolDown == maxCoolDown && inCombo)
        {
            inCombo = false;
            textAnimation.Play("Show");
            comboAnimation.Play("Show");
        }
        textMesh.text = comboInfo.Item1;
        if ((textAnimation.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Show" ||
            textAnimation.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Fail"))
        {
            comboMesh.text = $"{comboInfo.Item2 * comboInfo.Item3}";
        }
        else
        {
            comboMesh.text = $"{comboInfo.Item2} X {comboInfo.Item3}";
        }
        if (!inCombo)
        {
            currentCombo.Clear();
        }
    }
}
