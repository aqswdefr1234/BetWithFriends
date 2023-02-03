using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSetting_WheelOffline : MonoBehaviour
{
    public TMP_Text itemCountText;
    public TMP_Text itemCountText_Battle_Panel;
    public GameObject settingPanel;
    public GameObject inputFieldPanel;
    public TMP_InputField[] inputField = new TMP_InputField[8];
    public Transform roulleteImage_Transform;
    public GameObject line_Image_Prefab;
    public TMP_Text item_text_Prefab;

    private GameObject clone;
    private TMP_Text cloneText;

    public void ItemAddBtn()
    {
        if (Convert.ToInt32(itemCountText.text) > 1 && Convert.ToInt32(itemCountText.text) < 8)
        {
            itemCountText.text = Convert.ToString(Convert.ToInt32(itemCountText.text) + 1);
            inputField[Convert.ToInt32(itemCountText.text) - 1].gameObject.SetActive(true);
        }   //총 6명까지.인풋필드패널의 자식을 찾아 활성화 시킨다.gameObject 는 참조되는 대상의 자기자신을 가리킴.
    }
    public void ItemRemoveBtn()
    {
        if (Convert.ToInt32(itemCountText.text) > 2 && Convert.ToInt32(itemCountText.text) < 9)
        {
            inputField[Convert.ToInt32(itemCountText.text) - 1].gameObject.SetActive(false);
            itemCountText.text = Convert.ToString(Convert.ToInt32(itemCountText.text) - 1);
        }
    }
    public void GameStartBtn()
    {
        itemCountText_Battle_Panel.text = itemCountText.text;
        CreatLineAndDivideCircle();
        settingPanel.SetActive(false);
    }
    public void CreatLineAndDivideCircle()
    {
        for(int i = 0; i < Convert.ToInt32(itemCountText.text); i++)
        {
            clone = Instantiate(line_Image_Prefab, roulleteImage_Transform);
            cloneText = Instantiate(item_text_Prefab, roulleteImage_Transform);
            clone.transform.Rotate(0,0, (360 / float.Parse(itemCountText.text)) * i); // 한쪽이 인트형 다른쪽이 실수형일 경우 실수형으로 계산됨.암시적형변환
            //Transform은 오브젝트들이 가지고 있는 기본적인 컴포넌트이다. 사용할때는 transform처럼 소문자로 사용한다.( = GetComponent<Transform>())
            //https://ssabi.tistory.com/24 로테이션과 로테이트 차이
            cloneText.transform.Rotate(0, 0, (360 / float.Parse(itemCountText.text)) * i + (360 / float.Parse(itemCountText.text)) / 2);
            //Pivot 값을 안바꿔주면 제자리에서 회전해버린다. 따라서 원판의 중심점에 오브젝트의 기준점을 옮겼다. 왼쪽아래가(0,0), 오른쪽 위가 (1,1)이다.
            //여기서 text오브젝트는 (0.5 , -4.5) 이다.
            cloneText.text = inputField[i].text;
        }
    }
}