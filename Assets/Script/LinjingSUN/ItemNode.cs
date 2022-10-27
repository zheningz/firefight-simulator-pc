using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    public class ItemNode : MonoBehaviour
    {

        public bool canFire = false; // If the node item is fire or not, Used to set ignition source
        public bool isFire = false; // to mark is fire of this item is existing or not
        private bool finishedFire = false; // Its own fuel has been burned out, or extinguished
        [SerializeField, Range(0f, 1f)] private float HP = 1.0f; // The life of current node item

        public float temperature = .0f; // Temperature of current item, default is 0
        public float kindlingPoint = 80.0f; // Kindling point of current item, which can be set by user
        private float maxTemper = 100.0f; // To make sure that the temperature will not alwayse increase

        public GameObject connectNode; // Connect the different shelves

        public Fire inFire = null;     // Fire state in current node
        bool flag = false;

        bool enter = false;
        Vector3 ve = new Vector3(0,0,0);


        // Start is called before the first frame update
        void Start()
        {
            // Ignite the fire
            if(canFire && !isFire){
                CreateFire();
                isFire = true;
            }

            maxTemper = kindlingPoint + 20.0f; // Follow the user-set "kindlingPoint" to modify the value
        }

        // Update is called once per frame
        void Update()
        {
                if(inFire != null && inFire.extinguished == true){
                    finishedFire = true;
                }
                // Prevents the user from adjusting the "kindlingPoint" during operation
                maxTemper = kindlingPoint + 20.0f; 

                if(isFire && !flag){
                    this.temperature = kindlingPoint;
                    Invoke("ChangeColor", 4.0f);
                    flag = true;
                }

                // Check whether the user has arrived at the specified area, and then trigger the fire diffusion after arriving
                if(enter == false){
                    // position: x: 87.4635 y: 0.6864758 z: 49.72013
                    Vector3 pos = new Vector3(87f, 0.7f, 50.2f);
                    // print(transform.position.z);
                    ve = pos;
                    Collider[] testBar = Physics.OverlapSphere(pos, 2f, 1 << LayerMask.NameToLayer("User"));
                    if(testBar.Length > 1){
                        enter = true;
                        print("enter!");
                    }
                }

                // Determine if the object has reached the point of ignition and is in a state where it has not begun to burn
                if(canFire && !isFire && !finishedFire){
                    CreateFire();
                    isFire = true;
                }
                else if(canFire && isFire && !finishedFire && ((GameUIController.stage == 4 && SceneControl.scenario == 2) || (SceneControl.scenario == 3))){
                    float radius = 0f;
                    if(SceneControl.scenario == 2){
                        radius = 0.55f;
                    }else if(SceneControl.scenario == 3){
                        radius = 1.9f;
                    }
                    
                    Collider[] nearbyNodesCol = Physics.OverlapSphere(this.transform.position, radius, 1 << LayerMask.NameToLayer("Node"));
                    List<GameObject> nearbyNodes = new List<GameObject>();  // Nearby nodes
                    // print("num" + nearbyNodesCol.Length);
                    if(nearbyNodesCol.Length > 0)
                    {
                        for (int i = 0; i < nearbyNodesCol.Length; i++)
                        {
                            nearbyNodes.Add(nearbyNodesCol[i].gameObject);
                        }

                    }
                    // Add the diffusion point used to connect the two shelves to the heating list
                    if(connectNode){
                        nearbyNodes.Add(connectNode);
                    }
                    // Determine the heating rate according to the distance
                    for(int i = 0; i < nearbyNodes.Count; i++){
                        float distance = Vector3.Distance(nearbyNodes[i].transform.position, this.transform.position);
                        ItemNode node = nearbyNodes[i].GetComponent<ItemNode>();
                        // If the node is detected to have started burning, skip it
                        if(node != null){
                            if(node.isFire){
                                continue;
                            }
                        }
                        // If the node is not fire
                        if(node.temperature < maxTemper){
                            if(distance != 0.0f){
                                if(SceneControl.scenario == 2){
                                    node.temperature += 0.05f / distance;
                                }else if(SceneControl.scenario == 3){
                                    node.temperature += 0.07f / distance;
                                }
                            }
                            // If the temperature reaches the ignition point, it starts to burn
                            if(node.temperature >= kindlingPoint){
                                node.canFire = true;
                            }
                        }else{
                            node.temperature = maxTemper;
                        }

                    }
                }
        }
        

        void CreateFire(){
            if(SceneControl.scenario == 2){
                GameObject fire = Resources.Load("LinjingSUN/Prefab/BookFireSet") as GameObject;
                if(fire == null){
                    print("resources is null");
                }
                Vector3 pos = transform.position;
                pos.y += .16f;
                pos.z += .3f;
                pos.x += .13f;
                GameObject newFire = Instantiate(fire, pos, Quaternion.identity) as GameObject;        
                var tempFire = newFire.GetComponentInChildren<Fire>();
                if(tempFire != null){
                    inFire = tempFire;
                }                
            }else if(SceneControl.scenario == 3){
                GameObject fire = Resources.Load("LinjingSUN/Prefab/RoomFireSet") as GameObject;
                if(fire == null){
                    print("resources is null");
                }
                Vector3 pos = transform.position;
                pos.y += .5f;
                pos.z += .3f;
                pos.x += .13f;
                GameObject newFire = Instantiate(fire, pos, Quaternion.identity) as GameObject;        
                var tempFire = newFire.GetComponentInChildren<Fire>();
                if(tempFire != null){
                    inFire = tempFire;
                } 
            }
        }   

        void ChangeColor(){
            if(SceneControl.scenario == 2)
                this.gameObject.GetComponent<Renderer>().material.color = Color.black;
            else if(SceneControl.scenario == 3){
                this.gameObject.GetComponent<Renderer>().material.color = Color.black;
                Transform[] children = this.GetComponentsInChildren<Transform>();

                foreach (Transform item in children)
                {
                    item.GetComponent<Renderer>().material.color = Color.black;
                }
            }
        } 

        // private void OnDrawGizmos()
        // {
        //     if(ve != null){
        //         Gizmos.color = Color.blue;
        //         Gizmos.DrawSphere(ve, 2f);           
        //     }
        // }

    } 
}

