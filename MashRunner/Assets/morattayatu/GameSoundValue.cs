using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSoundValue : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SESlider;
    public static float BGMValue = 0.2f;
    public static float SEValue = 0.3f;

    //SE
    AudioSource audioSource;
    public AudioClip slideSE;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        BGMSlider.value = BGMValue * 100.0f;
        SESlider.value = SEValue * 100.0f;
    }

    public void BGM(Slider BGMSlider)
    {
        BGMValue = BGMSlider.value / 100.0f;
    }

    public void SE(Slider SESlider)
    {
        SEValue = SESlider.value / 100.0f;

        //SEスライダーを動かすとSEを鳴らす
        audioSource.PlayOneShot(slideSE);
    }
}