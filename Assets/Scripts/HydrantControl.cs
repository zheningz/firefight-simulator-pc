using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class HydrantControl : MonoBehaviour
{
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject Panel5;
    public GameObject arrow;
    public GameObject head1;
    [SerializeField] AudioSource Connect;
    [SerializeField] HighlightEffect Highlight;
    bool task3 = false;
    bool task5 = false;

    public void HydrantOnClick()
    {
        if (Panel3.activeSelf == true) 
        {
            if (task3 == false) 
            {
                Panel3.SetActive(false);
                Highlight.enabled = false;
                Panel4.SetActive(true);
                Panel5.SetActive(false);
                task3 = !task3;
                arrow.SetActive(true);
            }
        }
        if (!task5 && Panel5.activeSelf == true)
        {
            head1.SetActive(true);
            Connect.Play();
            Highlight.enabled = false;
            task5 = true;
        }
    }
}
