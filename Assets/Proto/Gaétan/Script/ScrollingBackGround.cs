using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScrollingBackGround : MonoBehaviour
{
    Material mat;
    Vector2 offset;

    [SerializeField] float xVelocity, yVelocity;

    bool isEvil=true;
    [SerializeField] Sprite texGood;
    [SerializeField] Sprite texEvil;

    SpriteRenderer spRenderer;

    float corbeille;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        spRenderer = gameObject.GetComponent<SpriteRenderer>() ;
        //Invoke("Switch", 3);
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2(xVelocity, yVelocity);
        mat.mainTextureOffset += offset * Time.deltaTime;
    }



    public void ChangeBackGround(bool playerIsEvil)
    {
        isEvil = playerIsEvil;
        if (isEvil)
        {
            spRenderer.sprite = texEvil;
        }
        else
        {
            spRenderer.sprite = texGood;

        }
    }


    public void StopBackGround()
    {
        corbeille = xVelocity;
        xVelocity = 0;
    }

    public void ResetBackGround()
    {
        xVelocity = corbeille;
    }

}





