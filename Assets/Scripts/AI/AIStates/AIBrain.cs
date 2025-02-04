using System;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    public AIStates currentState;

    public PowerGauge pwrGge;

    public Transform playerTransform;
    
    public int waveCount;
    
    public HealthComponent HP;

    public GameObject spawnObj;
    public GameObject flyObj;
    public GameObject walkObj;
    public GameObject growShrinkObj;
    public GameObject attackObj;
    public GameObject celebrateObj;
    public GameObject dieObj;
    
    public GameObject miniMapComp;
    
    public Dictionary<AIStates, GameObject> AIStatesDict = new Dictionary<AIStates, GameObject>();

    public GameObjectStateManager sm;
    
    private bool initialized = false;
    
    public event Action<AIStates> AnnounceAIState;
    
    private void OnEnable()
    {
        if (!initialized)
        {
            AIStatesDict.Add(AIStates.Spawn, spawnObj);
            AIStatesDict.Add(AIStates.GrowShrink, growShrinkObj);
            AIStatesDict.Add(AIStates.FlyToEarth, flyObj);
            AIStatesDict.Add(AIStates.WalkToPlayer, walkObj);
            AIStatesDict.Add(AIStates.Attack, attackObj);
            AIStatesDict.Add(AIStates.Celebrate, celebrateObj);
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
            AnnounceAIState?.Invoke(currentState);
        }
    }

    private void OnDisable()
    {
        HP.AnnounceHP -= Die;
    }
}
