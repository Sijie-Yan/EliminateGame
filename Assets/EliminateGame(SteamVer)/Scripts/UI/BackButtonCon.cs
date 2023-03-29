using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonCon : MonoBehaviour
{
    public void backToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
