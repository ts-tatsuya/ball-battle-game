using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameInfo;

public class GateHandler : MonoBehaviour
{
    public TeamType teamType;
    public GameData gameData;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider coll){

        if(coll.gameObject.tag == "Ball"){
            Debug.Log("myball is in");
            switch (PlayerRole)
            {
                case Roles.Attacker:
                    IncreaseScore(1, TeamType.Player);
                    break;
                case Roles.Defender:
                    IncreaseScore(1, TeamType.Enemy);
                    break;
            }
            coll.gameObject.transform.parent = null;
            coll.gameObject.GetComponent<Ball>().owner = null;
            StartCoroutine(gameData.NewGame());
        }

    }



}
