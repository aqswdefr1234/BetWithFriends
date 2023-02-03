using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;

public class RoomCodeAndName : MonoBehaviour
{
    public GameObject battleBox; //��Ʋ��ũ��Ʈ�ڽ��� Ȱ��ȭ ��Ų��.

    DatabaseReference myDatabaseRef;
    public string _inRPS_roomCode;
    public string _inRPS_name;

    public TMP_InputField roomCodeField;
    public TMP_InputField nameField;

    private int duplicatiedDataTrigger;
    private string temporaryText; // �� �ڵ� �ߺ�Ȯ�� �� �� �ڵ峻���� �ٲ㼭 ����� ���� ����

    public TMP_Text infoText; //TextMeshProUGUI �� �ؽ�Ʈ�� �ٲܼ��� �־ text������Ʈ�� �������� ���Ѵ�.
                              //public ���� ���ִ��� ���̶�Űâ���� �ν�����â�� ��ũ��Ʈ�� UI�� ���� ���� ����.
    public TMP_Text codeVisibleText;
    public TMP_Text yourVisibleName;

    public GameObject activePanel;
    public GameObject beforeEnterRoomReturnBtn; //��ư�� ������ ����ϴٺ��� ��Ȱ��ȭ ��������Ѵ�.
    public GameObject hostReturnBtn;

    void Start()
    {
        duplicatiedDataTrigger = 0;
        myDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference; //������ ���� ������ �غ�. �����ͺ��̽� ����
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
                PlayerPrefs.SetString("RPS Recent code", roomCode); //����������� playerprefs���� ������ ����� �������ֱ����Ͽ�
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
                    foreach (var _child in snapshot.Children) //users�� �����׸���� key���� �����´�.
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
        hostReturnBtn.SetActive(true); //ȣ��Ʈ�� �ǵ��ư��� ��ư. �������� �ǵ��ư����ư�� �ڵ尡 �ٸ��� ������ ��ư�� ����������.
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
