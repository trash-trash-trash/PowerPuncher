using UnityEngine;

public class BossStateBase : MonoBehaviour
{
   public BossBrain bossBrain;
   
   public virtual void OnEnable()
   {
      bossBrain = GetComponentInParent<BossBrain>();
   }
}
