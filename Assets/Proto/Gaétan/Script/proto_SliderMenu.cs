using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proto_SliderMenu : MonoBehaviour
{

    AudioManager audioManager;





    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SongLevel1()
    {
        audioManager.Play("Level1");
        //audioManager.
    }



}
