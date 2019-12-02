using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool inMainMenu = true;
    public bool inOptionsMenu = false;
    public bool currentlyTransitioning = false;
    [SerializeField] MainMenuCamera mainCamera;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //camera transitions
        if (inMainMenu == true && currentlyTransitioning == true)
        {
            animator.SetBool("cameraMain", true);
            animator.SetBool("cameraOptions", false);
        }
        else if (inOptionsMenu == true && currentlyTransitioning == true)
        {
            animator.SetBool("cameraOptions", true);
            animator.SetBool("cameraMain", false);
        }
        else
        {
            animator.SetBool("cameraOptions", false);
            animator.SetBool("cameraMain", false);
        }
    }
}
