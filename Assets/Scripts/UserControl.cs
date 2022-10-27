using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour
{
    [SerializeField] ExtinguishControl Nozzle;
    [SerializeField] AudioSource Run;
    [SerializeField] AudioSource Jog;
    public float speed = 1.0f;
    public float gravity = 20.0f;
    public CharacterController UserController;

    void FixedUpdate()
    {
        if(GameUI.isPause == false)
        {
            float x, y = 0, z;
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            Vector3 movement;

            if (!UserController.isGrounded)
            {
                y -= gravity * Time.deltaTime;
            }
            
            movement = transform.right * x + transform.up * y + transform.forward * z;
            if (movement != Vector3.zero) 
            {
                if (Nozzle.isOn == false)
                {
                    Jog.Stop();
                    if(!Run.isPlaying)
                    {
                        Run.Play();
                    }
                    UserController.Move(movement * speed * Time.deltaTime);
                }
                else
                {
                    Run.Stop();
                    if(!Jog.isPlaying)
                    {
                        Jog.Play();
                    }
                    UserController.Move(movement * speed * Time.deltaTime * 0.4f);
                }
            }
            else 
            {
                Run.Stop();
                Jog.Stop();
            }
        }        
    }
}