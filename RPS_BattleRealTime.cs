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

public class RPS_BattleRealTime : MonoBehaviour //호스트 전용입니다.
{
    DatabaseReference myDatabaseRef;
    public TMP_Text roomCode;
    public GameObject gameStartBtn; //사람이 두명 있으면 게임시작버튼이 활성화
    public TMP_Text timerText;  //타이머와 게임결과를 보여줌

    private int countUsers;
    private float timer;
    private int timerTrigger; //게임이 시작되면 타이머를 작동시키기 위해서
    private int enemyData;
    private int waitDB_CoroutineTrigger; // 코루틴안에 사용할 트리거값. db값을 받아올때까지 기다림
    private int choiceCounter;  // 상대방의 패가 등록되었을 때를 인지하기 위해 조인유저의 child 값을 읽어야함.
    //---상대방 정보 이미지
    public Sprite rock_Img;
    public Sprite paper_Img;
    public Sprite scissors_Img;
    public Image change_Img;
    //---
    public GameObject rematchBtn_Host; //호스트유저 리매치 버튼
    
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
                myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("HostValue").SetValueAsync(RockPaperScissorsScripts.choiceRPS); //시간이 다 되면 데이터베이스에 값을 입력한다.
                StartCoroutine("WaitDB_RPS");
            }  //가위바위보중 선택한 값을 "HostName" 필드 아래, "Choice" 필드에 값을 넣는다.
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
                    Debug.Log("코루틴실행중");
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
    public void StartBtnClickEventFunction()
    {
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("State").SetValueAsync("Start");
        timerTrigger = 1; // 타이머 시작!
        gameStartBtn.SetActive(false);
    }
    public void GameResult_Enemy()
    {// 상대방 데이터 가져오기
        if (choiceCounter != 1) //데이터 값이 입력되었는지 확인하는 과정. 조인유저가 선택한 값이 비어있지 않다면 choiceCounter에 1을 넣고, else 구문 실행.
                                //만약 비어있다면 waitDB_CoroutineTrigger값이 0이므로 WaitDB_RPS() 코루틴에 의해 if(choiceCounter != 1) 부분이 계속 반복된다.
                                //값이 입력된게 확인되면 choiceCounter가 1이 된다.
                                //아직까지는 waitDB_CoroutineTrigger값이 0이다. 따라서 코루틴에 의해 GameResult_Enemy() 반복된다. 코루틴이 한번 더 돌 때 enemyData를 가져오고
                                //waitDB_CoroutineTrigger이 1이 되므로, 코루틴 WaitDB_RPS()의 else 부분에 있는 WhoIsWinner()를 실행하고 코루틴이 종료된다.
                                //자신의 값과 비교한후 승자패자를 결정짓고, 상대방 값을 제거한다. 자신의 값이 아닌 상대방의 값을 데이터베이스에서 제거하는 이유는
                                //만약 서로의 기기가 성능차이가 심하다면 상대방이 자신의 값을 가져오기도 전에 자신의 데이터를 삭제할수가 있기 때문이다.
                                //데이터를 삭제하는 이유는 리매치를 하기 위해서이다.
                                //참고로 상대방의 값이 없다면 WaitDB_RPS() 코루틴이 계속 돌게 되므로 일반적으로 값을 읽어오지 못하는 경우는 없다.
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
        else //데이터 값이 들어있다면 아래 과정 실행
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
        //상대방 패 보이기
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

        Debug.Log("삭제 전");
        RemoveDatabase(); //상대방 값도 입력되어야 삭제된다. 왜냐하면 GameResult_Enemy()의 
        Debug.Log("삭제 후");//firebase는 키 값 데이터베이스이므로 값을 제거하면
                          //키가 자체적으로 존재할 수 없기 때문에 키도 제거됨.
        rematchBtn_Host.SetActive(true);
        this.gameObject.SetActive(false);


    }
    IEnumerator WaitDB_RPS()//시간이 다 되면 실행하는데 GameResult_Enemy()이 입력되어있는 데이터 값을 가져오는데 성공하면 waitDB_CoroutineTrigger = 1 이 된다.
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
        PlayerPrefs.SetInt("Graceful shutdown", 1); //정상적인 종료시 1 넣음
    }
}
