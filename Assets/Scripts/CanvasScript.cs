using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    private Text textScore;
    private Text textGameOver;

    // Start is called before the first frame update
    void Start()
    {
        textScore = transform.Find("TextScore").GetComponent<Text>();
        textGameOver = transform.Find("TextGameOver").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScore(int score, int caught, int missed)
    {
        textScore.text = "Score: " + score + "\nBananas: " + caught + " of " + (caught + missed);
    }
    public void ShowBigText(string printText)
    {
        textGameOver.text = printText;
        textGameOver.enabled = true;
    }
    public void HideBigText()
    {
        textGameOver.enabled = false;
    }

    

}
