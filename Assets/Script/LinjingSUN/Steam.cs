using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    public class Steam : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float currentIntensity = 0f;
        private float startIntensity = 0f;

        [SerializeField] private ParticleSystem steamParticleSystem = null;

        private bool isLit = true;
        
        private void Start() {
            startIntensity = 0f;
            // startIntensity = steamParticleSystem.emission.rateOverTime.constant;   
        }

        float timeLastWatered = 0;
        [SerializeField] private float regionDelay = .1f;
        [SerializeField] private float regionRate = .1f;
        
        private void Update() {
            if(isLit && currentIntensity > 0f && Time.time - timeLastWatered >= regionDelay){
                currentIntensity -= regionRate * Time.deltaTime * 15;    //steam disappear, *5 to speed 
                ChangeIntensity();
            }
        }

        public bool TryExtinguish (float amount, int shootTime){

            timeLastWatered = Time.time;

            if(shootTime == 0){
                currentIntensity = 15;
            }

            if(currentIntensity <= 20){
                currentIntensity += amount * 20;
            }
            
            ChangeIntensity();
            
            // print("steam:"+ currentIntensity);

            if(currentIntensity <= 0){
                isLit = false;
                return true;
            }

            return false;
        }

        private void ChangeIntensity(){
            var emission = steamParticleSystem.emission;
            emission.rateOverTime = currentIntensity; 
        }
    }
}