using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHomeOfflineScene : MonoBehaviour
{
    public void FromLadderOfflineToHomeOffline()
    {
        SceneManager.LoadScene("Home_Offline");
    }
}
