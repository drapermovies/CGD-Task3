using System.Collections;
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
        //transition buttons in
        if (menuController.hasTransitionedIn == false)
        {
            animator.SetBool("transitionedIn", true);
            menuController.currentlyTransitioning = false;
        }

        //transition buttons out
        if (menuController.hasTransitionedOut == true)
        {
            animator.SetBool("transitionedOut", false);
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
        animator.SetBool("pressed", true);
        animatorFunctions.disableOnce = true;
        menuController.hasTransitionedOut = true;
        menuController.currentlyTransitioning = true;
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
                    Debug.Log("Play the Game");
                    break;
                case 1:
                    Debug.Log("Options Menu");
                    animator.SetBool("transitionedIn", false);
                    animator.SetBool("transitionedOut", true);
                    animator.SetBool("pressed", false);
                    menuController.hasTransitionedOut = false;
                    canvas.inOptionsMenu = true;
                    canvas.inMainMenu = false;
                    menuController.mainMenu.gameObject.SetActive(false);
                    menuController.optionsMenu.gameObject.SetActive(true);
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
                    animator.SetBool("transitionedIn", false);
                    animator.SetBool("transitionedOut", true);
                    animator.SetBool("pressed", false);
                    menuController.hasTransitionedOut = false;
                    canvas.inMainMenu = true;
                    canvas.inOptionsMenu = false;
                    menuController.optionsMenu.gameObject.SetActive(false);
                    menuController.mainMenu.gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void mouseHighlight()
    {
        menuController.index = thisIndex;
    }

    public void mouseUnhighlight()
    {
        if (menuController.currentlyTransitioning == false)
        {
            menuController.index = 10;
        }

    }

    public void mouseSelect()
    {
        animator.SetBool("pressed", true);
        if (animator.GetBool("pressed"))
        {
            menuTransitionOut();
        }
    }


}
