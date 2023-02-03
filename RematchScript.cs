using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using TMPro;

public class RematchScript : MonoBehaviour
{
    DatabaseReference myDatabaseRef;
    public GameObject hostScriptBox; //리매치 버튼을 누르면 각각 해당되는 오브젝트가 활성화 된다. battle 스크립트는 start대신 onEnable함수를 넣었기 때문에 활성화 될때마다 다시 작동된다.
    public GameObject joinScriptBox;

    public GameObject hostRematchBtn;
    public GameObject joinRematchBtn;

    public TMP_Text roomCode; //방 정보를 가져오기 위하여
    void Start()
    {
        myDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void Rematch_HostActive()
    {
        hostScriptBox.SetActive(true);
        hostRematchBtn.SetActive(false);
    }
    public void Rematch_JoinActive()
    {
        joinScriptBox.SetActive(true);
        joinRematchBtn.SetActive(false);
        myDatabaseRef.Child("RPS_Room").Child(roomCode.text).Child("State").RemoveValueAsync(); //호스트의 스타트 버튼은 room code의 자식이 2일때 나타나므로 조인유저가 리매치버튼을 눌렀을때
    }                                                                                           //삭제되게 함으로써 그때서야 비로소 스타트 버튼이 활성화 되도록함.
}
