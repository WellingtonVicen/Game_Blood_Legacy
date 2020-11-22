using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    private bool loadingScene, inCommands;
    public Animator canvasAnim;
    public Image fade;
    private float fadeTransparency;
    private int targetScene;
    public bool pressAnyKeyScene;

    void Start()
    {

        fadeTransparency = 1;
        Cursor.visible = true;

    }

    void Update()
    {

        if (fade != null)
        {

            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fadeTransparency);

            if (loadingScene)
            {

                fadeTransparency += 0.3f * Time.deltaTime;

                if (fadeTransparency > 1)
                {

                    SceneManager.LoadScene(targetScene);

                }

            }
            else if (!loadingScene && fadeTransparency > 0f)
            {

                fadeTransparency -= 0.5f * Time.deltaTime;

            }

        }
        else
        {

            if (loadingScene)
            {

                SceneManager.LoadScene(targetScene);

            }

        }

        if (canvasAnim != null)
        {

            canvasAnim.SetBool("Started", loadingScene);
            canvasAnim.SetBool("InCommands", inCommands);
        }

        if (inCommands && Input.anyKeyDown)
        {

            inCommands = false;

        }

        if (pressAnyKeyScene && Input.anyKeyDown)
        {

            Voltar();

        }

    }

    public void Comeca()
    {

        targetScene = 1;
        loadingScene = true;
    }

    public void Options()
    {
        inCommands = true;
    }
    public void Restart()
    {
        GerennciadorArmas.instance.player.isDead = false;
        GerennciadorArmas.instance.player.possuiChave = false;
        SceneManager.LoadScene(0);
    }

    public void Voltar()
    {

        targetScene = 0;
        loadingScene = true;

    }

    public void Quit()
    {
        Application.Quit();
    }
}
