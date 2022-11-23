using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardMousePos : MonoBehaviour
{

     public GameObject target; //the enemy's target
     public float moveSpeed; //move speed
     public float rotationSpeed; //speed of turning
 
     void Start()
     {

     }
 
 
    void Update()
     {
 
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);

         transform.position += transform.forward * Time.deltaTime * moveSpeed;
 
     }
}
