using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ColliderPourPièce : MonoBehaviour
{
    [SerializeField] ProtoPlayerStats protoPlayerStats;

    public void Start()
    {
        protoPlayerStats = GetComponentInParent<ProtoPlayerStats>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pièce"))
        {
            protoPlayerStats.ResetAddComboAndScore();
            collision.GetComponent<Pièce>().Invoke("DestroyObject", 3f);
        }
    }
}
