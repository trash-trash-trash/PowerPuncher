using System;
using UnityEngine;

public class HPPickup : MonoBehaviour
{
   public int multiplier;

   void OnEnable()
   {
      Physics.IgnoreLayerCollision(0,7);
   }

   public void OnTriggerEnter(Collider other)
   {
      if (other.GetComponentInParent<PlayerHP>()!=null)
      {
         HealthComponent HP = other.GetComponentInParent<PlayerHP>();
         HP.ChangeHP(10 * multiplier);
         gameObject.SetActive(false);
      }
   }
}
