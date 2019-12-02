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

        //debug testing
        if (canvas.currentlyTransitioning == true)
        {
            animator.SetBool("transitionedIn", false);
        }
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
            canvas.currentlyTransitioning = false;
            animator.SetBool("transitionedIn", true);
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
        canvas.currentlyTransitioning = true;
        menuController.hasTransitionedOut = true;
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
                    switchMenuDisplay();
                    canvas.inOptionsMenu = true;
                    canvas.inMainMenu = false;
                    menuController.optionsMenu.gameObject.SetActive(true);
                    menuController.mainMenu.gameObject.SetActive(false);
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
                    switchMenuDisplay();
                    canvas.inMainMenu = true;
                    canvas.inOptionsMenu = false;
                    menuController.mainMenu.gameObject.SetActive(true);
                    menuController.optionsMenu.gameObject.SetActive(false);
                    break;
            }
        }
    }

    private void switchMenuDisplay()
    {
        menuController.hasTransitionedOut = false;
        animator.SetBool("pressed", false);
        animator.SetBool("transitionedOut", true);
        animator.SetBool("transitionedIn", false);
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
