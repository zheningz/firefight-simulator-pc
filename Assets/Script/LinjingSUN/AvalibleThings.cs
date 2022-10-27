using UnityEngine;

namespace LinjingSUN
{
    public class AvalibleThings : MonoBehaviour {
        Transform originTransform;
        Transform newTransform;

        public static bool enterObserve = false;

        // Use this for initialization
        void Start () {
            originTransform = new GameObject().transform;
        }

        // Update is called once per frame
        void Update () {
            if (Input.GetKey(KeyCode.R) && (SceneControl.scenario == 1 || SceneControl.scenario == 2))
            {
                // Only rotate x
                float x = Input.GetAxis("Mouse X");
                Vector3 rot = new Vector3(0, -x, 0);
                float spd = 10; //speed
                if(newTransform != null){
                    newTransform.Rotate(spd * rot, Space.World);
                }
                
            }
        }

        

        void OnMouseDown()
        {
            if(this.gameObject.name == "nozzle"){
                if(SceneControl.scenario == 3){
                    //这个地方可能有bug 后面改, 可能开了上面的门之后也能点击下面的灭火器
                    print(GameUIController.isChooseExtinguisher);
                    if(UpDoorController.openUpDoor == true && GameUIController.isChooseExtinguisher == false){
                        GameObject[] allObjs = GameObject.FindGameObjectsWithTag("extinguisher");
                        print("m" + FPSController.getMode());
                        if(FPSController.getMode()==0)  // cuz it is 0, so will enter watch mode
                        {
                            enterObserve = true;
                            for(int i = 0; i < allObjs.Length; i++){
                                if(allObjs[i].name != this.gameObject.name){
                                    allObjs[i].GetComponent<Collider>().enabled = false;
                                    allObjs[i].GetComponent<AvalibleThings>().enabled = false;
                                }
                            }
                            CopyTransform(transform, originTransform);	//record original location
                            Vector3 pos = Camera.main.transform.position;
                            print("this "+ this.gameObject.name);
                            // if ((SceneControl.scenario == 1 || SceneControl.scenario == 2))
                            // {
                                ExtinguisherController.extinguisherType = this.gameObject.name;
                            // }
                            // if(this.gameObject.name == "nozzle" && GameUIController.stage == 5 && SceneControl.scenario == 3)
                            // {
                            //     ExtinguisherController.extinguisherType = this.gameObject.name;
                            // }

                            print(this.gameObject.name);
                            print(GameUIController.stage);
                            print(SceneControl.scenario);   
                            if(SceneControl.scenario == 3 && this.gameObject.name == "nozzle" && (GameUIController.stage == 5 ||  GameUIController.stage == 6))
                            {
                                pos.y -= 1.8f;
                                transform.position = Camera.main.transform.forward * 0.7f + pos; //set it in front of camera

                                newTransform = transform;
                                FPSController.ModeChange(1);

                                // FPSController.setInteractiveThing(gameObject);
                            }
                            else if((SceneControl.scenario == 1 || SceneControl.scenario == 2) && this.gameObject.name != "nozzle")
                            {
                                pos.y -= .1f;
                                transform.position = Camera.main.transform.forward * 1.1f + pos; //set it in front of camera

                                newTransform = transform;
                                FPSController.ModeChange(1);

                                // FPSController.setInteractiveThing(gameObject);
                            }
                        }
                        else if(FPSController.getMode() == 1)
                        {
                            if(this.gameObject.name == "nozzle" && GameUIController.stage == 6){
                                GameUIController.stage = 5;
                                GameUIController.finishStage6 = false;
                            }
                            enterObserve = false;
                            for(int i = 0; i < allObjs.Length; i++){
                                if(allObjs[i].name != this.gameObject.name){
                                    allObjs[i].GetComponent<Collider>().enabled = true;
                                    allObjs[i].GetComponent<AvalibleThings>().enabled = true;
                                }
                            }         
                            CopyTransform(originTransform, transform);	// return back
                            FPSController.ModeChange(0);
                            // FPSController.setInteractiveThing(null);
                        }
                    }
                }
            }else{
                if(SceneControl.scenario == 1 || SceneControl.scenario == 2){
                    //这个地方可能有bug 后面改, 可能开了上面的门之后也能点击下面的灭火器
                    print(GameUIController.isChooseExtinguisher);
                    if((DownDoorController.openDownDoor == true && GameUIController.isChooseExtinguisher == false)){
                        GameObject[] allObjs = GameObject.FindGameObjectsWithTag("extinguisher");
                        print("m" + FPSController.getMode());
                        if(FPSController.getMode()==0)  // cuz it is 0, so will enter watch mode
                        {
                            enterObserve = true;
                            for(int i = 0; i < allObjs.Length; i++){
                                if(allObjs[i].name != this.gameObject.name){
                                    allObjs[i].GetComponent<Collider>().enabled = false;
                                    allObjs[i].GetComponent<AvalibleThings>().enabled = false;
                                }
                            }
                            CopyTransform(transform, originTransform);	//record original location
                            Vector3 pos = Camera.main.transform.position;
                            print("this "+ this.gameObject.name);
                            // if ((SceneControl.scenario == 1 || SceneControl.scenario == 2))
                            // {
                                ExtinguisherController.extinguisherType = this.gameObject.name;
                            // }
                            // if(this.gameObject.name == "nozzle" && GameUIController.stage == 5 && SceneControl.scenario == 3)
                            // {
                            //     ExtinguisherController.extinguisherType = this.gameObject.name;
                            // }

                            print(this.gameObject.name);
                            print(GameUIController.stage);
                            print(SceneControl.scenario);   
                            if(SceneControl.scenario == 3 && this.gameObject.name == "nozzle" && (GameUIController.stage == 5 ||  GameUIController.stage == 6))
                            {
                                pos.y -= 1.8f;
                                transform.position = Camera.main.transform.forward * 0.7f + pos; //set it in front of camera

                                newTransform = transform;
                                FPSController.ModeChange(1);

                                // FPSController.setInteractiveThing(gameObject);
                            }
                            else if((SceneControl.scenario == 1 || SceneControl.scenario == 2) && this.gameObject.name != "nozzle")
                            {
                                pos.y -= .1f;
                                transform.position = Camera.main.transform.forward * 1.1f + pos; //set it in front of camera

                                newTransform = transform;
                                FPSController.ModeChange(1);

                                // FPSController.setInteractiveThing(gameObject);
                            }
                        }
                        else if(FPSController.getMode() == 1)
                        {
                            if(this.gameObject.name == "nozzle" && GameUIController.stage == 6){
                                GameUIController.stage = 5;
                                GameUIController.finishStage6 = false;
                            }
                            enterObserve = false;
                            for(int i = 0; i < allObjs.Length; i++){
                                if(allObjs[i].name != this.gameObject.name){
                                    allObjs[i].GetComponent<Collider>().enabled = true;
                                    allObjs[i].GetComponent<AvalibleThings>().enabled = true;
                                }
                            }         
                            CopyTransform(originTransform, transform);	// return back
                            FPSController.ModeChange(0);
                            // FPSController.setInteractiveThing(null);
                        }
                    }
                }
            }            

        }

        void CopyTransform(Transform source, Transform destination)
        {
            destination.position = source.position;
            destination.rotation = source.rotation;
            destination.localScale = source.localScale;
        }

    }
}