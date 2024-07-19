using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStandScript : MonoBehaviour
{
    Animator anim = null;
    bool hasPlayedAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasPlayedAnim)
        {
            anim.SetTrigger("Jump");
            hasPlayedAnim = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        hasPlayedAnim = false;
    }
}
