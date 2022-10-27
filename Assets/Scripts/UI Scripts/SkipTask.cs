using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SkipTask : MonoBehaviour
{
    public GameObject Intercom;
    [SerializeField] AudioSource Audio;
    public float pressDurationTime = 3.0f;
    private float leftDownTime;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && GameUI.isPause == false)
        {
            leftDownTime -= Time.deltaTime;
            if (leftDownTime <= 0) {
                ClosePanel();
            }
            Intercom.SetActive(true);
            if (!Audio.isPlaying)
            {
                Audio.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab) && GameUI.isPause == false)
        {
            Reset();
        }
    }

    void Reset()
    {
        leftDownTime = pressDurationTime;
        Intercom.SetActive(false);
        Audio.Stop();
    }

    void ClosePanel()
    {
        SceneManager.LoadScene("End Page");
    }
}
