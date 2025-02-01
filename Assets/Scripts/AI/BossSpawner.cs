using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossObj;
    public GameObject bossHPObj;
    
    public GameObject powerGaugeObj;
    public PowerGauge powerGauge;
    
    public PowerupManager powerupManager;

    public AISpawner aiSpawner;

    public MusicPlayer musicPlayer;
    
    //why is boss handling power gauge stuff?
    //rushed lol
    
    void Start()
    {
        powerupManager.AnnounceAllPowerupsAdded += StartSpawnBoss;
    }

    void StartSpawnBoss()
    {
        StartCoroutine(SpawnBoss());
    }

    IEnumerator SpawnBoss()
    {
        powerGauge.MaxLevel();
        musicPlayer.StopAllClips();
        
        yield return new WaitForSeconds(10f);
     
        aiSpawner.NukeAll();
        powerGaugeObj.SetActive(false);
        bossObj.SetActive(true);
        bossHPObj.SetActive(true);
        aiSpawner.SpawnBoss();
        
        yield return new WaitForSeconds(10f);
        musicPlayer.BossSong();
    }

    void Disable()
    {
        powerupManager.AnnounceAllPowerupsAdded -= StartSpawnBoss;
    }
}
