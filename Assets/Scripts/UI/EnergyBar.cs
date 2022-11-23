using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameInfo;

public class EnergyBar : MonoBehaviour
{
    public Color filledColor;
    public Color partialColor;
    public EnergyManager energyManager;
    public GameObject[] bars;
    private float barSize;
    private float prevEnergy;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in bars)
        {
            barSize = obj.GetComponent<RectTransform>().sizeDelta.x;
            obj.GetComponent<RectTransform>().sizeDelta = 
            new Vector2(0, obj.GetComponent<RectTransform>().sizeDelta.y);
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateColorData();
        if(prevEnergy != energyManager.energy){
            UpdateEnergyBarUI(energyManager.energy);
        }
        prevEnergy = energyManager.energy;
    }

    void UpdateColorData(){
        switch (PlayerRole)
        {
            case Roles.Attacker:
            switch (energyManager.role)
            {
                case Roles.Attacker:
                filledColor = energyManager.gameData.playerColor;
                partialColor = new Color(
                    filledColor.r - (filledColor.r/5), 
                    filledColor.g - (filledColor.g/5), 
                    filledColor.b - (filledColor.b/5));
                break;
                case Roles.Defender:
                filledColor = energyManager.gameData.enemyColor;
                partialColor = new Color(
                    filledColor.r - (filledColor.r/5), 
                    filledColor.g - (filledColor.g/5), 
                    filledColor.b - (filledColor.b/5));
                break;
            }
            break;
            case Roles.Defender:
            switch (energyManager.role)
            {
                case Roles.Attacker:
                filledColor = energyManager.gameData.enemyColor;
                partialColor = new Color(
                    filledColor.r - (filledColor.r/5), 
                    filledColor.g - (filledColor.g/5), 
                    filledColor.b - (filledColor.b/5));
                break;
                case Roles.Defender:
                filledColor = energyManager.gameData.playerColor;
                partialColor = new Color(
                    filledColor.r - (filledColor.r/5), 
                    filledColor.g - (filledColor.g/5), 
                    filledColor.b - (filledColor.b/5));
                break;
            }
            break;
        }
    }
    void UpdateEnergyBarUI(float energyForUpdate){
        int eBar = Mathf.CeilToInt(energyForUpdate);
        for(int i = 0; i < 6; i++){
            if(i < eBar){
                if(energyForUpdate >= 1){
                    bars[i].GetComponent<RectTransform>().sizeDelta = 
                        new Vector2(barSize, bars[i].GetComponent<RectTransform>().sizeDelta.y);
                    bars[i].GetComponent<Image>().color = filledColor;
                    energyForUpdate -= 1;
                }else{
                    bars[i].GetComponent<RectTransform>().sizeDelta = 
                        new Vector2(energyForUpdate * barSize, bars[i].GetComponent<RectTransform>().sizeDelta.y);
                    bars[i].GetComponent<Image>().color = partialColor;
                    energyForUpdate = 0;
                }
            }else{
                bars[i].GetComponent<RectTransform>().sizeDelta = 
                        new Vector2(0, bars[i].GetComponent<RectTransform>().sizeDelta.y);
                bars[i].GetComponent<Image>().color = partialColor;
            }
        }
    }

    
}
