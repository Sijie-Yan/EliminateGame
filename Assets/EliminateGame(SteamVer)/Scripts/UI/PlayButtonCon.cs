using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonCon : MonoBehaviour
{
    public void goToTheFirstQuest()
    {
        if (QuestHandler.inst.QuestStat == null)
        {
            QuestHandler.inst.creatNewQuestStat(0);
        }
        SceneManager.LoadScene("TheFirst");
    }
}
