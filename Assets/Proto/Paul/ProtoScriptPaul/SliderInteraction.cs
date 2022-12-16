using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderInteraction : MonoBehaviour
{
    //
    [SerializeField] float timer;
    float currentTimer;
    bool canStartTimer = false;

    //
    ProtoPlayerStats protoPlayerStats;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<ProtoPlayerInteraction>().isOnClick == false)
        {
            desactiveObject();
        }
    }
    public void activeObject()
    {
        GetComponent<Animator>().SetBool("isOnClick", true);

        currentTimer = timer;
    }

    private void FixedUpdate()
    {
        if (canStartTimer)
        {
            if (currentTimer > 0)
            {
                currentTimer -= Time.fixedDeltaTime;
            }
            else
            {
                currentTimer = timer;

                //Ajouter le combo tous les X temps
                protoPlayerStats.GetComponent<ProtoPlayerStats>().AddComboAndScore(1);
            }
        }
    }

    void EndAnimation()
    {
        canStartTimer = false;
    }

    void desactiveObject()
    {
        GetComponent<Animator>().SetBool("isOnClick", false);
        protoPlayerStats.GetComponent<ProtoPlayerStats>().ResetAddComboAndScore();
        Destroy(gameObject);
    }
}
