using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] Animator vfx;
    [SerializeField] GameObject Parent;
    public void activeObject()
    {
        //Fonctionne mais besoin de faire jouer l'animation et ensuite détruire l'objet.
        GetComponent<Animator>().SetTrigger("takeAttack");
        vfx.SetTrigger("takeAttack");
    }

    public void DestroyObject()
    {
        Destroy(Parent);

        Debug.Log("Fan Destroy");
    }
}
