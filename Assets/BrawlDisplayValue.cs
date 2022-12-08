using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DolphinMemoryEngine;

public class BrawlDisplayValue : MonoBehaviour
{

    // Update is called once per frame
    public void Update()
    {
        int totalJumps = DME.Get<int>("/Total Jumps/P1");
        int jumpsUsed = DME.Get<int>("/Aerial Resources/P1/Jumps Used");
        GetComponent<TMPro.TextMeshPro>().text = (totalJumps - jumpsUsed).ToString();
    }
}
