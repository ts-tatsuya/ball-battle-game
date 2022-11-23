using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameInfo;

public class SoldierBehaviour : MonoBehaviour
{
    public enum States{
        Inactive, 
        Active
    }
    public States state;
    protected Color inactiveColor;
    protected Color activeColor;
    protected bool isOnCooltime;
    public GameRulesHandler gameRules;
    public GameObject chaseTarget = null;
    public Quaternion intitalRot;
    public float rotationSpeed;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        intitalRot = transform.rotation;
        activeColor = gameObject.GetComponent<Renderer>().material.color;
        inactiveColor = gameObject.GetComponent<Renderer>().material.color;
        inactiveColor.a = 0.5f;
    }
    protected virtual void Move(){

        
    }

}
