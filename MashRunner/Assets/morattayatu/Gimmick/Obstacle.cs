using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float destroyTime;

    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    //SE
    AudioSource fallSE;
    bool hasPlayedSE = false; //ÉTÉEÉìÉhçƒê∂í«ê’

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        fallSE = GetComponent<AudioSource>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //SE
            if (fallSE != null && !hasPlayedSE)
            {
                fallSE.Play();
                hasPlayedSE = true;
            }

            Invoke("Fall", destroyTime);
        }
    }

    void Fall()
    {
        rb.isKinematic = false;
        boxCollider.enabled = false;
        Destroy(gameObject, 0.5f);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        hasPlayedSE = false;
    }
}
