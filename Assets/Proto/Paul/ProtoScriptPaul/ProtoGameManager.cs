using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoGameManager : MonoBehaviour
{
    public static ProtoGameManager Instance = null;

    AudioManager _audioManager;

    proto_LDDeroulant ld;

    //******CheckPoint********\\
    [SerializeField] float _currentTransformCheckPoint;
    float _currentTimeCodeCheckPoint;
    AudioSource _currentClip;

    //Timer
    [SerializeField] float currentTimer;
    bool canCount=false;
    //
    int tryRemaning;


    float speedLD;

    ScrollingBackGround[] allScrollingBackGround;


    //ratio
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();

    }

    void Update()
    {
        if (canCount)
        {
            currentTimer += Time.deltaTime;
        }
    }

    public void GetInfo(float checkTransform, float timeCheck)
    {
        _currentTransformCheckPoint = checkTransform;

        //Ajout du timer 
        currentTimer = 0;
        canCount = true;
    }



    public void Respawn()
    {

        // TP le Player a _currentTransformCheckPoint

        ld.transform.position = new Vector2(_currentTransformCheckPoint,ld.transform.position.y);

        foreach (ScrollingBackGround scroll in allScrollingBackGround)
        {
            scroll.ResetBackGround();
        }

        //Soustraction du pitch avec le timer qui est entre le checkpoint et la mort du joueur.
        _currentClip.pitch = 1;
        _currentClip.time -= currentTimer;

        ld.resetSpeed(speedLD);
    }



    public void OnDeath()
    {
        canCount = false;
        currentTimer += 0.4f;
        allScrollingBackGround = FindObjectsOfType<ScrollingBackGround>();
        // pour set le timer au timer du checkpoint
        _currentClip = _audioManager.CheckPointEffect("Level1");

        tryRemaning -= 1;





        foreach(ScrollingBackGround scroll in allScrollingBackGround)
        {
            scroll.StopBackGround();
        }


        ld = FindObjectOfType<proto_LDDeroulant>();
        //  ici si on veut faire un truc quand elle prend un degat avant de respawn
        speedLD= ld.SendSpeed();
        ld.changeSpeed();







        Invoke("Respawn", 2);
    }



    public void Play(string label)
    {
        _audioManager.Play(label);
    }
}
    