using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGMPlayer : MonoBehaviour
{
    new AudioSource audio;
    private static bool isLoad = false;
    public static bool soundStop = false;

    private void Awake()
    {
        if (isLoad)
        {
            Destroy(gameObject);
            return;
        }

        if (soundStop)
        {
            isLoad = false;
            soundStop = false;
            Destroy(gameObject);
            return;
        }

        isLoad = true;
        DontDestroyOnLoad(gameObject);
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (soundStop)
        {
            isLoad = false;
            soundStop = false;
            Destroy(gameObject);
            return;
        }
    }

    public void BGMOff()
    {
        audio.Stop();
    }

    public void BGMOn()
    {
        audio.Play();
    }
}
