using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip bass;
    public AudioClip hiHat00;
    public AudioClip hiHat01;
    public AudioClip kick00;
    public AudioClip kick01;
    public AudioClip snare;
    public AudioClip melody;

    public AudioSource bassSource;
    public AudioSource hiHat00Source;
    public AudioSource hiHat01Source;
    public AudioSource kick00Source;
    public AudioSource kick01Source;
    public AudioSource snareSource;
    public AudioSource melodySource;

    public Dictionary<AudioClip, AudioSource> audioSources;
    private List<AudioSource> playingSources = new List<AudioSource>();

    public float increaseTime;

    void Start()
    {
        audioSources = new Dictionary<AudioClip, AudioSource>()
        {
            { kick00, kick00Source },
            { hiHat00, hiHat00Source },
            { snare, snareSource },
            { kick01, kick01Source },
            { bass, bassSource },
            { hiHat01, hiHat01Source },
            { melody, melodySource }
        };

        foreach (KeyValuePair<AudioClip, AudioSource> kvp in audioSources)
        {
            kvp.Value.clip = kvp.Key;
        }
        
        //start all clips playing from their individual sources at once (volume set to 0)
        
        StartAllClips();
        StartSong();
    }

    void StartSong()
    {
        playingSources.Clear();
        ShuffleNextInLine();
    }

    void ShuffleNextInLine()
    {
        foreach (var clip in new[] { kick00, hiHat00,  bass, kick01, snare, hiHat01 })
        {
            if (audioSources.TryGetValue(clip, out AudioSource source) && !playingSources.Contains(source))
            {
                playingSources.Add(source);
                StartCoroutine(IncreaseVolume(source));
                break;
            }
        }
    }

    IEnumerator IncreaseVolume(AudioSource source)
    {
        source.volume = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < increaseTime)
        {
            source.volume = Mathf.Lerp(0f, 1f, elapsedTime / increaseTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        source.volume = 1f;
        ShuffleNextInLine();
    }

    public void StopAllClips()
    {
        foreach (var source in audioSources.Values)
        {
            source.Stop();
        }
    }

    void StartAllClips()
    {
        foreach (var source in audioSources.Values)
        {
            source.Play();
        }
    }

    public void BossSong()
    {
        foreach (var source in audioSources.Values)
        {
            source.volume = 1;
            source.Play();
        }
    }
}
