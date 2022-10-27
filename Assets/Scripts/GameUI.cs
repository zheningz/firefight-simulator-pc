using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    private GameObject IntroPanel; 
    private GameObject Task1Panel;
    private GameObject Task2Panel;
    private GameObject Task3Panel;
    private GameObject Task4Panel;
    private GameObject Task5Panel;
    // private GameObject Task6Panel;
    private GameObject Task7Panel;
    private GameObject Task8Panel;
    private GameObject Task9Panel;
    private GameObject PausePanel; 
    private GameObject canvas;

    private GameObject PauseBtn;
    private GameObject ContinueBtn;
    private GameObject ReturnBtn;
    private GameObject QuitBtn;

    public static int task = 0;
    public static bool isPause = false;
    //可以直接在别的文件用GameUI.task去标明现在的task

    // Start is called before the first frame update
    void Start()
    {
        task = 0;
        canvas = GameObject.Find("Canvas");
        Task1Panel = canvas.transform.Find("Task_1").gameObject;
        Task2Panel = canvas.transform.Find("Task_2").gameObject;
        Task3Panel = canvas.transform.Find("Task_3").gameObject;
        Task4Panel = canvas.transform.Find("Task_4").gameObject;
        Task5Panel = canvas.transform.Find("Task_5").gameObject;
        // Task6Panel = canvas.transform.Find("Task_6").gameObject;
        Task7Panel = canvas.transform.Find("Task_7").gameObject;
        Task8Panel = canvas.transform.Find("Task_8").gameObject;
        Task9Panel = canvas.transform.Find("Task_9").gameObject;
        IntroPanel = canvas.transform.Find("Intro").gameObject;
        PausePanel = canvas.transform.Find("PausePanel").gameObject;

        PauseBtn = canvas.transform.Find("Pause").gameObject;
        ContinueBtn = canvas.transform.Find("Continue").gameObject;
        ReturnBtn = PausePanel.transform.Find("Retry").gameObject;
        QuitBtn = PausePanel.transform.Find("Quit").gameObject;

    }

    public void OnPause(){
        if(Task1Panel.activeSelf == true){
            task = 1;
        }else if(Task2Panel.activeSelf == true){
            task = 2;
        }else if(Task3Panel.activeSelf == true){
            task = 3;
        }else if(Task4Panel.activeSelf == true){
            task = 4;
        }else if(Task5Panel.activeSelf == true){
            task = 5;
        // }else if(task == 6){
        //     Task6Panel.SetActive(true);
        }else if(Task7Panel.activeSelf == true){
            task = 7;
        }else if(Task8Panel.activeSelf == true){
            task = 8;
        }
        isPause = true;
        Task1Panel.SetActive(false);
        Task2Panel.SetActive(false);
        Task3Panel.SetActive(false);
        Task4Panel.SetActive(false);
        Task5Panel.SetActive(false);
        // Task6Panel.SetActive(false);
        Task7Panel.SetActive(false);
        Task8Panel.SetActive(false);

        PauseBtn.SetActive(false);
        ContinueBtn.SetActive(true);
        PausePanel.SetActive(true);
    }

    public void OnContinue(){
        isPause = false;
        PauseBtn.SetActive(true);
        ContinueBtn.SetActive(false);
        PausePanel.SetActive(false);

        //去设置不同的页面
        if(task == 0){
            IntroPanel.SetActive(true);
        }else if(task == 1){
            Task1Panel.SetActive(true);
        }else if(task == 2){
            Task2Panel.SetActive(true);
        }else if(task == 3){
            Task3Panel.SetActive(true);
        }else if(task == 4){
            Task4Panel.SetActive(true);
        }else if(task == 5){
            Task5Panel.SetActive(true);
        // }else if(task == 6){
        //     Task6Panel.SetActive(true);
        }else if(task == 7){
            Task7Panel.SetActive(true);
        }else if(task == 8){
            Task8Panel.SetActive(true);
        }
    }

    public void OnReturn(){
        task = 0;
        isPause = false;
        SceneManager.LoadSceneAsync("Start Page");
    }

    public void OnQuit(){
        task = 0;
        isPause = false;
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void Update()
    {
        if (FireControl.FireNum <= 0)
        {
            Task9Panel.SetActive(true);
        }
    }
}  


