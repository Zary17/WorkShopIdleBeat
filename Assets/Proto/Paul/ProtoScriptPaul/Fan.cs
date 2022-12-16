using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public void activeObject()
    {
        //Fonctionne mais besoin de faire jouer l'animation et ensuite détruire l'objet.
        GetComponent<Animator>().SetTrigger("takeAttack");
        GetComponentInChildren<Animator>().SetTrigger("takeAttack");

        Debug.Log("Fan activé");
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
