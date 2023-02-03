using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePanel_StartBtn : MonoBehaviour
{
    public GameObject coverPanel;
    public void BattlePanelStartBtn()
    {
        coverPanel.SetActive(false);
    }
}
