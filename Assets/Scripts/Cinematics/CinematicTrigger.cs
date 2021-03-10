using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;


namespace RPG.Cinematics
{
    
public class CinematicTrigger : MonoBehaviour, ISaveable
{
    bool wasTriggered = false;



        private void OnTriggerEnter(Collider other) {

        //print("cinematics triggered");

        if (other.gameObject.tag == "Player" && !wasTriggered) {
            //print("true");
            GetComponent<PlayableDirector>().Play();
            wasTriggered = true;
            GetComponent<Collider>().enabled = false;
        }
        }

        object ISaveable.CaptureState()
        {
            return wasTriggered;
        }

        void ISaveable.RestoreState(object state)
        {
            wasTriggered = (bool)state;
            if(wasTriggered) {
                GetComponent<Collider>().enabled = false;
            } else {
                GetComponent<Collider>().enabled = true;
            }
        }
}
}