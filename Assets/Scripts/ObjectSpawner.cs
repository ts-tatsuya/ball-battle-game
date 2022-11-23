using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameInfo;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject attackerToSpawn;
    public GameObject defenderToSpawn;
    public GameObject land;
    public GameRulesHandler gameRules;
    public GameObject defenderGate;
    public GameData gameData;
    private bool isCooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooldown == false){
            if (Input.touchCount > 0 && PlayerRole == Roles.Attacker 
            && gameData.attackerEnergy 
            >= gameRules.attackerInfo.energyCost)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if(Physics.Raycast(ray, out RaycastHit hit) && hit.point.z < land.transform.position.z){
                    Debug.Log("spawnattacker");
                    Vector3 touchPos = hit.point;
                    touchPos.y += 1;
                    //touchPos.y += 1;
                    Debug.Log(touchPos);
                    gameData.attackersObj.Add(Instantiate(attackerToSpawn, touchPos, new Quaternion(0,0,0,1))
                        .GetComponent<AttackerBehaviour>()
                        .Init(defenderGate, gameRules, gameData));
                    gameData.attackerEnergy -= gameRules.attackerInfo.energyCost;
                    isCooldown = true;
                    StartCoroutine(SpawnCooldown());
                }
            }
            if (Input.touchCount > 0 && PlayerRole == Roles.Defender 
            && gameData.defenderEnergy 
            >= gameRules.defenderInfo.energyCost)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if(Physics.Raycast(ray, out RaycastHit hit) && hit.point.z > land.transform.position.z){
                    Debug.Log("spawndefender");
                    Vector3 touchPos = hit.point;
                    touchPos.y += 1;
                    
                    Debug.Log(touchPos);
                    gameData.defenderObj.Add(Instantiate(defenderToSpawn, touchPos, new Quaternion(0,0,0,1))
                        .GetComponent<DefenderBehaviour>()
                        .Init(gameRules));
                    gameData.defenderEnergy -= gameRules.defenderInfo.energyCost;
                    isCooldown = true;
                    StartCoroutine(SpawnCooldown());
                }
            }
        }
    }

    IEnumerator SpawnCooldown(){
        yield return new WaitForSeconds(1);
        isCooldown = false;
    }
}
