using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Comeca()
    {
        SceneManager.LoadScene(1);
    }

    public void Options() {
        print("OPTIONS");
    }
    public void Restart() {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
