using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Database;

public class RPS_ReturnBtnScript : MonoBehaviour
{
    DatabaseReference myDatabaseRef;
    public TMP_Text createdID;//���ӵ� ���̵� �÷��̾� �����տ� �־�� ��,
    public TMP_Text createdCode;//�ڷΰ����ưŬ���ÿ� ����������� �ش� id�� ������ �ش�Ǵ� �����͸� ������Ű�� ����.
    
    void Start()
    {
        myDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void HostReturnBtnClick()//��ư�� ȣ��Ʈ�� ���������� �ΰ��� �����.ȣ��Ʈ�� ������ ���� �����ǰ�, ���������� ������ �׻�������� �����ȴ�.
    {
        if(createdID.text != "" && createdCode.text != "")
        {
            PlayerPrefs.SetInt("Graceful shutdown", 1); //�������� ����� 1 ����
            myDatabaseRef.Child("RPS_Room").Child(createdCode.text).RemoveValueAsync();
        }
        SceneManager.LoadScene("Home_Online");
    }
    public void JoinUserReturnBtnClick()
    {
        if (createdID.text != "" && createdCode.text != "")
        {
            myDatabaseRef.Child("RPS_Room").Child(createdCode.text).Child("JoinUserName").RemoveValueAsync();
            myDatabaseRef.Child("RPS_Room").Child(createdCode.text).Child("State").RemoveValueAsync();
        }
        SceneManager.LoadScene("Home_Online");
    }
    public void BeforeEnterRoomReturnBtn()
    {
        SceneManager.LoadScene("Home_Online");
    }
}
