using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToStart : MonoBehaviour 
{
    [SerializeField] AudioSource Audio;
    float countTime = 0f;
    public float returnTime = 5.0f;
	
    void Start()
    {
        Audio.Play();
    }

	void Update () 
    {
        CountTime();
	}

    void CountTime() 
    {
        countTime += Time.deltaTime;
        if(countTime > returnTime)
        {
            SceneManager.LoadScene("Start Page");
        }
    }
}