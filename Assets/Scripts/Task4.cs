using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class Task4 : MonoBehaviour
{
    public GameObject arrow;
    public GameObject Panel4;
    public GameObject Panel5;
    [SerializeField] HighlightEffect Highlight;

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            arrow.transform.position -= transform.forward * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 3000;
        }
        if (arrow.transform.position.z <= -25.0f)
        {
            Panel4.SetActive(false);
            Panel5.SetActive(true);
            Highlight.enabled = true;
        }
    }
}
