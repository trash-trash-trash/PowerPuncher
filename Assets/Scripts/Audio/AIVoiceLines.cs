using System.Collections.Generic;
using UnityEngine;

public class AIVoiceLines : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();

    public AIHP hp;

    public void OnEnable()
    {
        hp.AnnounceHP += PlayClip;
    }

    public void PlayClip(HealthData hpData)
    {
        if (!hpData.isAlive)
        {
            int rand = Random.Range(0, audioClips.Count);
            audioSource.clip = audioClips[rand];
            audioSource.Play();
        }
    }

    void OnDisable()
    {
        hp.AnnounceHP -= PlayClip;
    }
}
