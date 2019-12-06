using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCharacter : MonoBehaviour
{
    static Animator anim;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    [SerializeField] private bool isJumping;
    [SerializeField] private KeyCode jumpKey;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0,0,translation);
        transform.Rotate(0,rotation,0);

        

        

        if(translation != 0)
        {
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsIdle", false);
        }
        else
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsIdle", true);
        }
    }
}



