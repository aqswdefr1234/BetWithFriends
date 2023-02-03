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

public class RPS_BattleRealTime : MonoBehaviour //ȣ��Ʈ �����Դϴ�.
{
    DatabaseReference myDatabaseRef;
    public TMP_Text roomCode;
    public GameObject gameStartBtn; //����� �θ� ������ ���ӽ��۹�ư�� Ȱ��ȭ
    public TMP_Text timerText;  //Ÿ�̸ӿ� ���Ӱ���� ������

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
    public GameObject rematchBtn_Host; //ȣ��Ʈ���� ����ġ ��ư
    
    void OnEnable()
    {
        choiceCounter = 0;
        change_Img.GetComponent<Image>();
        waitDB_CoroutineTrigger = 0;
        timerTrigger = 0;
        timer = 8.0f;
        countUsers = 0;
        myDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        StartCoroutine("StartGameBtnActive");
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
                myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("HostValue").SetValueAsync(RockPaperScissorsScripts.choiceRPS); //�ð��� �� �Ǹ� �����ͺ��̽��� ���� �Է��Ѵ�.
                StartCoroutine("WaitDB_RPS");
            }  //������������ ������ ���� "HostName" �ʵ� �Ʒ�, "Choice" �ʵ忡 ���� �ִ´�.
        }
    }
    public void JoinRoomCode_RPS_OnEndEdit()
    {
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text).GetValueAsync().ContinueWithOnMainThread(task =>
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
    IEnumerator StartGameBtnActive()
    {
        while (true)
        {
            if (roomCode.text != "")
            {
                if (countUsers == 2)
                {
                    gameStartBtn.SetActive(true);
                    Debug.Log(countUsers);
                    yield break;
                }
                else
                {
                    JoinRoomCode_RPS_OnEndEdit();
                    Debug.Log("�ڷ�ƾ������");
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
    public void StartBtnClickEventFunction()
    {
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("State").SetValueAsync("Start");
        timerTrigger = 1; // Ÿ�̸� ����!
        gameStartBtn.SetActive(false);
    }
    public void GameResult_Enemy()
    {// ���� ������ ��������
        if (choiceCounter != 1) //������ ���� �ԷµǾ����� Ȯ���ϴ� ����. ���������� ������ ���� ������� �ʴٸ� choiceCounter�� 1�� �ְ�, else ���� ����.
                                //���� ����ִٸ� waitDB_CoroutineTrigger���� 0�̹Ƿ� WaitDB_RPS() �ڷ�ƾ�� ���� if(choiceCounter != 1) �κ��� ��� �ݺ��ȴ�.
                                //���� �ԷµȰ� Ȯ�εǸ� choiceCounter�� 1�� �ȴ�.
                                //���������� waitDB_CoroutineTrigger���� 0�̴�. ���� �ڷ�ƾ�� ���� GameResult_Enemy() �ݺ��ȴ�. �ڷ�ƾ�� �ѹ� �� �� �� enemyData�� ��������
                                //waitDB_CoroutineTrigger�� 1�� �ǹǷ�, �ڷ�ƾ WaitDB_RPS()�� else �κп� �ִ� WhoIsWinner()�� �����ϰ� �ڷ�ƾ�� ����ȴ�.
                                //�ڽ��� ���� ������ �������ڸ� ��������, ���� ���� �����Ѵ�. �ڽ��� ���� �ƴ� ������ ���� �����ͺ��̽����� �����ϴ� ������
                                //���� ������ ��Ⱑ �������̰� ���ϴٸ� ������ �ڽ��� ���� �������⵵ ���� �ڽ��� �����͸� �����Ҽ��� �ֱ� �����̴�.
                                //�����͸� �����ϴ� ������ ����ġ�� �ϱ� ���ؼ��̴�.
                                //����� ������ ���� ���ٸ� WaitDB_RPS() �ڷ�ƾ�� ��� ���� �ǹǷ� �Ϲ������� ���� �о���� ���ϴ� ���� ����.
        {
            myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("JoinValue").GetValueAsync().ContinueWithOnMainThread(task =>
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
        else //������ ���� ����ִٸ� �Ʒ� ���� ����
        {
            myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("JoinValue").GetValueAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.Log("Wait...");
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
        else if( (enemyData == 1 && RockPaperScissorsScripts.choiceRPS == 2) || (enemyData == 2 && RockPaperScissorsScripts.choiceRPS == 3) || (enemyData == 3 && RockPaperScissorsScripts.choiceRPS == 1) || enemyData == 0)
            timerText.text = "Win!";
        else
            timerText.text = "Lose...";

        Debug.Log("���� ��");
        RemoveDatabase(); //���� ���� �ԷµǾ�� �����ȴ�. �ֳ��ϸ� GameResult_Enemy()�� 
        Debug.Log("���� ��");//firebase�� Ű �� �����ͺ��̽��̹Ƿ� ���� �����ϸ�
                          //Ű�� ��ü������ ������ �� ���� ������ Ű�� ���ŵ�.
        rematchBtn_Host.SetActive(true);
        this.gameObject.SetActive(false);


    }
    IEnumerator WaitDB_RPS()//�ð��� �� �Ǹ� �����ϴµ� GameResult_Enemy()�� �ԷµǾ��ִ� ������ ���� �������µ� �����ϸ� waitDB_CoroutineTrigger = 1 �� �ȴ�.
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
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("JoinValue").RemoveValueAsync();
    }
    void OnApplicationQuit()
    {
        QuitRemoveData();
    }
    void QuitRemoveData()
    {
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text).RemoveValueAsync();
        PlayerPrefs.SetInt("Graceful shutdown", 1); //�������� ����� 1 ����
    }
}
