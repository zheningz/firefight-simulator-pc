using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishControl : MonoBehaviour
{
    [SerializeField] ParticleSystem Splash;
    [SerializeField] ParticleSystem Stream;
    [SerializeField] AudioSource WaterSource;
    public bool isOn = false;

    void Start()
    {
        Reset();
    }

    void Reset()
    {
        Splash.Stop();
        Stream.Stop();
        WaterSource.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameUI.isPause == false)
        {
            Splash.Play();
            Stream.Play();
            WaterSource.Play();
            isOn = true;
        } 
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Reset();
            isOn = false;
        }
    }
}
