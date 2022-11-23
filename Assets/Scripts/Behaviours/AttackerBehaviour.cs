using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameInfo;

public class AttackerBehaviour : SoldierBehaviour
{

    public GameObject ball;
    public GameObject gate;
    public GameData gameData;
    protected override void Start()
    {
        base.Start();

    }
    // Update is called once per frame
    void Update()
    {
        if(ball != null){
            chaseTarget = gate;
        }else if(gameRules.ball.GetComponent<Ball>().owner == null &&
        ball == null){
            chaseTarget = gameRules.ball;
        }else if(gameRules.ball.GetComponent<Ball>().owner != null &&
        ball == null){
            chaseTarget = null;
        }
        switch (state)
        {
            case States.Inactive:  
                gameObject.GetComponent<Renderer>().material.color = inactiveColor;
                // Physics.IgnoreLayerCollision(8,8);
                // Physics.IgnoreLayerCollision(8,9);
                if(ball != null){
                    ball.transform.parent = null;
                    ball.GetComponent<Ball>().thrownTo = gameData.NearestAttacker(gameObject).transform.position;
                    ball.GetComponent<Ball>().isThrown = true;
                    ball = null;
                }
                if(isOnCooltime == false){
                    StartCoroutine(Deactivate());
                }
                break;
            case States.Active:
                // Physics.IgnoreLayerCollision(8,8, false);
                // Physics.IgnoreLayerCollision(8,9, false);
                gameObject.GetComponent<Renderer>().material.color = activeColor;
                Move();
                break;   
        }
    }

    IEnumerator Deactivate(){
        isOnCooltime = true;
        yield return new WaitForSecondsRealtime(gameRules.attackerInfo.cooltime);
        state = States.Active;
        isOnCooltime = false;
    }

    void BallLock(){

        Vector3 ballPos = gameObject.transform.forward;
        ballPos.z += 0.5f;
        ball.transform.position = ballPos;
    }

    void OnCollisionEnter(Collision coll){

        if(coll.gameObject.tag == "Ball" && state == States.Active){
            ball = coll.gameObject;
            ball.transform.parent = transform;
            chaseTarget = null;
        }
        if(coll.gameObject.tag == "Defender" && ball != null){
            state = States.Inactive;
            if(gameData.attackersObj.Count == 1){
                switch (PlayerRole)
                {
                    case Roles.Attacker:
                        IncreaseScore(1, TeamType.Enemy);
                        break;
                    case Roles.Defender:
                        IncreaseScore(1, TeamType.Player);
                        break;
                }
                StartCoroutine(gameData.NewGame());
            }
        }
        if(coll.gameObject.tag == "DefenderWall" && ball != null){
            ball.transform.parent = null;
            ball.GetComponent<Ball>().owner = null;
            Destroy(gameObject);
        }else if(coll.gameObject.tag == "DefenderWall" && ball == null){
            Destroy(gameObject);
        }

    }
    protected override void Move()
    {
        base.Move();
        Quaternion rotToTarget = transform.rotation;
        
        if(chaseTarget != null){
            Debug.Log("Chase");
            rotToTarget = Quaternion.Euler(
                rotToTarget.eulerAngles.x, 
                Quaternion.LookRotation(chaseTarget.transform.position - transform.position).eulerAngles.y,
                rotToTarget.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                rotToTarget, 
                rotationSpeed * Time.deltaTime);
        }else{
            Debug.Log("NotChase");
            transform.rotation = Quaternion.Lerp(transform.rotation, intitalRot, rotationSpeed);
        }
        float speed;
        speed = gameRules.attackerInfo.normalSpeed;
        
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    public GameObject Init(GameObject _gate, GameRulesHandler _gameRules, GameData _gameData){
        gate = _gate;
        gameRules = _gameRules;
        gameData = _gameData;
        return gameObject;
    }
}
