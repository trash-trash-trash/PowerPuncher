using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileShooter : MonoBehaviour
{
   public GameObject prefab;

   public Transform shootingTransform;
   
   public Transform playerTransform;

   public float projectileSpeed;

   public int numShots;

   public float fireRate;

   public float spreadRandomiserAmount;

   public List<GameObject> pooledObjs = new List<GameObject>();

   public void ShootMachineGun()
   {
      StartCoroutine(MachineGunShoot());
   }

   IEnumerator MachineGunShoot()
   {
      int counter = 0;
      for (int i = 0; i < numShots; i++)
      {
         Shoot(i);
         yield return new WaitForSeconds(fireRate);
         counter++;
      }
   }
   
   public void Shoot(int index)
   {
      GameObject obj = GetPooledObject();
      
      obj.transform.position = shootingTransform.position;
      Rigidbody rb = obj.GetComponent<Rigidbody>();
      
      Vector3 direction = (playerTransform.position - shootingTransform.position).normalized;

      if (index % 2 == 0)
      {
         float rand = Random.Range(-spreadRandomiserAmount, spreadRandomiserAmount);
         direction.x += rand;
      }
      else if (index % 3 == 0)
      {
         float rand = Random.Range(-spreadRandomiserAmount, spreadRandomiserAmount);
         direction.z += rand;
      }
      
      rb.linearVelocity = direction * projectileSpeed;

      Vector3 vec = GetRandomDirection();
      rb.AddTorque(vec * projectileSpeed);
   }
   
   Vector3 GetRandomDirection()
   {
      int x = Random.Range(-1, 2); 
      int z = Random.Range(-1, 2);

      return new Vector3(x, 0, z).normalized;
   }

   
   private GameObject GetPooledObject()
   {
      for (int i = 0; i < pooledObjs.Count; i++)
      {
         if (!pooledObjs[i].activeInHierarchy)
         {
            return pooledObjs[i];
         }
      }

      GameObject newObj = Instantiate(prefab, transform);
      pooledObjs.Add(newObj);
      return newObj;
   }  
}
