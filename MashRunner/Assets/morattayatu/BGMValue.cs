using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMValue : MonoBehaviour
{
    new AudioSource audio;
    // Start is called before the first frame update
    void Awake()
    {
        audio = gameObject.GetComponent<AudioSource>();
        audio.volume = GameSoundValue.BGMValue;
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = GameSoundValue.BGMValue;
    }
}
