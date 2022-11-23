using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class GateColorChange : MonoBehaviour
{
    // public Material material;
    // private Material prevMaterial;
    public Color gateColor;
    private Color gatePrevColor;
    public GameObject[] gateObjects;

    // Update is called once per frame
    void Update()
    {
        // if(prevMaterial != material && material != null){
        //     Debug.Log("material is changing");
        //     UpdateGateMaterial();
        //     prevMaterial = material;
        // }
        if(gatePrevColor != gateColor){
            UpdateGateColor();
            gatePrevColor = gateColor;
        }
        
    }

    // void UpdateGateMaterial(){
    //     foreach (GameObject obj in gateObjects)
    //     {
    //         obj.GetComponent<Renderer>().material = material;
    //     }
    // }
    void UpdateGateColor(){
        foreach (GameObject obj in gateObjects)
        {
            obj.GetComponent<Renderer>().sharedMaterial.color = gateColor;
        }
    }

}
