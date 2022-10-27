using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBoxControl : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    [SerializeField] AudioSource Switch;
    [SerializeField] AudioSource PowerOff;
    bool task1 = false;

    public void BoxOnClick()
    {
        if (task1 == false)
        {
            Switch.Play();
            PowerOff.Play();
            task1 = !task1;
            Panel1.SetActive(false);
            Panel2.SetActive(true);
        }
    }
}
