using System;//convert ��� ��
using System.Collections;
using System.Collections.Generic; //����Ʈ ���
using UnityEngine;
using TMPro;
using RPS_ScriptsNameSpace;
using UnityEngine.UI;
//���� PlayerSelectPanel ������Ʈ�� �پ�����.
public class PlayerCreatScript : MonoBehaviour //�÷��̾� �� Ȯ���ϰ� �÷��̾� ������ �����ϴ� ��ũ��Ʈ. �׸��� �� �÷��̾ ������ ���������� ���� Ȱ��.
{
    public TMP_Text playerCountText;
    public GameObject playerPrefab;
    public TMP_Text playerOrderText; //�÷��̾� ���� �˷��ֱ� ����
    public GameObject coverImage; //�̹����� ������������ �̹��� �ֱ� ������ ������Ʈ �Լ����� ����� 
                                  //�ٸ� �������� ����ϰ� �־ �ٲܼ�������(using RPS_ScriptsNameSpace)
                                  //�׷��� ������ ���� ������ ����� �����.
    private GameObject[] clone;
    private int count;
    
    private int[] playerValue; //�ڷ����� ���������� ���� �Լ����� �ٽ� �����ϸ� �ȵȴ�.�����۵�����. 
                               //ex, ��ŸƮ�Լ� �ȿ� int playerValue = new int[count] -> playerValue = new int[count]
    private int btnCount; //��ư ������ Ƚ��
    private List<string> loser_List; // �� ��� ����Ʈ
    private List<string> winner_List; // �̱� ��� ����Ʈ
    private List<int> kinds_List; //���� ���� �� �� �� ���� ����ֱ�
    private string losers; // ui�ؽ�Ʈ�� ����Ʈ ���� ����ϱ� ���ؼ�
    private string winners;
    //����ġ ��ư ����
    public GameObject drawReMatchBtn; //����� ��ġ ��ư
    public GameObject winnersMatchBtn;//�̱��� ��ġ ��ư
    public GameObject loserMatchBtn;//����� ��ġ ��ư

    //���������� �̹���
    public Sprite rockImage;
    public Sprite paperImage;
    public Sprite scissorsImage;

    public GameObject ResultPanel;
    public TMP_Text loserResultText;
    public TMP_Text winnerResultText;
    void Awake() //��Ȱ��ȭ �ǰ� �ٽ� Ȱ��ȭ�ȴٰ� �ص� �ٽ� �۵����� ���� ��ŸƮ �Լ��� �־�����
                 //���ϴµ��� �۵�����. �۵� ���� Awake -> OnEnable -> Start -> Update �̹Ƿ�
    {
        PlayerPrefs.SetInt("Result_2vs2", 0); //2��2������ ��� ����ġ�Ҷ� �ٸ� ��ƾ���� ���������Ͽ�
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
            clone[i] = Instantiate(playerPrefab, this.transform); //child�� �����ϱ� ���� this.transform�� �ִ´�.
            clone[i].name = "Player" + (i + 1);
        }
    }
    public void SelectDoneChoice() //���⼭ ��κ��� ��ɵ��� ����ȴ�.
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
                for (int i = 0; i < count; i++) //������ ������ �������� ��ġ�� �̹����� �ִ´�.
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
    
    bool IsDraw(int[] exArray)//���� ���� ����-����-�� �� ��� ������ ��찡 �ְ�, ��� ��������� ���� ��찡 �ִ�.
    {
        bool isResultSame = false; //��� ������� ������ true �ƴϸ� false

        for(int i = 0; i < exArray.Length - 1; i++) //����� ���� ���ٸ� �ݺ��� ���� �� isResultSame = true �ƴϸ� �Ʒ� if�� ����.
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
        if(isResultSame == false) //����� �ٸ��� �ִ� ��� ����
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
            return true; //���� ����� ���� ���
    }
    
    void WhoIsWinner(int[] exArray) //���� ��츦 ���������Ƿ� ������ �ΰ��� ���� �����Ѵ�.
    {
        foreach(int a in exArray) // �� ����� ����Ʈ�� �ִ´�. �� �� �ߺ��� �����ϱ� ���� contains���
        {
            if(kinds_List.Contains(a) == false)
                kinds_List.Add(a);
        }
        kinds_List.Sort(); // ���� ���� ����

        if(kinds_List[0] == 1 && kinds_List[1] == 2) // �ָ�, ��
        {
            for(int i = 0; i < exArray.Length; i++)
            {
                if (exArray[i] == 1)
                    loser_List.Add("Player" + (i + 1)); // �÷��̾�� 1���� �����ϹǷ�
                else
                    winner_List.Add("Player" + (i + 1));
            }
        }
        else if (kinds_List[0] == 1 && kinds_List[1] == 3) // �ָ�, ����
        {
            for (int i = 0; i < exArray.Length; i++)
            {
                if (exArray[i] == 3)
                    loser_List.Add("Player" + (i + 1));
                else
                    winner_List.Add("Player" + (i + 1));
            }
        }
        else //(kinds_List[0] == 2 && kinds_List[1] == 3)  ��, ����
        {
            for (int i = 0; i < exArray.Length; i++)
            {
                if (exArray[i] == 2)
                    loser_List.Add("Player" + (i + 1));
                else
                    winner_List.Add("Player" + (i + 1));
            }
        }
        for (int i = 0; i < loser_List.Count; i++) //�迭�� Length, ����Ʈ�� Count
        {
            losers = losers + "\n" + loser_List[i];
        }
        for (int i = 0; i < winner_List.Count; i++)
        {
            winners = winners + "\n" + winner_List[i];
        }
        loserResultText.text =  "Lose : " + losers;
        winnerResultText.text = "Win : " + winners;
        if (winner_List.Count == 2 && loser_List.Count == 2) // 2��2�� ������ ��� ��ư�� �μ��� �ȵ�
            PlayerPrefs.SetInt("Result_2vs2", 1);
        if (winner_List.Count > 1)
            winnersMatchBtn.SetActive(true);
        if (loser_List.Count > 1)
            loserMatchBtn.SetActive(true);

    }
    /* ���� ��ġ�Ǵ� ��
    �÷��̾� 1 playerValue[0]
    �÷��̾� 2 playerValue[1]
    �÷��̾� 3 playerValue[2]
    �÷��̾� 3 playerValue[4] */
    public void DrawReMatchBtn()
    {
        drawReMatchBtn.SetActive(false);
        btnCount = 0; //�̰��� �ʱ�ȭ �ؾ� �ٽ� ��� ���� ���´�
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
