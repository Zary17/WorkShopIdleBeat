using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtoPlayerInteraction : MonoBehaviour
{
    //------ATTACK-----//
    [Header("Attack")]
    [SerializeField] public bool canInteraction;
    [SerializeField] public int canInteractionInt;
    public bool isOnClick;

    //Collectable qui peut etre ramassé.
    [SerializeField] public Collider2D collectable;

    //
    ProtoPlayer protoPlayer;

    AudioManager _audioManager;

    private void Start()
    {
        protoPlayer = FindObjectOfType<ProtoPlayer>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void Interaction(InputAction.CallbackContext ctx)
    {

        if (!ctx.performed)
        {
            isOnClick = false;

            if (canInteraction && protoPlayer.isEvil)
            {
                activeObject(collectable);
            }
        }

        if (ctx.performed)
        {
            isOnClick = true;
            if (canInteraction && protoPlayer.isEvil)
            {

                activeObject(collectable);

                protoPlayer.gameObject.GetComponent<Animator>().SetTrigger("isAttack");


                //_audioManager.Play("Frappe");

                //GetComponentInParent<Animator>().SetBool("isAttack", true);

                Debug.Log("ObjetDetruit");
            }
            else
            {
                //GetComponentInParent<Animator>().SetBool("isAttack", false);
            }

            Debug.Log("Interaction Perform !");
        }
   
    }

    //Detecte les objets.
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Slider"))
        {
            canInteraction = true;

            //Préscision de quel objet interéagir.
            collectable = collision;
        }

        Debug.Log("TriggerEnter2D");
    }

    //Retire les objets.
    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteraction = false;

        collectable = null;

        ResetCombo();

        Debug.Log("TriggerExit2D");
    }*/

        
    void ComboAndScore(int addCombo)
    {
        GetComponentInParent<ProtoPlayerStats>().AddComboAndScore(addCombo);
    }

    public void ResetCombo()
    {
        GetComponentInParent<ProtoPlayerStats>().ResetAddComboAndScore();
    }

    public void activeObject(Collider2D collectable)
    {
        //Pour activer la fonction de l'objet.
        if (collectable.GetComponent<Fan>())
        {
            collectable.GetComponent<Fan>().activeObject();
        }
        
        //Pour le slider. Si le joueur arrete d'appuyer, il desactive et détrui l'objet. (A TESTER)
        if (collectable.GetComponent<SliderInteraction>())
        {
            collectable.GetComponent<SliderInteraction>().activeObject();

            Debug.Log("Animation activée");
        }

        switch (canInteractionInt)
        {
            case 1:
                ComboAndScore(1);
                Debug.Log("Good");
                break;
            case 2:
                ComboAndScore(2);
                Debug.Log("Perfect");
                break;
        }

        Debug.Log("activeOject");
    }
}
