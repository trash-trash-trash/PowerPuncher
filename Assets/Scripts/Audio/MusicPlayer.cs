using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip bass;
    public AudioClip hiHat00;
    public AudioClip kick00;
    public AudioClip drums;
    public AudioClip snare;
    public AudioClip chords;

    public AudioClip song;

    public AudioSource songSource;

    public AudioSource bassSource;
    public AudioSource hiHat00Source;
    public AudioSource kick00Source;
    public AudioSource drumsSource;
    public AudioSource snareSource;
    public AudioSource chordsSource;

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
            { drums, drumsSource },
            { bass, bassSource },
            { chords, chordsSource }
        };
    }

    public void StartSong()
    {
        playingSources.Clear();
        double startTime = AudioSettings.dspTime + 1.0;
        
        foreach (var kvp in audioSources)
        {
            kvp.Value.clip = kvp.Key;
            kvp.Value.loop = true;  
            kvp.Value.PlayScheduled(startTime);
        }

        ShuffleNextInLine();
    }

    void ShuffleNextInLine()
    {
        foreach (var clip in new[] { kick00, hiHat00, snare, bass, chords, drums })
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

    public void BossSong()
    {
        songSource.clip = song;
        songSource.loop = true;
        songSource.PlayScheduled(AudioSettings.dspTime); 
    }
}
