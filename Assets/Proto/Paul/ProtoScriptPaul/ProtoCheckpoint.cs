using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoCheckpoint : MonoBehaviour
{
    public void PlayAnimation()
    {
        GetComponent<Animator>().SetTrigger("takeCheckpoint");
    }
}
