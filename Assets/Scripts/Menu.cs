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

    public void Options()
    {
        SceneManager.LoadScene(23);
    }
    public void Restart()
    {
        GerennciadorArmas.instance.player.isDead = false;
        GerennciadorArmas.instance.player.possuiChave = false;
        SceneManager.LoadScene(0);
    }

    public void Voltar()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
