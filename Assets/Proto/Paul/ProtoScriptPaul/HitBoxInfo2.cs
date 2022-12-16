using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxInfo2 : MonoBehaviour
{
    //Detecte les objets.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Slider"))
        {
            GetComponentInParent<ProtoPlayerInteraction>().canInteractionInt += 1;

            Debug.Log("canInteractionInt + 1");
        }
    }

    //Retire les objets.
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponentInParent<ProtoPlayerInteraction>().canInteractionInt -= 1;

        GetComponentInParent<ProtoPlayerInteraction>().canInteraction = false;

        GetComponentInParent<ProtoPlayerInteraction>().collectable = null;

        GetComponentInParent<ProtoPlayerInteraction>().ResetCombo();

        Debug.Log("TriggerExit2D");

        Debug.Log("canInteractionInt - 1");
    }
}
