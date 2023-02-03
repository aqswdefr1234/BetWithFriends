using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
public class GracefulShutdown : MonoBehaviour
{
    DatabaseReference myDatabaseRef;
    void Awake()
    {
        myDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log(PlayerPrefs.GetInt("Graceful shutdown"));
        Debug.Log(PlayerPrefs.GetString("RPS Recent code"));
        Debug.Log(PlayerPrefs.GetString("RPS Recent name"));
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("Graceful shutdown") == 0) //비정상종료시 playerprefs값을 가져와 빈방을 삭제해주기위하여
        {
            ReadRecentData();
        }
        PlayerPrefs.SetInt("Graceful shutdown", 0);
    }
    void ReadRecentData()
    {
        Debug.Log("ReadRecentData() 시작");
        myDatabaseRef.Child("RPS_Room").Child(PlayerPrefs.GetString("RPS Recent code")).Child("HostName").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("예외사항 발생");
                }
                else if (task.IsCompleted)//값이 없으면 snapshot 값이 null로 나옴.
                {
                    DataSnapshot snapshot = task.Result; //스냅샷에는 키와 키값이 들어있다.
                    Debug.Log(snapshot);
                    if (snapshot != null)
                    {
                        if(snapshot.Value.ToString() == PlayerPrefs.GetString("RPS Recent name"))
                            myDatabaseRef.Child("RPS_Room").Child(PlayerPrefs.GetString("RPS Recent code")).RemoveValueAsync();
                    }
                }
            });
    }
}
