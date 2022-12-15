using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;

    AudioManager _audioManager;

    //******CheckPoint********\\
    Transform _currentTransformCheckPoint;
    float _currentTimeCodeCheckPoint;
    AudioSource _currentClip;

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



    public void GetInfo(Transform checkTransform,float timeCheck)
    {
        _currentTransformCheckPoint = checkTransform;
        _currentTimeCodeCheckPoint = timeCheck;
    }



    public void Respawn()
    {

        // TP le Player a _currentTransformCheckPoint

        // pour set le timer au timer du checkpoint
       _currentClip= _audioManager.CheckPointEffect("Level1");
        _currentClip.time = _currentTimeCodeCheckPoint;


    }



    public void OnDeath()
    {
        //  ici si on veut faire un truc quand elle prend un degat avant de respawn
    }


}
