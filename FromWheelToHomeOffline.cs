using UnityEngine;
using UnityEngine.SceneManagement;

public class FromWheelToHomeOffline : MonoBehaviour
{
    public void ReturnHomeOffline()
    {
        SceneManager.LoadScene("Home_Offline");
    }
}
