using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearControl : MonoBehaviour
{
    public GameObject gameObject;

    public void Disapper() 
    {
        gameObject.SetActive(false);
    }

}