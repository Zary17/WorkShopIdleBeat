using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class proto_UI : MonoBehaviour
{


    [SerializeField] RectTransform boutQuiBouge;
    [SerializeField] Image fillarea;

    bool isEvil;

    [SerializeField] Sprite fonduGood;
    [SerializeField] Sprite fonduEvil;
    [SerializeField] Sprite progressGood;
    [SerializeField] Sprite progressEvil;

    [SerializeField] Image Fondu;

    [SerializeField] Image CoeurVide;
    AudioManager _audioManager;
    int currentScene;
    // Start is called before the first frame update
    void Start()
    {
        fillarea.fillAmount = 0;
        boutQuiBouge.localPosition = new Vector2(fillarea.transform.localPosition.x      , 0);

        currentScene = SceneManager.GetActiveScene().buildIndex;
        _audioManager = FindObjectOfType<AudioManager>();
        _audioManager.Play("Level" + currentScene);
        _audioManager.Play("Ambiance");
    }

    // Update is called once per frame
    void Update()
    {

        fillarea.fillAmount += 0.00515f * Time.deltaTime;
        boutQuiBouge.localPosition = new Vector2(fillarea.transform.localPosition.x + (450 * fillarea.fillAmount),0);




    }




    public void ChangeUI(bool isEvilPlayer)
    {
        isEvil = isEvilPlayer;
        if (isEvil)
        {
            Fondu.sprite = fonduEvil;
            fillarea.sprite = progressEvil;
        }
        else
        {
            Fondu.sprite = fonduGood;
            fillarea.sprite = progressGood;
        }
    }



    public void TakeDamage()
    {
        CoeurVide.fillAmount += 1/3f ;
    }






}
