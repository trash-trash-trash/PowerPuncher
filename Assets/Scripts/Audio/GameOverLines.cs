using System.Collections.Generic;
using UnityEngine;

public class GameOverLines : MonoBehaviour
{
    public AudioSource audioSource;

    public List<AudioClip> audioClips = new List<AudioClip>();
    
    void OnEnable()
    {
    int rand = Random.Range(0, audioClips.Count);
    audioSource.clip = audioClips[rand];
    audioSource.Play();
    }
}
