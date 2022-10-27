using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTask : MonoBehaviour
{
    public GameObject PanelThis;
    public GameObject PanelNext;

    public void Next()
    {
        PanelThis.SetActive(false);
        PanelNext.SetActive(true);
    }
}