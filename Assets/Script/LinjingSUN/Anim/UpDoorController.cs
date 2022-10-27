using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    public class UpDoorController : MonoBehaviour
    {

        public Animator door;

        public static bool openUpDoor = false;
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
                UpBoxDoorController(nName);
            }
        }

        void UpBoxDoorController(string name){
            if (name == "UpDoor")
            {
                if (Input.GetMouseButtonDown(0) && !openUpDoor && GameUIController.isBasic == false)
                {
                    door.SetTrigger("open_up_door");
                    openUpDoor = true;
                }
                else if(Input.GetMouseButtonDown(0) && openUpDoor){
                    door.SetTrigger("close_up_door");
                    openUpDoor = false;
                }
            }
        }

    }
}

