using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOnMenu : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void Intialize()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            return;
        }
        SceneManager.LoadScene("MainMenu");
    }
}
