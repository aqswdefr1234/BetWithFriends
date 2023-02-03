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
    public GameObject joinPanel; //접속이 완료되면 비활성화 하기위해
    private int readingTimeWaitTrigger; // 정보 읽는 시간 동안 버튼 이벤트가 먼저 끝나버리므로 문제가 생길 수 있다.
                                        // 따라서 코드가 옳바르면 트리거값이 1이 되게함.
    public TMP_Text codeVisibleText;
    public TMP_Text yourVisibleName;

    public GameObject joinBtnClickEvent;

    private int childrenCount;

    public GameObject beforeEnterRoomReturnBtn; //버튼을 여러개 사용하다보니 비활성화 시켜줘야한다.
    public GameObject joinUserReturnBtn;

    void Start()
    {
        myDatabaseRef_JoinRoom = FirebaseDatabase.DefaultInstance.RootReference; //정보 읽을 준비
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
                    foreach (var _child in snapshot.Children) //users의 하위항목들의 key값을 가져온다.
                    {
                        if (_child.Key == joinCodeField.text)
                        {
                            _joinCode = joinCodeField.text;
                            childrenCount = Convert.ToInt32(snapshot.Child(_joinCode).ChildrenCount);//총 두명만 접속하기 위해
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
        if (readingTimeWaitTrigger == 1 && _joinCode == joinCodeField.text && childrenCount == 1)//한명만 접속되어 있어야만 만들어짐
        {
            Debug.Log(childrenCount);
            myDatabaseRef_JoinRoom.Child("RPS_Room").Child(_joinCode).Child("JoinUserName").SetValueAsync(joinNameField.text);
            codeVisibleText.text = _joinCode;
            yourVisibleName.text = joinNameField.text;
            joinBtnClickEvent.SetActive(true);
            joinPanel.SetActive(false);
            beforeEnterRoomReturnBtn.SetActive(false);
            joinUserReturnBtn.SetActive(true); //조인유저 되돌아가기 버튼
        }
        else
            joinInfoText.text = "Room is full or doesn't exist";
    }
    
}
