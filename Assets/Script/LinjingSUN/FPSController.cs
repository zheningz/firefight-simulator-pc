using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    [RequireComponent(typeof(CharacterController))]
    public class FPSController : MonoBehaviour
    {
        public float walkingSpeed = 5f;
        public float runningSpeed = 5f;
        public float gravity = 20.0f;
        public Camera playerCamera;
        public float lookSpeed = 1.2f;
        public float lookXLimit = 45.0f;

        CharacterController characterController;
        Vector3 moveDirection = Vector3.zero;
        float rotationX = 0;

        [HideInInspector]
        public bool canMove = true;

        public static int mode = 0;    //0 normal mode; 1 selected mode
        static GameObject interactiveThing;	// current selected item 

        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            if(mode == 1)
            {
            }
            else if(mode == 0 && GameUIController.isBasic == false)
            {
                // We are grounded, so recalculate move direction based on axes
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 right = transform.TransformDirection(Vector3.right);
                // Press Left Shift to run
                bool isRunning = Input.GetKey(KeyCode.LeftShift);
                float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
                float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);

                // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
                // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
                // as an acceleration (ms^-2)
                if (!characterController.isGrounded)
                {
                    moveDirection.y -= gravity * Time.deltaTime;
                }

                // print(GameUIController.isBasic);
                // Move the controller
                characterController.Move(moveDirection * Time.deltaTime);

                // Player and Camera rotation
                if (canMove && Input.GetMouseButton(1) && AvalibleThings.enterObserve == false)
                {
                    rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                    rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                    playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                    transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
                }
            }

        }

        public static void ModeChange(int state)
        {
            mode = state;
            print("current mode is -- " + state);
        }	
        public static int getMode()
        {
            return mode;
        }	
        public static void setInteractiveThing(GameObject o)
        {
            interactiveThing = o;
        }


    }   
}