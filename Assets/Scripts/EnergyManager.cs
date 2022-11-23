using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameInfo;

public class EnergyManager : MonoBehaviour
{
    public GameRulesHandler gameRules;
    public Roles role;
    public GameData gameData;
    public float energy;
    private bool isRegenerating;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (role)
        {
            case Roles.Attacker:
                energy = gameData.attackerEnergy;
                break;
            case Roles.Defender:
                energy = gameData.defenderEnergy;
                break;
        }

    }
    
}
