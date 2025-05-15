using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }

    private CinemachineImpulseSource cinemachinempulseSource;

    private void Awake()
    {
        Instance = this;

        cinemachinempulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float intensity = 1f)
    {
        cinemachinempulseSource.GenerateImpulse(intensity);
    }
}
