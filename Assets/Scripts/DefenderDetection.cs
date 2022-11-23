using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderDetection : MonoBehaviour
{
    public GameObject target;

    void Update(){
        if(target != null){
            if(target.GetComponent<AttackerBehaviour>().ball == null){
                target = null;
            }
        }
    }
    void OnTriggerEnter(Collider coll){
        if(coll.gameObject.tag == "Attacker"){
            if(coll.gameObject.GetComponent<AttackerBehaviour>().ball != null){
                target = coll.gameObject;
            }
        }
    }
    void OnTriggerExit(Collider coll){

        if(coll.gameObject.tag == "Attacker"){
            if(coll.gameObject.GetComponent<AttackerBehaviour>().ball != null){
                target = null;
            }
        }

    }
}
