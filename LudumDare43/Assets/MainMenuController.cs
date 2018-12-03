using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    Animator anim;

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

    public void ToInstructions()
    {
        anim.Play("anim_MainMenu_Slide");
    }
}
