using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gacha_Offline;
public class GachaImageChange : MonoBehaviour
{
    private Button meBtn;

    public Sprite pung;
    public Sprite pass;

    void Start()
    {
        meBtn = transform.GetComponent<Button>();
        meBtn.onClick.AddListener(Gacha);
    }
    void Gacha()
    {
        if (Convert.ToInt32(transform.parent.name) == Gacha_Offline_Battle.randomCount)
        {
            if (Gacha_Offline_Battle.selectMode == 0)
                transform.parent.GetComponent<Image>().sprite = pass;
            else
                transform.parent.GetComponent<Image>().sprite = pung;
        }
        else
        {
            if (Gacha_Offline_Battle.selectMode == 0)
                transform.parent.GetComponent<Image>().sprite = pung;
            else
                transform.parent.GetComponent<Image>().sprite = pass;
        }
        transform.parent.GetComponent<Image>().color = new Color32(255, 255, 225, 225); //기본 상태는 알베도 값이 0으로 되어 있음.
        gameObject.SetActive(false);
    }
    
}
