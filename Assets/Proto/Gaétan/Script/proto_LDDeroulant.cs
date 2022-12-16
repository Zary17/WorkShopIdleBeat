using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.InputSystem;

public class proto_LDDeroulant : MonoBehaviour
{

    [SerializeField] float speed;

    [SerializeField] AudioSource source;
    [SerializeField] float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
        timer = source.time;
    }

    public void GiveTime(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log(source.time);
            source.time = 142.592f;
            transform.position = new Vector3(-1436.834f, transform.position.y,transform.position.z);
        }
    }

    public float SendSpeed()
    {
        return speed;
    }

    public void changeSpeed()
    {
        speed = 0;
    }
    
    public void resetSpeed(float backUp)
    {
        speed = backUp;
    }

    


}
