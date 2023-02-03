using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCreateORJoin : MonoBehaviour
{
    public GameObject creatProcessManager;
    public GameObject joinProcessManager;

    public GameObject beforeStartGameObject;
    public GameObject beforeJoinGameObject;

    public GameObject createAndJoinPanel;

    
    

    public void CreateModeBtn()
    {
        creatProcessManager.SetActive(true);
        beforeStartGameObject.SetActive(true);
        createAndJoinPanel.SetActive(false);
    }

    public void JoinModeBtn()
    {
        joinProcessManager.SetActive(true);
        beforeJoinGameObject.SetActive(true);
        
        createAndJoinPanel.SetActive(false);
    }
}
