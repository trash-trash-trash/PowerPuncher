using UnityEngine;

//Redundant, just copy pasted AIFaceAnimator due to time constraints
public class BossAIFaceAnimator : MonoBehaviour
{
        public BossBrain bossBrain;

        public Animator animator;

        void OnEnable()
        {
            bossBrain.AnnounceState += ChangeFace;
        }

        private void ChangeFace(BossStates obj)
        {
            if(obj == BossStates.TurnToPlayer)
                animator.Play(("EnemyAIFace_Neutral"));
            else if (obj == BossStates.Die)
                animator.Play("EnemyAIFace_TakeDamage");
            else
                animator.Play("EnemyAIFace_Attack");
        }

        void OnDisable()
        {
            bossBrain.AnnounceState -= ChangeFace;
        }
    }
