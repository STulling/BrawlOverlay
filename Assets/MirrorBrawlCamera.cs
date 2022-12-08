using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DolphinMemoryEngine;
using System;

public class MirrorBrawlCamera : MonoBehaviour
{
    Camera m_MainCamera;

    public void Start()
    {
        m_MainCamera = Camera.main;
    }

    // Update is called once per frame
    public void Update()
    {
        Matrix4x4 projectionMatrix = Matrix4x4.zero;

        float f1 = 57.2958f * DME.Read<float>(0x805b6d20 + 0xD8);
        float f2 = DME.Read<float>(0x805b6d20 + 0xE4);
        float f3 = DME.Read<float>(0x805b6d20 + 0xDC);
        float f4 = DME.Read<float>(0x805b6d20 + 0xE0);

        float t = (float)Math.Tan(0.5 * 0.017453292 * f1);

        projectionMatrix[0, 0] = 1.0f / (t * f2);
        projectionMatrix[1, 1] = 1.0f / t;
        projectionMatrix[2, 2] = (-f3 / (f4 - f3));
        projectionMatrix[2, 3] = -(f3 * f4 / (f4 - f3));
        projectionMatrix[3, 2] = -1;

       m_MainCamera.projectionMatrix = projectionMatrix;

        Matrix4x4 viewMatrix = Matrix4x4.zero;
        for (int i = 0; i < 12; i++)
        {
            viewMatrix[i] = DME.Read<float>((uint)(0x805b6d20 + i * 4));
        }
        viewMatrix[15] = 1;
        m_MainCamera.worldToCameraMatrix = viewMatrix.transpose;

    }
}
