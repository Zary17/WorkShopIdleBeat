using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] Animator vfx;
    public void activeObject()
    {
        //Fonctionne mais besoin de faire jouer l'animation et ensuite d�truire l'objet.
        //GetComponent<Animator>().SetTrigger("takeAttack");
        vfx.SetTrigger("takeAttack");
        Invoke("DestroyObject", 0.1f);

        Debug.Log("Fan activ�");
        //DestroyObject();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
