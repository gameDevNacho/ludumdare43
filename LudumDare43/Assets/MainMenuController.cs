using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    Animator anim;
    public string gameScene;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Test()
    {
        Debug.Log("hola");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SwitchInstructions()
    {
        anim.SetBool("Instructions", !anim.GetBool("Instructions"));
    }

    public void SwitchCredits()
    {
        anim.SetBool("Credits", !anim.GetBool("Credits"));
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }
   
}
