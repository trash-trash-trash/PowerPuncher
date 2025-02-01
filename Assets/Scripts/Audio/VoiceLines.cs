using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLines : MonoBehaviour
{
   public AudioSource audioSource;
   
   public AudioClip titleClip;
   
   public List<AudioClip> powerUpClips = new List<AudioClip>();
   
   public List<AudioClip> powerUpEvilClips = new List<AudioClip>();
   
   public List<AudioClip> takeDamageClips = new List<AudioClip>();
   
   public List<AudioClip> allPowerupClips = new List<AudioClip>();

   public PlayerHP HP;

   public PowerupManager powerupManager;

   public bool playing = false;

   void Start()
   {
      foreach (AudioClip poClip in powerUpClips)
      {
         allPowerupClips.Add(poClip);
      }

      foreach (AudioClip poClip in powerUpEvilClips)
      {
         allPowerupClips.Add(poClip);
      }
      
      HP.AnnounceTookDamage += PlayDamageClip;
      powerupManager.AnnouncePowerup += AddPowerup;
      
      PlayTitleClip();
   }

   private void AddPowerup(Powerup obj)
   {  
      if (playing)
         return;
      List<AudioClip> clips;
      if (powerupManager.powerupCounter < 20)
         clips = powerUpClips;
      else
         clips = allPowerupClips;
      
      int rand = Random.Range(0, clips.Count);
      audioSource.PlayOneShot(clips[rand]);
      StartCoroutine(PlayClip(clips[rand]));
   }

   private void PlayDamageClip()
   {
      if (playing)
         return;
      int random = Random.Range(0, takeDamageClips.Count);
      audioSource.PlayOneShot(takeDamageClips[random]);
      StartCoroutine(PlayClip(takeDamageClips[random]));
   }

   public void PlayTitleClip()
   {
      audioSource.PlayOneShot(titleClip);
   }

   IEnumerator PlayClip(AudioClip clip)
   {
      playing = true;
      yield return new WaitForSeconds(clip.length);
      playing = false;
   }
}
