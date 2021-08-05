using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    private Text textScore;
    private Text textMain;
    private Text textInfo;
    private Text textGameTitle;
    private Button btnStart;
    private Button btnRestart;
    private Button btnQuit;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CanvasSCript Start");
        textScore = transform.Find("TextScore").GetComponent<Text>();
        textMain = transform.Find("TextMain").GetComponent<Text>();
        textInfo = transform.Find("TextInfo").GetComponent<Text>();
        textGameTitle = transform.Find("TextGameTitle").GetComponent<Text>();
        btnStart = transform.Find("BtnStart").GetComponent<Button>();
        btnRestart = transform.Find("BtnRestart").GetComponent<Button>();
        btnQuit = transform.Find("BtnQuit").GetComponent<Button>();
    }

    // Update is called once per frame


    public void ShowScore(int score, int caught, int missed)
    {
        textScore.text = "Score: " + score + "\nBananas: " + caught + " of " + (caught + missed);
    }
    public void ShowMainText(string printText)
    {
        textMain.text = printText;
        textMain.enabled = true;
    }
    public void HideMainText()
    {
        textMain.enabled = false;
    }

    public void ShowFirstInfo(bool doShow)
    {
        textGameTitle.enabled = doShow;
        textInfo.enabled = doShow;
        btnStart.gameObject.SetActive(doShow);
    }

    public void ShowButtons(bool doShow)
    {
        btnRestart.gameObject.SetActive(doShow);
        btnQuit.gameObject.SetActive(doShow);   
    }


}
