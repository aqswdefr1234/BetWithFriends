using System;//convert 사용 등
using System.Collections;
using System.Collections.Generic; //리스트 사용
using UnityEngine;
using TMPro;
using RPS_ScriptsNameSpace;
using UnityEngine.UI;
//현재 PlayerSelectPanel 오브젝트에 붙어있음.
public class PlayerCreatScript : MonoBehaviour //플레이어 수 확인하고 플레이어 프리팹 복제하는 스크립트. 그리고 각 플레이어가 선택한 가위바위보 값을 활용.
{
    public TMP_Text playerCountText;
    public GameObject playerPrefab;
    public TMP_Text playerOrderText; //플레이어 순서 알려주기 위해
    public GameObject coverImage; //이미지를 비우려고했지만 이미지 넣기 로직을 업데이트 함수내에 만들고 
                                  //다른 씬에서도 사용하고 있어서 바꿀수가없음(using RPS_ScriptsNameSpace)
                                  //그래서 덮개를 씌워 가리는 방법을 사용함.
    private GameObject[] clone;
    private int count;
    
    private int[] playerValue; //자료형을 선언했으면 하위 함수에서 다시 선언하면 안된다.정상작동안함. 
                               //ex, 스타트함수 안에 int playerValue = new int[count] -> playerValue = new int[count]
    private int btnCount; //버튼 눌리는 횟수
    private List<string> loser_List; // 진 사람 리스트
    private List<string> winner_List; // 이긴 사람 리스트
    private List<int> kinds_List; //가위 바위 보 중 낸 종류 집어넣기
    private string losers; // ui텍스트에 리스트 값을 출력하기 위해서
    private string winners;
    //리매치 버튼 모음
    public GameObject drawReMatchBtn; //비길경우 매치 버튼
    public GameObject winnersMatchBtn;//이긴사람 매치 버튼
    public GameObject loserMatchBtn;//진사람 매치 버튼

    //가위바위보 이미지
    public Sprite rockImage;
    public Sprite paperImage;
    public Sprite scissorsImage;

