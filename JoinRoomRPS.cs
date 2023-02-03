using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using TMPro;

public class JoinRoomRPS : MonoBehaviour
{
    DatabaseReference myDatabaseRef_JoinRoom;

    public TMP_InputField joinCodeField;
    public TMP_InputField joinNameField;
    private string _joinCode;
    public TMP_Text joinInfoText;
    public GameObject joinPanel; //������ �Ϸ�Ǹ� ��Ȱ��ȭ �ϱ�����
    private int readingTimeWaitTrigger; // ���� �д� �ð� ���� ��ư �̺�Ʈ�� ���� ���������Ƿ� ������ ���� �� �ִ�.
                                        // ���� �ڵ尡 �ǹٸ��� Ʈ���Ű��� 1�� �ǰ���.
    public TMP_Text codeVisibleText;
    public TMP_Text yourVisibleName;

    public GameObject joinBtnClickEvent;

    private int childrenCount;

    public GameObject beforeEnterRoomReturnBtn; //��ư�� ������ ����ϴٺ��� ��Ȱ��ȭ ��������Ѵ�.
    public GameObject joinUserReturnBtn;

    void Start()
    {
        myDatabaseRef_JoinRoom = FirebaseDatabase.DefaultInstance.RootReference; //���� ���� �غ�
        _joinCode = "";
        readingTimeWaitTrigger = 0;
        childrenCount = 0;
    }
    public void JoinRoomCode_RPS_OnEndEdit()
    {
        myDatabaseRef_JoinRoom.Child("RPS_Room").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {

                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    foreach (var _child in snapshot.Children) //users�� �����׸���� key���� �����´�.
                    {
                        if (_child.Key == joinCodeField.text)
                        {
                            _joinCode = joinCodeField.text;
                            childrenCount = Convert.ToInt32(snapshot.Child(_joinCode).ChildrenCount);//�� �θ� �����ϱ� ����
                        }
                            
                    }
                    if(_joinCode == joinCodeField.text)
                    {
                        readingTimeWaitTrigger = 1;
                    }
                    else
                    {
                        joinInfoText.text = "The code is not correct!";
                        _joinCode = "";
                        joinCodeField.text = "";
                    }
                }
            });
    }
    public void JoinRoomBtn()
    {
        if (readingTimeWaitTrigger == 1 && _joinCode == joinCodeField.text && childrenCount == 1)//�Ѹ� ���ӵǾ� �־�߸� �������
        {
            Debug.Log(childrenCount);
            myDatabaseRef_JoinRoom.Child("RPS_Room").Child(_joinCode).Child("JoinUserName").SetValueAsync(joinNameField.text);
            codeVisibleText.text = _joinCode;
            yourVisibleName.text = joinNameField.text;
            joinBtnClickEvent.SetActive(true);
            joinPanel.SetActive(false);
            beforeEnterRoomReturnBtn.SetActive(false);
            joinUserReturnBtn.SetActive(true); //�������� �ǵ��ư��� ��ư
        }
        else
            joinInfoText.text = "Room is full or doesn't exist";
    }
    
}
