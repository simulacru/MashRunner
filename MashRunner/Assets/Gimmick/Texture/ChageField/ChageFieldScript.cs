using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChageFieldScript : MonoBehaviour
{
    Animator anim = null;
    int onTriggerNumber;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        onTriggerNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerNumber++;
        anim.SetInteger("Chage Anim", onTriggerNumber);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerNumber--;
        if(onTriggerNumber < 0)
        {
            onTriggerNumber = 0;
        }
        anim.SetInteger("Chage Anim", onTriggerNumber);
    }
}
