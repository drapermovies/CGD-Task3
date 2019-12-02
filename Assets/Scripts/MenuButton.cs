﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

    [SerializeField] MainMenu canvas;
    [SerializeField] MenuController menuController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        universalAnimation();
        individualAnimation();
    }

    private void universalAnimation()
    {

        //transition buttons 
        if (menuController.hasTransitionedOut == true)
        {
            animator.SetBool("transitionedOut", false);
        }
        else
        {
            animator.SetBool("transitionedIn", true);
            canvas.currentlyTransitioning = false;
        }
    }

    private void individualAnimation()
    {
        if (menuController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("pressed", true);
            }
            else if (animator.GetBool("pressed"))
            {
                menuTransitionOut();
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }

    public void menuTransitionOut()
    {
        animatorFunctions.disableOnce = true;
        canvas.currentlyTransitioning = true;
        menuController.hasTransitionedOut = true;
        animator.SetBool("pressed", true);
        menuController.lockedIndex = menuController.index;
    }

    public void menuProgression()
    {

        //following code occurs once the fade out transition animation has ended
        // transitioning from main menu
        if (canvas.inMainMenu)
        {
            switch (menuController.lockedIndex)
            {
                case 0:
                    //FOR AUDIO FRIENDS, this code occurs when player has pressed/clicked 'NEW GAME' (haven't made it go to new scene yet lol)
                    Debug.Log("Play the Game");
                    break;
                case 1:
                    //FOR AUDIO FRIENDS, this code occurs when player has pressed/clicked 'OPTIONS' (less intense sfx, can be reused for the rest of the cases tbh)
                    Debug.Log("Options Menu");
                    canvas.inOptionsMenu = true;
                    canvas.inMainMenu = false;
                    switchMenuDisplay();
                    break;
                case 2:
                    Debug.Log("Exiting Game");
                    Application.Quit();
                    break;
            }
        }
        else if (canvas.inOptionsMenu)
        {
            switch (menuController.lockedIndex)
            {
                case 0:
                    Debug.Log("Options 1");
                    break;
                case 1:
                    Debug.Log("Options 2");
                    break;
                case 2:
                    Debug.Log("Go back to Main Menu");
                    canvas.inMainMenu = true;
                    canvas.inOptionsMenu = false;
                    switchMenuDisplay();
                    break;
            }
        }
        else if (canvas.inEndMenu)
        {
            switch (menuController.lockedIndex)
            {
                case 0:
                    Debug.Log("Play the Game");
                    break;
                case 1:
                    Debug.Log("Go back to Main Menu");
                    canvas.inMainMenu = true;
                    canvas.inEndMenu = false;
                    switchMenuDisplay();
                    break;
                case 2:
                    Debug.Log("Exiting Game");
                    Application.Quit();
                    break;
            }
        }
    }

    private void switchMenuDisplay()
    {
        animator.SetBool("pressed", false);
        animator.SetBool("transitionedOut", true);
        animator.SetBool("transitionedIn", false);
        menuController.hasTransitionedOut = false;
    }

    public void mouseHighlight()
    {
        if (canvas.currentlyTransitioning == false)
        {
            menuController.index = thisIndex;
        }
    }

    public void mouseUnhighlight()
    {
        if (canvas.currentlyTransitioning == false)
        {
            menuController.index = 10;
        }

    }

    public void mouseSelect()
    {
        if (canvas.currentlyTransitioning == false)
        {
            animator.SetBool("pressed", true);
            if (animator.GetBool("pressed"))
            {
                menuTransitionOut();
            }
        }

    }
}
