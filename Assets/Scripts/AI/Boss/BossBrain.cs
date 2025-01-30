using System;
using System.Collections.Generic;
using UnityEngine;

public class BossBrain : MonoBehaviour
{
    public Transform playerTransform;
    
    public BossStates currentState;
    
    public GameObject spawnObj;
    public GameObject turnToPlayerObj;
    public GameObject attackObj;
    public GameObject jumpObj;
    public GameObject dieObj;

    public Dictionary<BossStates, GameObject> bossStates;

    public event Action<BossStates> AnnounceState;
    
    public GameObjectStateManager sM;

    void OnEnable()
    {
        bossStates = new Dictionary<BossStates, GameObject>()
        {
            { BossStates.Spawn , spawnObj},
            { BossStates.TurnToPlayer , turnToPlayerObj},
            { BossStates.Attack , attackObj},
            { BossStates.Jump , jumpObj},
            { BossStates.Die , dieObj}
        };
        ChangeState(BossStates.Spawn);
    }

    public void ChangeState(BossStates newState)
    {
        if (bossStates.ContainsKey(newState))
        {
            currentState = newState;
            sM.ChangeState(bossStates[currentState]);
            AnnounceState?.Invoke(currentState);
        }
    }
}
