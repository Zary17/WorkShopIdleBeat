using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxInfo : MonoBehaviour
{
    //Detecte les objets.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Slider"))
        {
            GetComponentInParent<ProtoPlayerInteraction>().canInteractionInt += 1;

            GetComponentInParent<ProtoPlayerInteraction>().canInteraction = true;

            //Préscision de quel objet interéagir.
            GetComponentInParent<ProtoPlayerInteraction>().collectable = collision;

            Debug.Log("canInteractionInt + 1");
        }
    }

    //Retire les objets.
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponentInParent<ProtoPlayerInteraction>().canInteractionInt -= 1;

        GetComponentInParent<ProtoPlayerInteraction>().collectable = null;

        GetComponentInParent<ProtoPlayerInteraction>().ResetCombo();

        Debug.Log("TriggerExit2D");

        Debug.Log("canInteractionInt - 1");
    }
}
