using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using TMPro;

public class RematchScript : MonoBehaviour
{
    DatabaseReference myDatabaseRef;
    public GameObject hostScriptBox; //����ġ ��ư�� ������ ���� �ش�Ǵ� ������Ʈ�� Ȱ��ȭ �ȴ�. battle ��ũ��Ʈ�� start��� onEnable�Լ��� �־��� ������ Ȱ��ȭ �ɶ����� �ٽ� �۵��ȴ�.
    public GameObject joinScriptBox;

    public GameObject hostRematchBtn;
    public GameObject joinRematchBtn;

    public TMP_Text roomCode; //�� ������ �������� ���Ͽ�
    void Start()
    {
        myDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void Rematch_HostActive()
    {
        hostScriptBox.SetActive(true);
        hostRematchBtn.SetActive(false);
    }
    public void Rematch_JoinActive()
    {
        joinScriptBox.SetActive(true);
        joinRematchBtn.SetActive(false);
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("State").RemoveValueAsync(); //ȣ��Ʈ�� ��ŸƮ ��ư�� room code�� �ڽ��� 2�϶� ��Ÿ���Ƿ� ���������� ����ġ��ư�� ��������
    }                                                                                           //�����ǰ� �����ν� �׶����� ��μ� ��ŸƮ ��ư�� Ȱ��ȭ �ǵ�����.
}
