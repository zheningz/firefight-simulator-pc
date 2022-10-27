using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LinjingSUN
{
    /***
    Deprecated version, previously used to bind to the Camera, 
    later upgraded to bind to the ejected particle system "ParticleCollision.cs",
    ***/
    public class Extinguisher : MonoBehaviour
    {
        [SerializeField] private float amountExtinguishedPerSecond = 0.1f;
        [SerializeField] private bool isFireEnd = false;
        [SerializeField] private bool fireState = false;
        public int endFireNum = 0;  // Number of ended fire
        int flag = 0;

        // Update is called once per frame
        void Update()
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, 100f);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                if(hit.collider.TryGetComponent(out Fire fire)){
                    fireState = fire.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime);
                    if(!fire.counted){
                        if(fire.extinguished){
                            endFireNum += 1;
                            fire.counted = true;
                        }
                    }
                }
                
                if(hit.collider.TryGetComponent(out Steam steam) && fireState == false){
                    print("enter steam");
                    steam.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime , flag);
                    flag += 1;
                }else{
                    flag = 0;
                }
            }
        // if(fireState == false && hit.collider.TryGetComponent(out Steam steam)){
            // if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit2, 100f)
            //     && hit2.collider.TryGetComponent(out Steam steam))
            // {
            //     print("enter steam");
            //     steam.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime , flag);
            //     flag += 1;
            // }else
            // {
            //     flag = 0;
            // }
        }
    }
}
