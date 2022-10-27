using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    public class ParticleCollision : MonoBehaviour
    {
        [SerializeField] private float amountExtinguishedPerSecond = 0.1f;
        private List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();
        private ParticleSystem _particleSystem;
        [SerializeField] private bool isFireEnd = false;
        [SerializeField] private bool fireState = false;
        public int endFireNum = 0;  // Number of ended fire
        int flag = 0;

        void Start(){
            _particleSystem = this.GetComponent<ParticleSystem>();
        }

        private void OnParticleCollision(GameObject other) {
            Fire firehit = null;
            Steam steamhit = null;
            int hitCount = 0;
            if(other != null){
                int numCollisionEvents = _particleSystem.GetCollisionEvents(other, CollisionEvents);

                for(int i=0; i< numCollisionEvents; i++){
                    var col = CollisionEvents[i].colliderComponent.gameObject;
                    var fire = col.GetComponentInChildren<Fire>();
                    var steam = col.GetComponentInChildren<Steam>();
                    if(fire != null && steam != null){
                        hitCount++;
                        firehit = fire;
                        steamhit = steam;
                    }
                }

                if(firehit != null){
                    // print("hitCount"+hitCount + "  "+ Time.deltaTime);
                    fireState = firehit.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime);
                    if(!firehit.counted){
                        if(firehit.extinguished){
                            endFireNum += 1;
                            firehit.counted = true;
                        }
                    }      
                }

                if(steamhit != null && fireState == false){
                    // print("steam");
                    steamhit.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime , flag);
                    flag += 1;
                }else{
                    flag = 0;
                }
            }

        }
    } 
}

