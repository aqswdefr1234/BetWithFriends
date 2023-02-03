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
        if (PlayerPrefs.GetInt("Graceful shutdown") == 0) //����������� playerprefs���� ������ ����� �������ֱ����Ͽ�
        {
            ReadRecentData();
        }
        PlayerPrefs.SetInt("Graceful shutdown", 0);
    }
    void ReadRecentData()
    {
        Debug.Log("ReadRecentData() ����");
        myDatabaseRef.Child("RPS_Room").Child(PlayerPrefs.GetString("RPS Recent code")).Child("HostName").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("���ܻ��� �߻�");
                }
                else if (task.IsCompleted)//���� ������ snapshot ���� null�� ����.
                {
                    DataSnapshot snapshot = task.Result; //���������� Ű�� Ű���� ����ִ�.
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