    public GameObject ResultPanel;
    public TMP_Text loserResultText;
    public TMP_Text winnerResultText;
    void Awake() //비활성화 되고 다시 활성화된다고 해도 다시 작동하지 않음 스타트 함수에 넣었더니
                 //원하는데로 작동안함. 작동 순서 Awake -> OnEnable -> Start -> Update 이므로
    {
        PlayerPrefs.SetInt("Result_2vs2", 0); //2대2나오는 경우 리매치할때 다른 루틴으로 돌리기위하여
    }
    void OnEnable()
    {
        winners = "";
        losers = "";
        
        kinds_List = new List<int>();
        loser_List = new List<string>();
        winner_List = new List<string>();
        playerOrderText.text = "Player 1 's Turn";
        btnCount = 0;
        count = Convert.ToInt32(playerCountText.text);
        if (PlayerPrefs.GetInt("Result_2vs2") == 1)
            count = 2;
        playerValue = new int[count];
        clone = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            clone[i] = Instantiate(playerPrefab, this.transform); //child로 복제하기 위해 this.transform을 넣는다.
            clone[i].name = "Player" + (i + 1);
        }
    }
    public void SelectDoneChoice() //여기서 대부분의 기능들이 수행된다.
    {
        if (btnCount < count)
        {
            playerValue[btnCount] = RockPaperScissorsScripts.choiceRPS;
            Debug.Log("value :" + playerValue[btnCount]);
            btnCount += 1;
            coverImage.SetActive(true);
            if (btnCount == count)
            {
                playerOrderText.text = "";
                ResultBattle();
                for (int i = 0; i < count; i++) //마지막 참여자 순서까지 마치면 이미지를 넣는다.
                {
                    if (playerValue[i] == 1)
                        this.transform.Find("Player" + (i + 1)).GetComponent<Image>().sprite = rockImage;
                    else if (playerValue[i] == 2)
                        this.transform.Find("Player" + (i + 1)).GetComponent<Image>().sprite = paperImage;
                    else
                        this.transform.Find("Player" + (i + 1)).GetComponent<Image>().sprite = scissorsImage;
                }
            }
            else
                playerOrderText.text = "Player " + (btnCount + 1).ToString() + " 's Turn";
        }
        else
            playerOrderText.text = "";
    }
    public void RPS_Btn_CoverImageDisabled()
    {
        coverImage.SetActive(false);
    }
    public void ResultBattle()
    {
        if(IsDraw(playerValue) == true)
        {
            playerOrderText.text = "Draw!";
            drawReMatchBtn.SetActive(true);
        }
        else
        {
            WhoIsWinner(playerValue);
            ResultPanel.SetActive(true);
        }
    }
    
    bool IsDraw(int[] exArray)//비기는 경우는 가위-바위-보 가 모두 나오는 경우가 있고, 모두 같은모양을 내는 경우가 있다.
    {
        bool isResultSame = false; //모두 같은모양 냈으면 true 아니면 false

        for(int i = 0; i < exArray.Length - 1; i++) //모양이 전부 같다면 반복문 끝날 때 isResultSame = true 아니면 아래 if문 실행.
        {
            if (exArray[i] == exArray[i + 1])
            {
                isResultSame = true;
            }
            else
            {
                isResultSame = false;
                break;
            }
        }
        if(isResultSame == false) //모양이 다른게 있는 경우 실행
        {
            if (Array.Exists(exArray, x => x == 1) == true)
            {
                if (Array.Exists(exArray, x => x == 2) == true)
                {
                    if (Array.Exists(exArray, x => x == 3) == true)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        else
            return true; //전부 모양이 같은 경우
    }
    
    void WhoIsWinner(int[] exArray) //비기는 경우를 제외했으므로 무조건 두개의 값만 존재한다.
    {
        foreach(int a in exArray) // 낸 모양을 리스트에 넣는다. 이 때 중복값 제거하기 위해 contains사용
        {
            if(kinds_List.Contains(a) == false)
                kinds_List.Add(a);
        }
        kinds_List.Sort(); // 오름 차순 정렬

        if(kinds_List[0] == 1 && kinds_List[1] == 2) // 주먹, 보
        {
            for(int i = 0; i < exArray.Length; i++)
            {
                if (exArray[i] == 1)
                    loser_List.Add("Player" + (i + 1)); // 플레이어는 1부터 시작하므로
                else
                    winner_List.Add("Player" + (i + 1));
            }
        }
        else if (kinds_List[0] == 1 && kinds_List[1] == 3) // 주먹, 가위
        {
            for (int i = 0; i < exArray.Length; i++)
            {
                if (exArray[i] == 3)
                    loser_List.Add("Player" + (i + 1));
                else
                    winner_List.Add("Player" + (i + 1));
            }
        }
        else //(kinds_List[0] == 2 && kinds_List[1] == 3)  보, 가위
        {
            for (int i = 0; i < exArray.Length; i++)
            {
                if (exArray[i] == 2)
                    loser_List.Add("Player" + (i + 1));
                else
                    winner_List.Add("Player" + (i + 1));
            }
        }
        for (int i = 0; i < loser_List.Count; i++) //배열은 Length, 리스트는 Count
        {
            losers = losers + "\n" + loser_List[i];
        }
        for (int i = 0; i < winner_List.Count; i++)
        {
            winners = winners + "\n" + winner_List[i];
        }
        loserResultText.text =  "Lose : " + losers;
        winnerResultText.text = "Win : " + winners;
        if (winner_List.Count == 2 && loser_List.Count == 2) // 2대2가 나오는 경우 버튼을 부수면 안됨
            PlayerPrefs.SetInt("Result_2vs2", 1);
        if (winner_List.Count > 1)
            winnersMatchBtn.SetActive(true);
        if (loser_List.Count > 1)
            loserMatchBtn.SetActive(true);

    }
    /* 각각 매치되는 값
    플레이어 1 playerValue[0]
    플레이어 2 playerValue[1]
    플레이어 3 playerValue[2]
    플레이어 3 playerValue[4] */
    public void DrawReMatchBtn()
    {
        drawReMatchBtn.SetActive(false);
        btnCount = 0; //이것을 초기화 해야 다시 결과 값이 나온다
        playerOrderText.text = "Player 1 's Turn";
    }
    public void WinnersMatchBtn()
    {
        if(PlayerPrefs.GetInt("Result_2vs2") == 1)
        {
            winnersMatchBtn.SetActive(false);
            for (int i = 0; i < count; i++)
                Destroy(clone[i]);
            ResultPanel.SetActive(false);
            playerCountText.text = winner_List.Count.ToString();
            gameObject.SetActive(false);
        }
        else
        {
            winnersMatchBtn.SetActive(false);
            loserMatchBtn.SetActive(false);
            for (int i = 0; i < count; i++)
                Destroy(clone[i]);
            ResultPanel.SetActive(false);
            playerCountText.text = winner_List.Count.ToString();
            gameObject.SetActive(false);
        }
        
    }
    public void LosersMatchBtn()
    {
        if (PlayerPrefs.GetInt("Result_2vs2") == 1)
        {
            loserMatchBtn.SetActive(false);
            for (int i = 0; i < count; i++)
                Destroy(clone[i]);
            ResultPanel.SetActive(false);
            playerCountText.text = loser_List.Count.ToString();
            gameObject.SetActive(false);
        }
        else
        {
            loserMatchBtn.SetActive(false);
            winnersMatchBtn.SetActive(false);
            for (int i = 0; i < count; i++)
                Destroy(clone[i]);
            ResultPanel.SetActive(false);
            playerCountText.text = loser_List.Count.ToString();
            gameObject.SetActive(false);
        }
    }
    
}
