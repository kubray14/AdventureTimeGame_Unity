using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField]
    Animator flag;

    public bool degdiMi = false;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        degdiMi = Character.degdiMi;
        if (degdiMi && Character.score == 10)
        {
            flag.SetBool("degdiMi",degdiMi);
        }
    }


   
} 
