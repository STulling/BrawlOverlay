using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DolphinMemoryEngine.MemoryWatch;

public class BrawlPositionBind : MonoBehaviour
{
    // Update is called once per frame
    public void Update()
    {
        this.transform.position = GetComponent<BrawlPlayer>().Position;
    }
}
