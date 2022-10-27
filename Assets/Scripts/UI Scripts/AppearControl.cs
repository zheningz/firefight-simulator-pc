using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearControl : MonoBehaviour
{
    public GameObject gameObject;
	
    public void Appear() 
    {
        gameObject.SetActive(true);
    }
}
