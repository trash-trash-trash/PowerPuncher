using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
   public Transform centerTransform;
   public GameObject AIPrefab;
   
   public float minSpawnRadius;
   public float maxSpawnRadius;

   public List<GameObject> pooledObjects = new List<GameObject>();
   public int poolSize = 10;

   public int numberToSpawn=0;

   void Start()
   {
      for (int i = 0; i < poolSize; i++)
      {
         GameObject pooledObj = Instantiate(AIPrefab);
         pooledObj.SetActive(false);
         pooledObjects.Add(pooledObj);
         pooledObj.transform.parent = transform;
      }

      StartCoroutine(SpawnLoop());
   }

   public IEnumerator SpawnLoop()
   {
      while (true)
      {
         numberToSpawn++;

         for (int i = 0; i < numberToSpawn; i++)
         {
            SpawnInCircle();
         }

         yield return new WaitForSeconds(5f);
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

      Vector3 spawnPosition = new Vector3(
         centerTransform.position.x + randomPosition.x,
         centerTransform.position.y,
         centerTransform.position.z + randomPosition.y
      );
      
      GameObject spawnedObj = GetPooledObject();

      if (spawnedObj != null)
      {
         spawnedObj.transform.position = spawnPosition;
         spawnedObj.SetActive(true);

         AIBrain brain = spawnedObj.GetComponent<AIBrain>();
         brain.ChangeState(AIStates.Spawn);
      }
   }

   //get an object from the pool
   private GameObject GetPooledObject()
   {
      //find the first inactive object in the pool
      for (int i = 0; i < pooledObjects.Count; i++)
      {
         if (!pooledObjects[i].activeInHierarchy)
         {
            return pooledObjects[i];
         }
      }

      GameObject newObj = Instantiate(AIPrefab);
      newObj.transform.parent = transform;
      pooledObjects.Add(newObj);
      return newObj;
   }
}
