using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    public class DownDoorController : MonoBehaviour
    {

        public Animator door;

        public static bool openDownDoor = false;
        // true: open
        // false: close

        // Start is called before the first frame update
        void Start()
        {
            door = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var nName = hitInfo.collider.gameObject.name;
                DownBoxDoorController(nName);
            }
        }

        void DownBoxDoorController(string name){
            if (name == "DownDoor")
            {
                if (Input.GetMouseButtonDown(0) && !openDownDoor && GameUIController.isBasic == false)
                {
                    door.SetTrigger("open_down_door");
                    openDownDoor = true;
                }
                else if(Input.GetMouseButtonDown(0) && openDownDoor){
                    door.SetTrigger("close_down_door");
                    openDownDoor = false;
                }
            }
        }
    }
    
}
