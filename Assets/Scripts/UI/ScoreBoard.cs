using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameInfo;

public class ScoreBoard : MonoBehaviour
{
    public Text text;
    Color boardColor;
    Color textColor;
    
    // Start is called before the first frame update
    void Start()
    {
        boardColor = gameObject.GetComponent<Image>().color;
        textColor = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = 
        "Player - Enemy" +
        "\n\n" +
        PlayerScore + " - " + EnemyScore;
    }

    public void HighlightScore(bool isOn){

        
        switch (isOn)
        {
            case true:
                gameObject.GetComponent<Image>().color = 
                new Color(boardColor.r, boardColor.g, boardColor.b, 1);
                text.color = new Color(textColor.r, textColor.g, textColor.b, 1);
                break;
            case false:
                Debug.Log("turnOff");
                gameObject.GetComponent<Image>().color = 
                new Color(boardColor.r, boardColor.g, boardColor.b, 0.5f);
                text.color = new Color(textColor.r, textColor.g, textColor.b, 0.5f);
                break;
        }
    }

    public IEnumerator HighlightScoreTemp(float t){

        HighlightScore(true);
        yield return new WaitForSecondsRealtime(t);
        HighlightScore(false);

    }
}
