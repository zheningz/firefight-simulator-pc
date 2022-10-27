using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace LinjingSUN
{
    public class GameUIController : MonoBehaviour
    {
        private GameObject UIPanel;
        private GameObject nowTaskPanel;
        private GameObject operateHintPanel;
        private GameObject finalTaskPanel;
        private GameObject hintPanel;
        private GameObject basicOperPanel;
        private GameObject successPanel;
        private GameObject loadPanel;   

        private GameObject basicOperBtn;
        private GameObject closeBasicOperBtn;
        private GameObject confirmFireBtn;
        private GameObject startBtn;
        private GameObject endReturnBtn;
        private GameObject endQuitBtn;
        private GameObject returnBtn;
        private GameObject quitBtn;

        private GameObject extinguisher;
        private GameObject eShoot;
        private GameObject eWater;
        private GameObject hShoot;
        private GameObject hWater;  
        private GameObject rope;
        private GameObject nozzle;
        private GameObject headValve;
        private GameObject headNozzle;

        private Text bottomHint;
        private Text curTask;
        private Text allTask;
        private Text hint;

        public bool findFire = false;

        public static int stage;
        //---extinguisher---
        // 0: not star
        // 1: start game
        // 2: find fire
        // 3: open box door
        // 4: pulled ring
        // 5: put out fire

        //---hydrant---
        // 0: not star
        // 1: start game
        // 2: find fire
        // 3: open box door
        // 4: pick hose
        // 5: connect valve        
        // 6: pick nozzle
        // 7: connect nozzle
        // 8: put out fire
        
        private bool finishStage1 = false;
        private bool finishStage2 = false;
        private bool finishStage3 = false;
        private bool finishStage4 = false;
        private bool finishStage5 = false;
        public static bool finishStage6 = false;
        private bool finishStage7 = false;
        private bool finishStage8 = false;

        private bool startWater = false;

        public static bool isChooseExtinguisher = false;

        public static bool isBasic = true; //Determine whether to enter the basic operation reminder panel

        // Start is called before the first frame update
        void Start()
        {
            UIPanel = GameObject.Find("UIPanel");
            nowTaskPanel = UIPanel.transform.Find("nowTask").gameObject;
            operateHintPanel = UIPanel.transform.Find("operateBottomHint").gameObject;
            finalTaskPanel = UIPanel.transform.Find("finalTask").gameObject;
            hintPanel = UIPanel.transform.Find("hint").gameObject;
            basicOperPanel = UIPanel.transform.Find("basicOperate").gameObject;
            successPanel = UIPanel.transform.Find("success").gameObject;
            loadPanel = UIPanel.transform.Find("loading").gameObject;

            basicOperBtn = UIPanel.transform.Find("openBasicOperate").gameObject;
            closeBasicOperBtn = basicOperPanel.transform.Find("closeBasicOper").gameObject;
            if(SceneControl.scenario == 1 || SceneControl.scenario == 2){
                confirmFireBtn = hintPanel.transform.Find("ConfirmFire").gameObject;
            }else if(SceneControl.scenario == 3){
                confirmFireBtn = UIPanel.transform.Find("ConfirmFire").gameObject;
            }
            startBtn = basicOperPanel.transform.Find("startBtn").gameObject;
            returnBtn = basicOperPanel.transform.Find("return").gameObject;
            quitBtn = basicOperPanel.transform.Find("quit").gameObject;
            endReturnBtn = successPanel.transform.Find("endReturn").gameObject;
            endQuitBtn = successPanel.transform.Find("endQuit").gameObject;

            bottomHint = operateHintPanel.transform.Find("operateBottomHintText").gameObject.GetComponent<Text>();
            hint = hintPanel.transform.Find("hintContent").gameObject.GetComponent<Text>();
            curTask = nowTaskPanel.transform.Find("curTaskContent").gameObject.GetComponent<Text>();
            allTask = finalTaskPanel.transform.Find("finalTaskSubtitle").gameObject.GetComponent<Text>();

            eShoot = GameObject.Find("Main Camera").transform.Find("Shoot-E").gameObject;
            eWater = GameObject.Find("Main Camera").transform.Find("Shoot-E").gameObject.transform.Find("WaterHose").gameObject;
            hShoot = GameObject.Find("Main Camera").transform.Find("Shoot-H").gameObject;
            hWater = GameObject.Find("Main Camera").transform.Find("Shoot-H").gameObject.transform.Find("WaterHose").gameObject;
            var obj = GameObject.Find("box-FH");
            rope = obj.transform.Find("rope").gameObject;
            nozzle = obj.transform.Find("nozzleObj").gameObject.transform.Find("nozzle").gameObject;
            headValve = obj.transform.Find("waterTurn").gameObject.transform.Find("head").gameObject;
            if(SceneControl.scenario == 3){
                headNozzle = nozzle.transform.Find("head1").gameObject;
            }
                


        }

        // Update is called once per frame
        void Update()
        {
            if(SceneControl.scenario == 1 || SceneControl.scenario == 2){
                WatchBottomOper();
                WatchFinalTask();
                WatchHint();
                WatchCurTask();
                if(DownDoorController.openDownDoor == true && finishStage3 == false){
                    stage = 3;
                    finishStage3 = true;
                }
                if(ExtinguisherController.isFall == true && finishStage4 == false && finishStage3 == true){
                    stage = 4;
                    string name = ExtinguisherController.extinguisherType;
                    print("Name: "+ name);
                    extinguisher = GameObject.Find(name);
                    if(extinguisher != null)
                        extinguisher.SetActive(false);    
                    eShoot.SetActive(true);
                    AvalibleThings.enterObserve = false;
                    finishStage4 = true;
                    isChooseExtinguisher = true;
                    FPSController.mode = 0;
                    print("stage: "+stage);
                }
                WatchWater();        
                if(ShelvingController.success == true && finishStage5 == false && finishStage4 == true){
                    stage = 5;
                    finishStage5 = true;
                    enterSuccess();
                }
            }
            else if(SceneControl.scenario == 3){
                WatchBottomOper();
                WatchFinalTask();
                WatchHint();
                WatchCurTask();
                if(UpDoorController.openUpDoor == true && finishStage3 == false){
                    stage = 3;
                    finishStage3 = true;
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    var nName = hitInfo.collider.gameObject.name;
                    if(nName == "roperope"){
                        if (Input.GetMouseButtonDown(1) && rope.activeSelf == true && finishStage4 == false && finishStage3 == true)
                        {
                            stage = 4;
                            finishStage4 = true;
                            if(rope != null)
                                rope.SetActive(false);
                        }                        
                    }
                    if(nName == "waterTurn"){
                        if (Input.GetMouseButtonDown(1) && rope.activeSelf == false && finishStage5 == false && finishStage4 == true)
                        {
                            headValve.SetActive(true);
                            stage = 5;
                            finishStage5 = true;
                            print("stage: "+ stage);
                        } 
                    }
                }
                if(ExtinguisherController.extinguisherType == "nozzle" && finishStage6 == false && finishStage5 == true)
                {
                    stage = 6;
                    finishStage6 = true;
                    print("stage: "+ stage);
                }
                if (Physics.Raycast(ray, out hitInfo))
                {
                    var nName = hitInfo.collider.gameObject.name;
                    if(nName == "nozzle"){
                        if (Input.GetMouseButtonDown(1) && nozzle.activeSelf == true && finishStage7 == false)
                        {
                            stage = 7;
                            finishStage7 = true;
                            if(headNozzle != null){
                                headNozzle.SetActive(true);
                            }
                            Invoke("ShowNozzleShoot", 0.7f);
                        }                        
                    }
                } 
                WatchWater();        
                if(ShelvingController.success == true && finishStage8 == false && finishStage7 == true){
                    stage = 8;
                    finishStage8 = true;
                    enterSuccess();
                }  
            }
        }

        private void ShowNozzleShoot(){
            if(nozzle != null)
                nozzle.SetActive(false);
            hShoot.SetActive(true);
            AvalibleThings.enterObserve = false;
            FPSController.mode = 0;
            print("stage: "+ stage);            
        }

        public void OnOpenBasicOper(){
            isBasic = true;
            nowTaskPanel.SetActive(false);
            operateHintPanel.SetActive(false);
            finalTaskPanel.SetActive(false);
            hintPanel.SetActive(false);
            basicOperPanel.SetActive(true);  
            basicOperBtn.SetActive(false); 
            confirmFireBtn.SetActive(false); 
        }

        public void CloseBasicOper(){
            isBasic = false;
            nowTaskPanel.SetActive(true);
            operateHintPanel.SetActive(true);
            finalTaskPanel.SetActive(true);
            hintPanel.SetActive(true);
            basicOperPanel.SetActive(false); 
            basicOperBtn.SetActive(true);
            if(stage < 2){
                confirmFireBtn.SetActive(true);
            }
            print("stage: "+stage);
                
        }

        public void OnStart(){
            isBasic = false;
            nowTaskPanel.SetActive(true);
            operateHintPanel.SetActive(true);
            finalTaskPanel.SetActive(true);
            if(SceneControl.scenario == 1 || SceneControl.scenario == 2){
                hintPanel.SetActive(true);
            }
            basicOperBtn.SetActive(true);
            if(confirmFireBtn != null)
                confirmFireBtn.SetActive(true);
            closeBasicOperBtn.SetActive(true);  
            startBtn.SetActive(false); 
            returnBtn.SetActive(true); 
            quitBtn.SetActive(true); 
            basicOperPanel.SetActive(false); 

            stage = 1;
            finishStage1 = true; 
            
            print("stage: "+ stage);
        }

        public void OnConfirm(){
            confirmFireBtn.SetActive(false);
            findFire = true;
            stage = 2;
            finishStage2 = true;
            print("stage: "+ stage);
        }

        public void OnPointerEnter(){
            hintPanel.SetActive(true);
        }

        public void OutPointerEnter(){
            hintPanel.SetActive(false);
        }

        private void enterSuccess(){
            nowTaskPanel.SetActive(false);
            operateHintPanel.SetActive(false);
            finalTaskPanel.SetActive(false);
            hintPanel.SetActive(false);
            basicOperPanel.SetActive(false);  
            basicOperBtn.SetActive(false); 
            confirmFireBtn.SetActive(false);
            eShoot.SetActive(false);
            hShoot.SetActive(false);
            // successPanel.SetActive(true);  
            Recover();    
            SceneManager.LoadSceneAsync("End Page");  
        }
        
        private void WatchFinalTask(){
            if(SceneControl.scenario == 1){
                allTask.text = "PLEASE PUT OUT ONE COMPUTER FIRE";
            }else if(SceneControl.scenario == 2){
                allTask.text = "PLEASE PUT OUT THE FIRE ON THE SHELF";
            }else if(SceneControl.scenario == 3){
                allTask.text = "PLEASE PUT OUT THE FIRE IN THE ROOM";
            }else if(SceneControl.scenario == 4){
                allTask.text = "PLEASE PUT OUT THREE FIRES";
            }
        }

        private void WatchBottomOper(){
            if(SceneControl.scenario == 1){
                if(FPSController.getMode() == 1 && stage == 3){
                    bottomHint.text = "[ HOLD \"R\" AND MOVE THE MOUSE TO ROTATE ]\n[ LEFT CLICK TO PUT THE OBJECT BACK IN PLACE ]\n[ RIGHT CLICK TO INTERACTIVELY OPERATE ]";
                }else if(FPSController.getMode() == 0 && stage == 3){
                    bottomHint.text = "[ CLICK THE FIRE EXTINGUISHER WITH THE LEFT MOUSE BUTTON TO ENLARGE ]";
                }else if(stage == 2){
                    bottomHint.text = "[ CLICK THE LEFT MOUSE BUTTON TO INTERACT WITH THE ENVIRONMENT ]";
                }else{
                    bottomHint.text = null;
                }
                if(stage == 4){
                    bottomHint.text = "[ PRESS \"SPACE\" TO SPRAY, PRESS AGAIN TO STOP ]";
                }
            }else if(SceneControl.scenario == 2){
                if(FPSController.getMode() == 1 && stage == 3){
                    bottomHint.text = "[ HOLD \"R\" AND MOVE THE MOUSE TO ROTATE ]\n[ LEFT CLICK TO PUT THE OBJECT BACK IN PLACE ]\n[ RIGHT CLICK TO INTERACTIVELY OPERATE ]";
                }else if(FPSController.getMode() == 0 && stage == 3){
                    bottomHint.text = "[ CLICK THE FIRE EXTINGUISHER WITH THE LEFT MOUSE BUTTON TO ENLARGE ]";
                }else if(stage == 2){
                    bottomHint.text = "[ CLICK THE LEFT MOUSE BUTTON TO INTERACT WITH THE ENVIRONMENT ]";
                }else{
                    bottomHint.text = null;
                }
                if(stage == 4){
                    bottomHint.text = "[ HOLD \"SPACE\" TO SPRAY ]";
                }
            }else if(SceneControl.scenario == 3){
                if(FPSController.getMode() == 1 && stage == 6){
                    bottomHint.text = "[ CLICK THE RIGHT MOUSE BUTTON ON THE WATER NOZZLE TO SIMULATE THE CONNECTION OPERATION ]\n[ CLICK THE LEFT MOUSE BUTTON TO PUT THE OBJECT BACK IN PLACE ]";
                }else if(FPSController.getMode() == 0 && stage == 5){
                    bottomHint.text = "[ CLICK THE FIRE NOZZLE WITH THE LEFT MOUSE BUTTON TO ENLARGE ]";
                }else if(stage == 3){
                    bottomHint.text = "[ CLICK THE LEFT MOUSE BUTTON TO INTERACT WITH THE ENVIRONMENT ]\n[ CLICK THE RIGHT MOUSE BUTTON TO SIMULATE THE GET OPERATION ]";
                }else if(stage == 2){
                    bottomHint.text = "[ CLICK THE LEFT MOUSE BUTTON TO INTERACT WITH THE ENVIRONMENT ]";
                }else if(stage == 4){
                    bottomHint.text = "[ CLICK THE RIGHT MOUSE BUTTON ON THE WATER VALVE TO SIMULATE THE CONNECTION OPERATION ]";
                }else{
                    bottomHint.text = null;
                }
                if(stage == 7 ){
                    bottomHint.text = "[ PRESS \"SPACE\" TO SPRAY, PRESS AGAIN TO STOP ]\n[ CLICK THE LEFT MOUSE BUTTON TO INTERACT WITH THE ENVIRONMENT ]";
                }
            }
        }

        private void WatchWater(){
            if(SceneControl.scenario == 1){
                if(Input.GetKeyDown(KeyCode.Space) && stage == 4){
                    if(startWater == false){
                        eWater.SetActive(true);
                        startWater = true;
                    }else{
                        eWater.SetActive(false);
                        startWater = false;                
                    }
                }
            }else if(SceneControl.scenario == 3){
                if(Input.GetKeyDown(KeyCode.Space) && stage == 7){
                    if(startWater == false){
                        hWater.SetActive(true);
                        startWater = true;
                    }else{
                        hWater.SetActive(false);
                        startWater = false;                
                    }
                }                
            }else if(SceneControl.scenario == 2){
                if(Input.GetKeyDown(KeyCode.Space) && stage == 4){
                    eWater.SetActive(true);
                }else if(Input.GetKeyUp(KeyCode.Space) && stage == 4){
                    eWater.SetActive(false);                       
                }              
            }

        }

        private void WatchHint(){
            if(SceneControl.scenario == 1 || SceneControl.scenario == 2){
                if(stage == 1){
                    hint.text = "PLEASE LOOK AROUND FOR THE IGNITION AND PRESS THE \"CONFIRM\" BUTTON AT THE BOTTOM WHEN YOU FIND IT";
                }else if(stage == 2){
                    hint.text = "PLEASE FIND THE BOX WITH THE FIRE EXTINGUISHER";
                }else if(stage == 3){
                    hint.text = "NEED TO CHECK THE PRESSURE GAUGE FIRST, MAKE SURE IT CAN BE USED BEFORE UNPLUGGING THE RING \n   [RED: INSUFFICIENT PRESSURE]\n   [GREEN: NORMAL]\n   [YELLOW: EXCESSIVE PRESSURE]";
                }else if(stage == 4){
                    hint.text = "FIGHT THE FIRE NEAR THE POINT OF IGNITION";
                }else{
                    hint.text = null;
                }                  
            }else if(SceneControl.scenario == 3){
                if(stage == 1){
                    hint.text = "LOOK AROUND FOR THE IGNITION AND PRESS THE \"CONFIRM\" BUTTON ON THE CENTER OF SCREEN WHEN YOU FIND IT";
                }else if(stage == 2){
                    hint.text = "OPEN FIRE BOX DOOR";
                }else if(stage == 3){
                    hint.text = "THE FIRE HOSE SHOULD BE LAID IN THE DIRECTION OF THE FIRE FIELD TO AVOID TWISTING";
                }else if(stage == 4){
                    hint.text = "TIGHTEN IT CLOCKWISE";
                }else if(stage == 5){
                    hint.text = "THIS IS SOLID STREAM NOZZLE";
                }else if(stage == 6){
                    hint.text = "TIGHTEN IT CLOCKWISE";
                }else if(stage == 7){
                    hint.text = "FIGHT THE FIRE NEAR THE POINT OF IGNITION";
                }else{
                    hint.text = null;
                }
            }
        }

        private void WatchCurTask(){
            if(SceneControl.scenario == 1 || SceneControl.scenario == 2){
                if(stage == 1){
                    curTask.text = "FIND FIRE";
                }else if(stage == 2){
                    curTask.text = "GET EXTINGUISHER";
                }else if(stage == 3){
                    curTask.text = "PULL RING";
                }else if(stage == 4){
                    curTask.text = "PUT OUT FIRE";
                }else{
                    curTask.text = null;
                }           
            }else if(SceneControl.scenario == 3){
                if(stage == 1){
                    curTask.text = "FIND FIRE";
                }else if(stage == 2){
                    curTask.text = "FIND AN INDOOR FIRE HYDRANT";
                }else if(stage == 3){
                    curTask.text = "PICK FIRE HOSE";
                }else if(stage == 4){
                    curTask.text = "CONNECT HOSE END TO VALVE PORT";
                }else if(stage == 5){
                    curTask.text = "PICK FIRE NOZZLE";
                }else if(stage == 6){
                    curTask.text = "CONNECT THE OTHER END OF THE HOSE TO FIRE NOZZLE";
                }else if(stage == 7){
                    curTask.text = "PUT OUT FIRE";
                }else{
                    curTask.text = null;
                }
            }
        }

        public void ExitGame()
        {
            Recover();
            nowTaskPanel.SetActive(false);
            operateHintPanel.SetActive(false);
            finalTaskPanel.SetActive(false);
            hintPanel.SetActive(false);
            basicOperPanel.SetActive(false);  
            basicOperBtn.SetActive(false); 
            confirmFireBtn.SetActive(false);
            eShoot.SetActive(false);
            hShoot.SetActive(false);
            successPanel.SetActive(false);
            loadPanel.SetActive(true); 
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }    

        public void ReturnMenu(){
            Recover();
            nowTaskPanel.SetActive(false);
            operateHintPanel.SetActive(false);
            finalTaskPanel.SetActive(false);
            hintPanel.SetActive(false);
            basicOperPanel.SetActive(false);  
            basicOperBtn.SetActive(false); 
            confirmFireBtn.SetActive(false);
            eShoot.SetActive(false);
            hShoot.SetActive(false);
            successPanel.SetActive(false);
            loadPanel.SetActive(true); 
            SceneManager.LoadSceneAsync("Start Page");
        }

        public void Recover(){
            stage = 0;
            DownDoorController.openDownDoor = false;
            UpDoorController.openUpDoor = false;
            ExtinguisherController.isFall = false;
            AvalibleThings.enterObserve = false;
            FPSController.mode = 0;
            ShelvingController.success = false;
            isChooseExtinguisher = false;
            isBasic = false;
        }
    }  
}

