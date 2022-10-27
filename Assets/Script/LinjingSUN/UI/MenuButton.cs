using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LinjingSUN
{
    public class MenuButton : MonoBehaviour
    {
        private GameObject IntroPanel;
        private GameObject ModePanel;
        private GameObject ScenarioPanel;
        private GameObject QuitPanel;
        private GameObject box;
        private GameObject BufferPanel;

        private GameObject title;
        private GameObject QuitBtn;

        public static int flag;
        //  1: intro
        //  2: mode
        //  3: train
        //  4: test
        public static bool marker = false;

        public GameObject scenarioMode;
        public static int scenario;
        //  1: computer;
        //  2: shelf;
        //  3: room;
        //  4: out;

        // Start is called before the first frame update
        void Start()
        {
            box = GameObject.Find("Box");
            IntroPanel = box.transform.Find("Intro").gameObject;
            ModePanel = box.transform.Find("Mode").gameObject;
            ScenarioPanel = box.transform.Find("Scenarios").gameObject;
            QuitPanel = box.transform.Find("Quit").gameObject;

            QuitBtn = box.transform.Find("QuitBtn").gameObject;

            BufferPanel = box.transform.Find("Buffer").gameObject;

            title = box.transform.Find("Title").gameObject;
            if(marker == false){
                flag = 1;
                marker = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void OnContinue(){
            IntroPanel.SetActive(false);
            ModePanel.SetActive(true);
            flag = 2;
        }

        public void OnTrain(){
            ModePanel.SetActive(false);
            ScenarioPanel.SetActive(true);
            title.SetActive(false);
            flag = 3;
        }

        public void OnTest(){

        }

        public void Return(){
            ScenarioPanel.SetActive(false);
            ModePanel.SetActive(true);
            title.SetActive(true);
            flag = 2;
        }

        public void OnQuit(){
            QuitBtn.SetActive(false);
            QuitPanel.SetActive(true);
            ScenarioPanel.SetActive(false);
            ModePanel.SetActive(false);
            IntroPanel.SetActive(false);
            title.SetActive(false);
        }

        public void SureQuit(){
            ExitGame();
        }

        public void CancelQuit(){
            QuitPanel.SetActive(false);
            QuitBtn.SetActive(true);
            title.SetActive(true);
            if(flag == 2){
                OnContinue(); 
            }else if(flag == 3){
                OnTrain();
            }else{
                IntroPanel.SetActive(true);
                ModePanel.SetActive(false);
                ScenarioPanel.SetActive(false); 
            }
        }

        public void JumpScene(){
            BufferPanel.SetActive(true);
            ScenarioPanel.SetActive(false);
            QuitBtn.SetActive(false);
            if(scenarioMode.name == "Computer"){
                scenario = 1;
                SceneManager.LoadSceneAsync(1);
            }else if(scenarioMode.name == "Shelf"){
                scenario = 2;
                SceneManager.LoadSceneAsync(2);
            }else if(scenarioMode.name == "Room"){
                scenario = 3;
                SceneManager.LoadSceneAsync(3);
            }else if(scenarioMode.name == "OutST"){
                scenario = 4;
                SceneManager.LoadSceneAsync(4);
            }
        }

        public void ExitGame()
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
            }
    }
}
