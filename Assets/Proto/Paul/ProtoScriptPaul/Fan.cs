using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public void activeObject()
    {
        //Fonctionne mais besoin de faire jouer l'animation et ensuite d�truire l'objet.
        GetComponent<Animator>().SetTrigger("takeAttack");
        GetComponentInChildren<Animator>().SetBool("takeAttack", true);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}