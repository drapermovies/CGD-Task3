using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public bool inMainMenu = true;
    public bool inOptionsMenu = false;
    public bool currentlyTransitioning = false;
    [SerializeField] MainMenuCamera mainCamera;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (inMainMenu)
        {
            optionsMenu.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inMainMenu)
        {
            optionsMenu.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
        }
        else if (inOptionsMenu)
        {
            mainMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(true);
        }
        //camera transitions
        if (inMainMenu == true && currentlyTransitioning == true)
        {
            animator.SetBool("cameraMain", false);
            animator.SetBool("cameraOptions", true);
        }
        else if (inOptionsMenu == true && currentlyTransitioning == true)
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
