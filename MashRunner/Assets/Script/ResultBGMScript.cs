using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBGMScript : MonoBehaviour
{
    //public AudioSource[] resultFanfare;

    public AudioSource resultFanfare;
    public AudioSource resultBGM;

    void Awake()
    {
        //resultFanfare = GetComponents<AudioSource>();
        resultBGM = GetComponent<AudioSource>();
        resultFanfare = GetComponent<AudioSource>();

        resultFanfare.Play();
        resultBGM.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
