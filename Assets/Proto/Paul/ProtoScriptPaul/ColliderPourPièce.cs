using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPourPièce : MonoBehaviour
{
    ProtoPlayerStats protoPlayerStats;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pièce"))
        {
            protoPlayerStats.GetComponent<ProtoPlayerStats>().ResetAddComboAndScore();
            collision.GetComponent<Pièce>().Invoke("DestroyObject", 3f);
        }
    }
}
