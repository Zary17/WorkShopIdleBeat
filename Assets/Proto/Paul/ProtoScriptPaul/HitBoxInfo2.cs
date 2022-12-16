using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class HitBoxInfo2 : MonoBehaviour
{
    ProtoPlayerInteraction interact;


    void Start()
    {

        interact = GetComponentInParent<ProtoPlayerInteraction>();
    }
    //Detecte les objets.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Slider"))
        {
            interact.canInteraction = true;
            interact.collectable = collision;
            /*GetComponentInParent<ProtoPlayerInteraction>().canInteractionInt += 1;

            Debug.Log("canInteractionInt + 1");*/
        }
    }

    //Retire les objets.
    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.CompareTag("Enemy") || collision.CompareTag("Slider"))
        {
            interact.canInteraction = false;
            interact.collectable = null;
            interact.ResetCombo();

        Debug.Log("TriggerExit2D"); 
        }


        // A Paul 


       /* GetComponentInParent<ProtoPlayerInteraction>().canInteractionInt -= 1;

        GetComponentInParent<ProtoPlayerInteraction>().canInteraction = false;

        GetComponentInParent<ProtoPlayerInteraction>().collectable = null;


        Debug.Log("TriggerExit2D");

        Debug.Log("canInteractionInt - 1");*/
    }
}
