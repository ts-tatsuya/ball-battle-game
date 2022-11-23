using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameInfo;


public class UINameChanger : MonoBehaviour
{

    public Text attackerNameText;
    public Text defenderNameText;
    public void UpdateName(){
        switch (PlayerRole)
        {
            case Roles.Attacker:
                attackerNameText.text = "Player - Attacker";
                defenderNameText.text = "Enemy - AI Defender";                
                break;
            case Roles.Defender:
                attackerNameText.text = "Enemy - AI Attacker";
                defenderNameText.text = "Player - Defender";
                break;
        }        
    }
}
