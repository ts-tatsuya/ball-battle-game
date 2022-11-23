using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameInfo;
public class GameData : MonoBehaviour
{

    public GameRulesHandler gameRules;
    public Text timeUI;
    public UINameChanger nameChanger;
    public ScoreBoard scoreBoard;
    public GateColorChange defenderGateMat;
    public GateColorChange attackerGateMat;
    public GateHandler defenderGate;
    public GateHandler attackerGate;
    public Material attackerMat;
    public Material defenderMat;
    public float attackerEnergy;
    public float defenderEnergy;
    public int timeInSec;
    public List<GameObject> attackersObj = new List<GameObject>();
    public List<GameObject> defenderObj = new List<GameObject>();
    public Color playerColor;
    public Color enemyColor;
    private bool isRegenerating;
    private bool isCounting;
    // Start is called before the first frame update
    void Start()
    {
        timeInSec = gameRules.timeLimit;
        UpdateColor();
        UpdateGateTeam();
    }

    // Update is called once per frame
    void Update()
    {
        RemoveAllEmptyActors();
        if(timeInSec == 0){
            StartCoroutine(NewGame());
        }
        if(isCounting == false && timeInSec > 0){
            isCounting = true;
            timeUI.text = timeInSec + "s";
            StartCoroutine(TimeCounter());
        }
        if(isRegenerating == false){
            isRegenerating = true;
            StartCoroutine(BarRegen());
        }
    }

    IEnumerator BarRegen(){
        yield return new WaitForSecondsRealtime(0.1f);
        attackerEnergy += gameRules.attackerInfo.energyRegen/10;
        defenderEnergy += gameRules.defenderInfo.energyRegen/10;
        isRegenerating = false;
    }

    IEnumerator TimeCounter(){
        yield return new WaitForSecondsRealtime(1);
        timeInSec--;
        isCounting = false;
    }

    public void DestroyAllActors(){
        foreach (GameObject obj in attackersObj)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in defenderObj)
        {
            Destroy(obj);
        }
        attackersObj = new List<GameObject>();
        defenderObj = new List<GameObject>();
    }

    public void RemoveAllEmptyActors(){
        attackersObj.RemoveAll(obj => obj == null);
        defenderObj.RemoveAll(obj => obj == null);
    }

    public IEnumerator NewGame(){

        gameRules.ball.transform.parent = null;
        gameRules.ball.GetComponent<Ball>().owner = null;
        Vector3 landPos = gameRules.landField.transform.position;
        gameRules.ball.transform.position = 
        new Vector3(Random.Range(landPos.x - 4, landPos.x + 4), landPos.y + 2, Random.Range(landPos.z, landPos.z - 6));
        DestroyAllActors();
        StartCoroutine(scoreBoard.HighlightScoreTemp(5));
        yield return new WaitForSecondsRealtime(5);
        timeInSec = gameRules.timeLimit;
        RoleSwitch();
        UpdateColor();
        UpdateGateTeam();
        nameChanger.UpdateName();
        
    }

    public void UpdateObjList(){
        
    }

    public GameObject NearestAttacker(GameObject selfObj){
        
        float distance = float.MaxValue;
        GameObject nearestObj = new GameObject();

        foreach (GameObject obj in attackersObj)
        {
            if(obj != selfObj && obj.GetComponent<AttackerBehaviour>().state == SoldierBehaviour.States.Active){
                if(distance > Vector3.Distance(selfObj.transform.position, obj.transform.position)){
                    distance = Vector3.Distance(selfObj.transform.position, obj.transform.position);
                    nearestObj = obj;
                }
            }
        }



        return nearestObj;
    }

    void UpdateColor(){

        switch (PlayerRole)
        {
            case Roles.Attacker:
                attackerGateMat.gateColor = playerColor;
                attackerMat.color = playerColor;
                defenderGateMat.gateColor = enemyColor;
                defenderMat.color = enemyColor;
                break;
            case Roles.Defender:
                attackerGateMat.gateColor = enemyColor;
                attackerMat.color = enemyColor;
                defenderGateMat.gateColor = playerColor;
                defenderMat.color = playerColor;
                break;
        }

    }

    void UpdateGateTeam(){

        switch (PlayerRole)
        {
            case Roles.Attacker:
                attackerGate.teamType = TeamType.Player;
                defenderGate.teamType = TeamType.Enemy;
                break;
            case Roles.Defender:
                attackerGate.teamType = TeamType.Enemy;
                defenderGate.teamType = TeamType.Player;
                break;
        }
        
    }

}
