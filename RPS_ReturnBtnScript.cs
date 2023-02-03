using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Database;

public class RPS_ReturnBtnScript : MonoBehaviour
{
    DatabaseReference myDatabaseRef;
    public TMP_Text createdID;//접속된 아이디를 플레이어 프리팹에 넣어논 후,
    public TMP_Text createdCode;//뒤로가기버튼클릭시와 비정상종료시 해당 id를 가져와 해당되는 데이터를 삭제시키기 위함.
    
    void Start()
    {
        myDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void HostReturnBtnClick()//버튼을 호스트용 조인유저용 두개를 만든다.호스트가 나가면 방이 삭제되고, 조인유저가 나가면 그사람정보만 삭제된다.
    {
        if(createdID.text != "" && createdCode.text != "")
        {
            PlayerPrefs.SetInt("Graceful shutdown", 1); //정상적인 종료시 1 넣음
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
