using UnityEngine;

public class AIFaceAnimator : MonoBehaviour
{
   public AIBrain aiBrain;

   public Animator animator;

   void OnEnable()
   {
      aiBrain.AnnounceAIState += ChangeFace;
   }

   private void ChangeFace(AIStates obj)
   {
      if(obj == AIStates.Attack || obj == AIStates.Celebrate || obj==AIStates.FlyToEarth)
         animator.Play(("EnemyAIFace_Attack"));
      else if (obj == AIStates.Die)
         animator.Play("EnemyAIFace_TakeDamage");
      else
         animator.Play("EnemyAIFace_Neutral");
   }

   void OnDisable()
   {
      aiBrain.AnnounceAIState -= ChangeFace;
   }
}
