using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HareketliPlatform : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D Rb;
 
    Vector2 move;
    Vector2 move2;

   public float vertical = 0.5f;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        move = Rb.position + new Vector2(0, 1);
        move2 = Rb.position - new Vector2(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
        //print(Rb.position);
        if (Rb.position == move || Rb.position == move2)
        {
            vertical = vertical*(-1);
        }
        Rb.velocity = new Vector2(0, vertical);

    }  
} 

