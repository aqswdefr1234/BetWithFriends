using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPS_SceneGo : MonoBehaviour
{
    public void RPS_SceneEnter()
    {
        SceneManager.LoadScene("RockPaperScissors");
    }
    public void Home_SceneReturn()
    {
        SceneManager.LoadScene("Home");
    }
}
