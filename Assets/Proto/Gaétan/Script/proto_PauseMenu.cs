using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class proto_PauseMenu : MonoBehaviour
{

    bool GameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI, OptionUI, CreditUI,inGameUI;
    [SerializeField] GameObject pauseFirstButton, optionsFirstButton, creditFirtButton;
    GameObject lastSelected;

    [SerializeField] proto_Options options;
    bool inOption=false;



    public void Pause()
    {
        GameIsPaused = !GameIsPaused;

        if (GameIsPaused)
        {
            inGameUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);


        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }



    public void Option()
    {
        inOption = true;
        options.UpdateValue();
        pauseMenuUI.SetActive(false);
        OptionUI.gameObject.SetActive(true);
        lastSelected = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);

    }

    public void Credit()
    {
        pauseMenuUI.SetActive(false);
        CreditUI.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(creditFirtButton);
    }

    public void LeaveOption()
    {
        inOption = false;
        EventSystem.current.SetSelectedGameObject(lastSelected);
        OptionUI.SetActive(false);
        CreditUI.SetActive(false);
        pauseMenuUI.SetActive(true);

    }

    public void RetourMenuButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && inOption)
        {
            LeaveOption();
        }
        else
            return;
    }




    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }





}
