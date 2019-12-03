using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject endMenu;

    public bool inMainMenu = true;
    public bool inOptionsMenu = false;
    public bool inEndMenu = false;
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
        menuTypeConfiguration();
        cameraTransitions();
    }

    private void menuTypeConfiguration()
    {
        if (inMainMenu)
        {
            mainMenu.gameObject.SetActive(true);
            optionsMenu.gameObject.SetActive(false);
            endMenu.gameObject.SetActive(false);
        }
        else if (inOptionsMenu)
        {
            mainMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(true);
            endMenu.gameObject.SetActive(false);
        }
        else if (inEndMenu)
        {
            mainMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
            endMenu.gameObject.SetActive(true);
        }
    }

    private void cameraTransitions()
    {
        //camera transitions
        if (inOptionsMenu == true && currentlyTransitioning == true)
        {
            animator.SetBool("cameraMain", false);
            animator.SetBool("cameraOptions", true);
        }
        else if (inMainMenu == true && currentlyTransitioning == true)
        {
            animator.SetBool("cameraOptions", false);
            animator.SetBool("cameraMain", true);
        }
        else
        {
            animator.SetBool("cameraOptions", false);
            animator.SetBool("cameraMain", false);
        }
    }
}
