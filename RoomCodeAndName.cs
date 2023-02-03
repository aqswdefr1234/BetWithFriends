using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;

public class RoomCodeAndName : MonoBehaviour
{
    public GameObject battleBox; //배틀스크립트박스를 활성화 시킨다.

    DatabaseReference myDatabaseRef;
    public string _inRPS_roomCode;
    public string _inRPS_name;

    public TMP_InputField roomCodeField;
    public TMP_InputField nameField;

    private int duplicatiedDataTrigger;
    private string temporaryText; // 룸 코드 중복확인 후 룸 코드내용을 바꿔서 만드는 것을 방지

    public TMP_Text infoText; //TextMeshProUGUI 는 텍스트를 바꿀수는 있어도 text오브젝트에 직접연결 못한다.
                              //public 선언 해주더라도 하이라키창에서 인스펙터창의 스크립트에 UI를 직접 연결 못함.
    public TMP_Text codeVisibleText;
    public TMP_Text yourVisibleName;

    public GameObject activePanel;
    public GameObject beforeEnterRoomReturnBtn; //버튼을 여러개 사용하다보니 비활성화 시켜줘야한다.
    public GameObject hostReturnBtn;

    void Start()
    {
        duplicatiedDataTrigger = 0;
        myDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference; //데이터 쓰고 가져올 준비. 데이터베이스 정보
    }
    public void RPS_CreaterRoomCode(string roomCode, string hostName)
    {
        if (duplicatiedDataTrigger == 1 && temporaryText == _inRPS_roomCode)
            if (roomCodeField.text == "" || nameField.text == "")
            {
                infoText.text = "Please fill in the blanks";
            }
            else
            {
                myDatabaseRef.Child("RPS_Room").Child(roomCode).Child("HostName").SetValueAsync(hostName);
                duplicatiedDataTrigger = 0;
                codeVisibleText.text = roomCode;
                yourVisibleName.text = hostName;
                PlayerPrefs.SetString("RPS Recent code", roomCode); //비정상종료시 playerprefs값을 가져와 빈방을 삭제해주기위하여
                PlayerPrefs.SetString("RPS Recent name", hostName);
                activePanel.SetActive(false);
            }
                
        else
            infoText.text = "Please check the duplication";
    }
    public void HostRoomCodeDuplication_Search()
    {
        myDatabaseRef.Child("RPS_Room").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {

                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    foreach (var _child in snapshot.Children) //users의 하위항목들의 key값을 가져온다.
                    {
                        if(_child.Key == _inRPS_roomCode)
                        {
                            _inRPS_roomCode = "";
                            roomCodeField.text = "";
                        }
                    }
                    if(roomCodeField.text == "")
                    {
                        infoText.text = "There is a duplicate code, so please fill it out again";
                        duplicatiedDataTrigger = 0;
                    }
                        
                    else
                    {
                        infoText.text = "Available code!";
                        duplicatiedDataTrigger = 1;
                        temporaryText = _inRPS_roomCode;
                    }
                }
            });

    }
    public void CreateBtn()
    {
        RPS_CreaterRoomCode(_inRPS_roomCode, _inRPS_name);
        battleBox.SetActive(true);
        beforeEnterRoomReturnBtn.SetActive(false);
        hostReturnBtn.SetActive(true); //호스트용 되돌아가기 버튼. 조인유저 되돌아가기버튼과 코드가 다르기 때문에 버튼을 여러개만듬.
    }
    public void OnEndEdit_RoomCode()
    {
        _inRPS_roomCode = roomCodeField.text;
    }
    public void OnEndEdit_Name()
    {
        _inRPS_name = nameField.text;
    }
    
}
