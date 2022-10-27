using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    public class RecordFlag : MonoBehaviour
    {
        private int nflag = 0;
        // Update is called once per frame
        void Update()
        {
            // setFlag()
        }

        void setFlag(int flag){
            nflag = flag;
        }

        int getFlag(){
            return nflag;
        }
    }
}
