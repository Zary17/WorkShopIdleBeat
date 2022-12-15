using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pièce : MonoBehaviour
{
    public void activeObject()
    {
        GetComponent<Animator>().SetTrigger("takePièce");
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
