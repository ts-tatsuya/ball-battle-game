using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DefenderBehaviour : SoldierBehaviour
{
    // Start is called before the first frame update
    public SphereCollider fieldOfVision;
    public DefenderDetection detection;
    private Vector3 initialPos;
    public enum ActionStates{
        Idle,
        Chase
    }
    public ActionStates actionStates;
    protected override void Start()
    {
        base.Start();
        Mesh landMesh = gameRules.landField.GetComponent<MeshFilter>().mesh;
        Bounds landBounds = landMesh.bounds;
        float landWidth = landBounds.size.z;
        initialPos = transform.position;
        fieldOfVision.radius = (landWidth/100) * 35;

    }

    // Update is called once per frame
    void Update()
    {
        if(detection.target != null){
            actionStates = ActionStates.Chase;
        }else{
            actionStates = ActionStates.Idle;
        }
        switch (actionStates)
        {
            case ActionStates.Idle:
                chaseTarget = null;
                break;
            case ActionStates.Chase:
                chaseTarget = detection.target;
                break;
        }
        switch (state)
        {
            case States.Inactive:
                gameObject.GetComponent<Renderer>().material.color = inactiveColor;
                if(isOnCooltime == false){
                    StartCoroutine(Deactivate());
                }
                transform.position = 
                Vector3.MoveTowards(
                    transform.position, 
                    initialPos, 
                    Time.deltaTime * gameRules.defenderInfo.returnSpeed);
                break;
            case States.Active:
                gameObject.GetComponent<Renderer>().material.color = activeColor;
                if(actionStates == ActionStates.Chase){
                    Move();
                }
                break;   
        }
    }

    void OnCollisionEnter(Collision coll){
        if(coll.gameObject.tag == "Attacker"){
            if(coll.gameObject.GetComponent<AttackerBehaviour>().ball != null){
                state = States.Inactive;
            }
            
        }
    }

    protected override void Move()
    {
        base.Move();
        float speed;
        speed = gameRules.defenderInfo.normalSpeed;
        transform.position = Vector3.MoveTowards(transform.position, chaseTarget.transform.position, Time.deltaTime * speed);
        // transform.Translate(transform.forward * Time.deltaTime * speed);
    }
    IEnumerator Deactivate(){
        isOnCooltime = true;
        yield return new WaitForSecondsRealtime(gameRules.defenderInfo.cooltime);
        state = States.Active;
        isOnCooltime = false;
    }

    

    public GameObject Init(GameRulesHandler _gameRules){
        
        gameRules = _gameRules;
        return gameObject;
    }
}
