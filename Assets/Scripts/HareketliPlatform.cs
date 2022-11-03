using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HareketliPlatform : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D Rb;

    [SerializeField]  float a,b,speed;
    Vector2 move;
    Vector2 move2;

    float vertical = 1f;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        move = Rb.position + new Vector2(0, a);
        move2 = Rb.position - new Vector2(0, b);
    }
    private void FixedUpdate()
    {
        
        //print(Rb.position);
        if (Rb.position.y >= move.y || Rb.position.y <= move2.y)
        {
            vertical *= (-1);
        }
        Rb.velocity = speed * Time.deltaTime * new Vector2(0, vertical);

    }  
} 

