using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameCon : MonoBehaviour
{
    public GameObject endGamePanel;
    public GameObject endGameText;
    public Sprite winText;
    public Sprite loseText;

    void Start()
    {
        GameEventsHandler.current.onWin += this.win;
        GameEventsHandler.current.onLose += this.lose;
        this.gameObject.SetActive(false);
    }

    private void showBG()
    {
        this.gameObject.SetActive(true);
        this.endGamePanel.transform.localScale = new Vector3(0f, 0f, 1f);
        LeanTween.scale(this.endGamePanel, new Vector3(1f, 1f, 1f), 0.5f);
    }

    private void win()
    {
        this.showBG();
        this.endGameText.GetComponent<Image>().sprite = this.winText;
        GameObject.Find("NextButton").SetActive(true);
        GameObject.Find("RestartButton").transform.localPosition = new Vector3(0f, -70f, 0f);
    }

    private void lose()
    {
        this.showBG();
        this.endGameText.GetComponent<Image>().sprite = this.loseText;
        GameObject.Find("NextButton").SetActive(false);
        GameObject.Find("RestartButton").transform.localPosition = new Vector3(150f, -70f, 0f);
    }

    private void OnDestroy()
    {
        GameEventsHandler.current.onWin -= this.win;
        GameEventsHandler.current.onLose -= this.lose;
    }
}
