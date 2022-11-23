using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject owner;
    public bool isThrown;
    public Vector3 thrownTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isThrown){
            Thrown();
        }
        if(owner != null){
            if(owner.GetComponent<AttackerBehaviour>().state == AttackerBehaviour.States.Inactive){
                owner = null;
            }
        }
        
    }

    void OnCollisionEnter(Collision coll){
        if(coll.gameObject.tag == "Attacker"){
            if(coll.gameObject.GetComponent<AttackerBehaviour>().state == 
                SoldierBehaviour.States.Active){
                isThrown = false;
                thrownTo = new Vector3();
                owner = coll.gameObject;
            }
        }
    }

    void Thrown(){
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, thrownTo, 1.5f * Time.deltaTime);
    }
}
