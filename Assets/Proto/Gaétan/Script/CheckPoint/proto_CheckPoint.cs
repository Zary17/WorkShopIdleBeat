using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class proto_CheckPoint : MonoBehaviour
{

    [SerializeField] float positionRespawn;

    [SerializeField] float timeCode;

    ProtoGameManager gameManager;

    proto_LDDeroulant ld;



    private void Start()
    {
        gameManager = FindObjectOfType<ProtoGameManager>();
        ld = GetComponentInParent<proto_LDDeroulant>();
            
    }


    public void SendData()
    {
        positionRespawn = ld.transform.position.x;
        gameManager.GetInfo(positionRespawn, timeCode);
        Destroy(GetComponent<BoxCollider2D>());
    }



}
