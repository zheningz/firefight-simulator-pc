using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LinjingSUN;

public class SceneControl : MonoBehaviour
{
    private GameObject LoadPanel;
    private GameObject ButtonPanel;
    private GameObject canvas;

    public static int scenario;
    //  1: computer;
    //  2: shelf;
    //  3: room;
    //  4: out;    

    private void Start() {
        if(SceneManager.GetActiveScene().name == "Select Page")
        {
            canvas = GameObject.Find("Canvas");
            LoadPanel = canvas.transform.Find("Load").gameObject;
            ButtonPanel = canvas.transform.Find("Button").gameObject;

            scenario = 0;
        }
    }

    public void StartScene()
    {
        SceneManager.LoadScene("Select Page");
    }

    public void Quit()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void StartWarehouse()
    {
        scenario = 4;
        LoadPanel.SetActive(true);
        ButtonPanel.SetActive(false);
        SceneManager.LoadSceneAsync("Warehouse");
    }

    public void StartComputerFire()
    {
        scenario = 1;
        ShelvingController.success = false;
        LoadPanel.SetActive(true);
        ButtonPanel.SetActive(false);
        SceneManager.LoadSceneAsync("computer fire");
    }

    public void StartShelfFire()
    {
        scenario = 2;
        ShelvingController.success = false;
        LoadPanel.SetActive(true);
        ButtonPanel.SetActive(false);
        SceneManager.LoadSceneAsync("shelf fire");
    }    

    public void StartRoomFire()
    {
        scenario = 3;
        ShelvingController.success = false;
        LoadPanel.SetActive(true);
        ButtonPanel.SetActive(false);
        SceneManager.LoadSceneAsync("room fire");
    }    
}
