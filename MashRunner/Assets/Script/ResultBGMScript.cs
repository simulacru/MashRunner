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
        resultBGM = GetComponent<AudioSource>();
        resultFanfare = GetComponent<AudioSource>();

        resultFanfare.Play();
        resultBGM.Play();
    }
}
