using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonCon : MonoBehaviour
{
    public void restartGame()
    {
        // QuestHandler.inst.resetQuestStat(QuestHandler.inst.QuestStat.curQuestIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
