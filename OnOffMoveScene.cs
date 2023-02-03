using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OnOffMoveScene : MonoBehaviour
{
    public TMP_Text onOffText;

    public void OfflineScene()
    {
        SceneManager.LoadScene("Home_Offline");
    }
    public void OnlineScene()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            onOffText.text = "Check network!";
        else
            SceneManager.LoadScene("Home_Online");
    }
}
