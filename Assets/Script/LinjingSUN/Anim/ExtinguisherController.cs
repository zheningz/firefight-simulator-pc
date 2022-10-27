using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    public class ExtinguisherController : MonoBehaviour
    {

        public Animator ring;

        public static bool isFall = false;
        // true: open
        // false: close

        public static string extinguisherType;
        // 1: water 
        // 2: co2 
        // 3: foam 
        // 4: fowder 
        // 5: wet

        // Start is called before the first frame update
        void Start()
        {
            ring = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var nName = hitInfo.collider.gameObject.name;
                StartRingFall(nName);
            }
        }

        void StartRingFall(string name){
            if (name == "PullRing")
            {
                if (Input.GetMouseButtonDown(1) && isFall == false)
                {
                    ring.SetTrigger("touchRing");
                    // isFall = true;
                    Invoke("ChangeIsFallValue", 2.1f);
                    // extinguisherType = this.transform.parent.gameObject.name;
                }
            }
        }
        void ChangeIsFallValue()
        {
            isFall = true;
        }
    }
  
}
