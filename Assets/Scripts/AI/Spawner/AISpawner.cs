using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
   public Transform centerTransform;
   public Transform playerTransform;
   public GameObject AIPrefab;
   public GameObject HPPrefab;
   
   public float minSpawnRadius;
   public float maxSpawnRadius;

   public List<GameObject> pooledAIObjects = new List<GameObject>();
   public int poolSize = 10;
   
   public List<GameObject> pooledHPObjects = new List<GameObject>();

   private float yOffset = 100;
   
   public int numberToSpawn=0;

   public int waveCount = 1;

   private bool spawningLevelOne = false;
   
   void Start()
   {
      for (int i = 0; i < poolSize; i++)
      {
         GameObject pooledObj = Instantiate(AIPrefab);
         pooledObj.SetActive(false);
         pooledAIObjects.Add(pooledObj);
         pooledObj.transform.parent = transform;
      }
      spawningLevelOne = true;
      StartCoroutine(SpawnLoop());
   }

   public IEnumerator SpawnLoop()
   {
      while (spawningLevelOne)
      {
         numberToSpawn++;
         if(numberToSpawn % 5 == 0)
            waveCount++;
         
         for (int i = 0; i < numberToSpawn; i++)
         {
            SpawnInCircle();
         }

         yield return new WaitForSeconds(waveCount);
      }
   }

   public void SpawnInCircle()
   {
      //find random angle from centre
      float angle = Random.Range(0f, Mathf.PI * 2f);
      float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
      
      Vector2 randomPosition = new Vector2(
         Mathf.Cos(angle) * radius,
         Mathf.Sin(angle) * radius
      );

      float randomDistance = Random.Range(100f, 500f);

      Vector3 spawnPosition = new Vector3(
         centerTransform.position.x + randomPosition.x,
         centerTransform.position.y + yOffset + randomDistance * waveCount,
         centerTransform.position.z + randomPosition.y
      );

      bool chance = OneInTenChance();

      if (!chance)
      {
         GameObject spawnedObj = GetPooledAIObject();

         if (spawnedObj != null)
         {
            spawnedObj.transform.position = spawnPosition;
            spawnedObj.SetActive(true);

            AIBrain brain = spawnedObj.GetComponent<AIBrain>();
            brain.playerTransform = playerTransform;
            brain.waveCount = waveCount;
            brain.ChangeState(AIStates.GrowShrink);
         }
      }
      else
      {
         GameObject spawnedObj = GetPooledHPObject();
         if (spawnedObj != null)
         {
            spawnedObj.transform.position = spawnPosition;
            spawnedObj.SetActive(true);
            
            MoveToGround moveToGround = spawnedObj.GetComponent<MoveToGround>();
            moveToGround.FlipMovingToGround(true);
            
            HPPickup pick = spawnedObj.GetComponent<HPPickup>();
            pick.multiplier = waveCount;
         }
      }
   }

   bool OneInTenChance()
   {
      return Random.Range(0, 100/waveCount) == 0;
   }

   private GameObject GetPooledAIObject()
   {
      //find the first inactive object in the pool
      for (int i = 0; i < pooledAIObjects.Count; i++)
      {
         if (!pooledAIObjects[i].activeInHierarchy)
         {
            return pooledAIObjects[i];
         }
      }

      GameObject newObj = Instantiate(AIPrefab, transform);
      pooledAIObjects.Add(newObj);
      return newObj;
   }
   
   private GameObject GetPooledHPObject()
   {
      for (int i = 0; i < pooledHPObjects.Count; i++)
      {
         if (!pooledHPObjects[i].activeInHierarchy)
         {
            return pooledHPObjects[i];
         }
      }

      GameObject newObj = Instantiate(HPPrefab, transform);
      pooledHPObjects.Add(newObj);
      return newObj;
   }
}
