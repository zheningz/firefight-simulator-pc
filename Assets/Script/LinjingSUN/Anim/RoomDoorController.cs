using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    public class RoomDoorController : MonoBehaviour
    {

        public Animator door;

        public bool openRoomDoor = false;
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
                OnRoomDoorController(nName);
            }
        }

        void OnRoomDoorController(string name){
            if (name == "RoomDoor")
            {
                if (Input.GetMouseButtonDown(0) && !openRoomDoor)
                {
                    door.SetTrigger("open_room_door");
                    openRoomDoor = true;
                }
                else if(Input.GetMouseButtonDown(0) && openRoomDoor){
                    door.SetTrigger("close_room_door");
                    openRoomDoor = false;
                }
            }
        }

    }
}

