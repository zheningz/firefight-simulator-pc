using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    public class ShelvingController : MonoBehaviour
    {
        private bool exit = false;
        public static bool success = false;
        // Update is called once per frame
        void Update()
        {
            if(exit == false){
                var gos = GameObject.FindGameObjectsWithTag("fire");
                for(int i = 0; i < gos.Length; i++){
                    if(gos[i].GetComponentInChildren<Fire>().extinguished == false){
                        break;
                    }
                    if(i == gos.Length - 1){
                        exit = true;
                        Invoke("FinishTask", 7.0f);
                    }
                }
            }
        }

        void FinishTask(){
            success = true;
            print("success!");
        }
    }
}
