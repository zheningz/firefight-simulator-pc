using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinjingSUN
{
    public class Fire : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float currentIntensity = 1.0f;
        private float [] startIntensities = new float[0];

        [SerializeField] private ParticleSystem [] fireParticleSystems = new ParticleSystem[0];

        private bool isLit = true;
        public bool extinguished = false;
        public static bool isExtinguished = false;
        public bool counted = false; // To record if the fire end is counted
        
        private void Start() {
            startIntensities = new float[fireParticleSystems.Length];

            for (var i = 0; i < fireParticleSystems.Length; i++)
            {
                startIntensities[i] = fireParticleSystems[i].emission.rateOverTime.constant;  
                print (startIntensities[i]);
            }
        }

        float timeLastWatered = 0;
        [SerializeField] private float regionDelay = 2.5f;
        [SerializeField] private float regionRate = .1f;
        
        private void Update() {
            if(isLit && currentIntensity < 1.0f && Time.time - timeLastWatered >= regionDelay){
                currentIntensity += regionRate * Time.deltaTime * 0.5f;   //0.5 to delay increase
                ChangeIntensity();
            }
        }

        public bool TryExtinguish (float amount){

            timeLastWatered = Time.time;
            if(ExtinguisherController.extinguisherType == "nozzle"){
                currentIntensity -= amount * 3.7f;
            }else if(SceneControl.scenario == 2){
                currentIntensity -= amount * 2.7f;
            }else if(SceneControl.scenario == 1){
                currentIntensity -= amount * 1.7f;
            }

            ChangeIntensity();
            // print("amount"+amount);

            if(currentIntensity <= 0){
                isLit = false;
                extinguished = true;
                isExtinguished = extinguished;
                return true;
            }

            return false;
        }

        private void ChangeIntensity(){
            for (var i = 0; i < fireParticleSystems.Length; i++)
            {
                var emission = fireParticleSystems[i].emission;
                // emission.rateOverTime = currentIntensity * startIntensities[i]; 
                emission.rateOverTime = currentIntensity;
            }
        }
    }
}
