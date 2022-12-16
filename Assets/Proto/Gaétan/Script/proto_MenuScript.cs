using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class proto_MenuScript : MonoBehaviour
{

    [SerializeField] GameObject Options;
    [SerializeField] GameObject Menu;




    [SerializeField] GameObject menuFirstButton, optionsFirstButton;

    [SerializeField] GameObject splashcreen;

    GameObject lastSelected;

    bool inOption;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayButton()
    {
        audioManager.ChangeScene("Menu",2,0);
        SceneManager.LoadScene(1);
        
        // transition + changement de scene
    }



    //Code pour afficher les options.
    public void boutonOption()
    {
        Menu.SetActive(false);
        Options.SetActive(true);
        inOption = true;


        lastSelected = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);

    }


    public void Quit()
    {
        Application.Quit();
    }

    //Code pour le retour menu.
    public void Retourmenu()
    {

        inOption = false;
        Menu.SetActive(true);
        Options.SetActive(false);

        EventSystem.current.SetSelectedGameObject(lastSelected);

    }


    public void RetourMenuButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && inOption)
        {
            Retourmenu();
        }
        else
            return;
    }


    public void CreditGame()
    {
        //SceneManager.LoadScene("Credit2");
        inOption = true;
    }


    public void SplashScreen()
    {
        splashcreen.SetActive(false);
    }



}
