using System;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    public AIStates currentState;
    
    public HealthComponent HP;

    public GameObject spawnObj;
    public GameObject walkObj;
    public GameObject growShrinkObj;
    public GameObject dieObj;

    public Dictionary<AIStates, GameObject> AIStatesDict = new Dictionary<AIStates, GameObject>();

    public GameObjectStateManager sm;
    
    private bool initialized = false;
    
    private void OnEnable()
    {
        if (!initialized)
        {
            AIStatesDict.Add(AIStates.Spawn, spawnObj);
            AIStatesDict.Add(AIStates.WalkToPlayer, walkObj);
            AIStatesDict.Add(AIStates.GrowShrink, growShrinkObj);
            AIStatesDict.Add(AIStates.Die, dieObj);

            //hack?
            Physics.IgnoreLayerCollision(7, 7);
            initialized = true;
        }

        HP.AnnounceHP += Die;
    }

    private void Die(HealthData data)
    {
        if(!data.isAlive)
            ChangeState(AIStates.Die);
    }

    public void ChangeState(AIStates newState)
    {
        if (AIStatesDict.ContainsKey(newState))
        {
            currentState = newState; 
            sm.ChangeState(AIStatesDict[currentState]);
        }
    }

    private void OnDisable()
    {
        HP.AnnounceHP -= Die;
    }
}
