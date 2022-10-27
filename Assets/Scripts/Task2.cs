using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class Task2 : MonoBehaviour
{
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Nozzle;
    public GameObject Hose;
    [SerializeField] HighlightEffect Highlight;

    public void NozzleOnClick()
    {
        if (Panel2.activeSelf == true) 
        {
            Nozzle.SetActive(false);
            if (Hose.activeSelf == false)
            {
                Panel2.SetActive(false);
                Panel3.SetActive(true);
                Highlight.enabled = true;
            }
        }
    }

    public void HoseOnClick()
    {
        if (Panel2.activeSelf == true) 
        {
            Hose.SetActive(false);
            if (Nozzle.activeSelf == false)
            {
                Panel2.SetActive(false);
                Panel3.SetActive(true);
                Highlight.enabled = true;
            }
        }
    }
}
