using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using RPS_ScriptsNameSpace;
using UnityEngine.UI;

public class RPS_Battle_Join : MonoBehaviour  //JoinUser �����Դϴ�.
{
    DatabaseReference myDatabaseRef;
    public TMP_Text roomCode;
    public TMP_Text joinUserName;
    public TMP_Text timerText;

    private int countUsers;
    private float timer;
    private int timerTrigger; //������ ���۵Ǹ� Ÿ�̸Ӹ� �۵���Ű�� ���ؼ�
    private int enemyData;
    private int waitDB_CoroutineTrigger; // �ڷ�ƾ�ȿ� ����� Ʈ���Ű�. db���� �޾ƿö����� ��ٸ�
    private int choiceCounter;  // ������ �а� ��ϵǾ��� ���� �����ϱ� ���� ���������� child ���� �о����.
    //---���� ���� �̹���
    public Sprite rock_Img;
    public Sprite paper_Img;
    public Sprite scissors_Img;
    public Image change_Img;
    //---
    public GameObject rematchBtn_Join; //�������� ����ġ ��ư

    void OnEnable()
    {
        choiceCounter = 0;
        change_Img.GetComponent<Image>();
        waitDB_CoroutineTrigger = 0;
        countUsers = 0;
        timer = 0f;
        timerTrigger = 0;
        myDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        StartCoroutine("StartGameJoinUser");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (timerTrigger == 1)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString();
            if (timer <= 0f)
            {
                timerTrigger = 0;
                timer = 0f;
                timerText.text = timer.ToString();
                myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("JoinValue").SetValueAsync(RockPaperScissorsScripts.choiceRPS);
                StartCoroutine("WaitDB_RPS");
            }  //������������ ������ ���� "JoinUserName" �ʵ� �Ʒ�, "Choice" �ʵ忡 ���� �ִ´�.
        }
    }
    void JoinRoomCountUser_RPS()
    {
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {

                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    countUsers = Convert.ToInt32(snapshot.ChildrenCount);
                }
            });
    }
    IEnumerator StartGameJoinUser()
    {
        while (true)
        {
            if (roomCode.text != "")
            {
                if (countUsers == 3)
                {
                    Debug.Log(countUsers);

                    TimerStart_RPS_JoinUser();
                    yield break;
                }
                else
                {
                    JoinRoomCountUser_RPS();
                    Debug.Log("�ڷ�ƾ������");
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
    
    public void GameResult_Enemy()
    {// ���� ������ ��������
        if (choiceCounter != 1) //������ ���� �ԷµǾ����� Ȯ���ϴ� ����
        {
            myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("HostValue").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Wait...");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Value != null)
                        choiceCounter = 1;
                }
            });
        }
        else
        {
            myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("HostValue").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    enemyData = Convert.ToInt32(snapshot.Value);
                    waitDB_CoroutineTrigger = 1;
                    // Do something with snapshot...
                }
            });
        }
    }
    void TimerStart_RPS_JoinUser()
    {
        timer = 8;  //Ÿ�̸� 8�� ����
        timerTrigger = 1; //Ÿ�̸� �۵�����!
    }
    public void WhoIsWinner()
    {
        //���� �� ���̱�
        if (enemyData == 1)
            change_Img.sprite = rock_Img;
        else if (enemyData == 2)
            change_Img.sprite = paper_Img;
        else if (enemyData == 3)
            change_Img.sprite = scissors_Img;
        else
            change_Img.sprite = null;

        if (enemyData == RockPaperScissorsScripts.choiceRPS)
            timerText.text = "Draw!";
        else if ((enemyData == 1 && RockPaperScissorsScripts.choiceRPS == 2) || (enemyData == 2 && RockPaperScissorsScripts.choiceRPS == 3) || (enemyData == 3 && RockPaperScissorsScripts.choiceRPS == 1) || enemyData == 0 )
            timerText.text = "Win!";
        else
            timerText.text = "Lose...";
        Debug.Log("���� ��");
        RemoveDatabase();
        Debug.Log("���� ��");
        this.gameObject.SetActive(false);
        rematchBtn_Join.SetActive(true);

    }
    IEnumerator WaitDB_RPS()
    {
        while (true)
        {

            if (waitDB_CoroutineTrigger == 0)
            {
                GameResult_Enemy();
            }
            else
            {
                WhoIsWinner();
                yield break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    void RemoveDatabase()
    {
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("HostValue").RemoveValueAsync();
    }
    void OnApplicationQuit() //�� ����� ������ �����ϱ� ����
    {
        QuitRemoveData();
    }
    void QuitRemoveData()
    {
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("JoinUserName").RemoveValueAsync();
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("State").RemoveValueAsync();
    }
}
