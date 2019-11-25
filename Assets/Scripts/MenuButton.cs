using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{

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
                animator.SetBool("pressed", false);
                animatorFunctions.disableOnce = true;
                menuController.hasTransitionedOut = true;
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }
}
