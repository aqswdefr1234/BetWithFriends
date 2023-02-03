using UnityEngine;
using UnityEngine.SceneManagement;
public class OfflineSceneBtnScripts : MonoBehaviour
{
    public void RPS_SceneEnter()
    {
        SceneManager.LoadScene("RPS_Offline");
    }
    public void Home_SceneReturn()
    {
        SceneManager.LoadScene("Home");
    }
    public void Gacha_SceneEnter()
    {
        SceneManager.LoadScene("Gacha_Offline");
    }
    public void LadderGame_SceneEnter()
    {
        SceneManager.LoadScene("LadderGame_Offline");
    }
    public void WheelGame_SceneEnter()
    {
        SceneManager.LoadScene("WheelGame_Offline");
    }
}
