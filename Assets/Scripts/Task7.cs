using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task7 : MonoBehaviour
{
    float countTime = 0f;
    public float returnTime = 10.0f;
    public GameObject Panel7;
    public GameObject Panel8;
	
	void Update () 
    {
        CountTime();
	}

    void CountTime() 
    {
        countTime += Time.deltaTime;
        if(countTime > returnTime)
        {
            Panel7.SetActive(false);
            Panel8.SetActive(true);
        }
    }
}
