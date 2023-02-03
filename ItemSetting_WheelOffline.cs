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
        }   //�� 6�����.��ǲ�ʵ��г��� �ڽ��� ã�� Ȱ��ȭ ��Ų��.gameObject �� �����Ǵ� ����� �ڱ��ڽ��� ����Ŵ.
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
            clone.transform.Rotate(0,0, (360 / float.Parse(itemCountText.text)) * i); // ������ ��Ʈ�� �ٸ����� �Ǽ����� ��� �Ǽ������� ����.�Ͻ�������ȯ
            //Transform�� ������Ʈ���� ������ �ִ� �⺻���� ������Ʈ�̴�. ����Ҷ��� transformó�� �ҹ��ڷ� ����Ѵ�.( = GetComponent<Transform>())
            //https://ssabi.tistory.com/24 �����̼ǰ� ������Ʈ ����
            cloneText.transform.Rotate(0, 0, (360 / float.Parse(itemCountText.text)) * i + (360 / float.Parse(itemCountText.text)) / 2);
            //Pivot ���� �ȹٲ��ָ� ���ڸ����� ȸ���ع�����. ���� ������ �߽����� ������Ʈ�� �������� �Ű��. ���ʾƷ���(0,0), ������ ���� (1,1)�̴�.
            //���⼭ text������Ʈ�� (0.5 , -4.5) �̴�.
            cloneText.text = inputField[i].text;
        }
    }
}