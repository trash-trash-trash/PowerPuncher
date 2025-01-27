using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Punch : MonoBehaviour
{
    public PlayerControls playerControls;

    public int rightArmDmg;
    public float rightArmPunchForce;
    
    public int leftArmDmg;
    public float leftArmPunchForce;
    
    public float leftTimer;
    public bool chargingLeftPunch;

    public float rightTimer;
    public bool chargingRightPunch;
    
    public float minSweetTime;
    public float maxSweetTime;
    
    private float sphereRadius;
    public float sweetPunchRadius;
    
    public LayerMask layerMask;
    private IEnumerator leftPunchCoroutine;
    private IEnumerator rightPunchCoroutine;
    
    public Transform leftPunchTransform;   
    public Transform rightPunchTransform;

    public int numberOfPunchFrames;
    
    private FlingAndRotate flingAndRotate;
    
    private Collider[] colliders = new Collider[50];

    public event Action<bool> AnnounceLeftRightFail;

    public event Action<bool> AnnounceLeftRightPunch;

    public event Action<bool, float> AnnounceLeftRightTimer;

    public event Action<bool> AnnouncePerfectPunch;

    private void Start()
    {
        leftPunchCoroutine = ChargeLeftPunch();
        rightPunchCoroutine = ChargeRightPunch();
        playerControls.AnnounceLeftPunch += HandleLeftPunch;
        playerControls.AnnounceRightPunch += HandleRightPunch;
        
        AnnounceLeftRightTimer?.Invoke(true, 0);
        AnnounceLeftRightTimer?.Invoke(false, 0);
    }

    private void HandleLeftPunch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            leftTimer = 0;
            chargingLeftPunch = true;
            StartCoroutine(leftPunchCoroutine);
        }
        else
        {
            chargingLeftPunch = false;
            StopCoroutine(leftPunchCoroutine);
            SwingPunch(leftTimer, true);
            leftTimer = 0;
            AnnounceLeftRightTimer?.Invoke(true, 0);
        }
    }

    IEnumerator ChargeLeftPunch()
    {
        while (chargingLeftPunch)
        {
            yield return new WaitForSeconds(0.1f);
            leftTimer += 0.1f; 
            AnnounceLeftRightTimer?.Invoke(true, leftTimer);
        }
    }
    
    private void HandleRightPunch(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            rightTimer = 0;
            chargingRightPunch = true;
            StartCoroutine(rightPunchCoroutine);
        }
        else
        {
            chargingRightPunch = false;
            StopCoroutine(rightPunchCoroutine);
            SwingPunch(rightTimer, false);
            rightTimer = 0;
            AnnounceLeftRightTimer?.Invoke(false, 0);
        }
    }

    IEnumerator ChargeRightPunch()
    {
        while (chargingRightPunch)
        {
            yield return new WaitForSeconds(0.1f);
            rightTimer += 0.1f;
            AnnounceLeftRightTimer?.Invoke(false, rightTimer);
        }
    }

    void SwingPunch(float time, bool leftRight)
    {
        bool isPerfectPunch = time >= minSweetTime && time <= maxSweetTime;
        
        int punchDmg = leftRight ? rightArmDmg : leftArmDmg;
        float punchForce = leftRight ? rightArmPunchForce : leftArmPunchForce;
    
        if (isPerfectPunch)
        {
            sphereRadius = sweetPunchRadius;
            StartCoroutine(SwingPunchCoro(leftRight, punchDmg, punchForce, true));
        }
        else
        {
            AnnounceLeftRightFail?.Invoke(leftRight);
            return;
        }

        // Add punch duration here later
        AnnounceLeftRightPunch?.Invoke(leftRight);
    }

    public IEnumerator SwingPunchCoro(bool input, int newAttackDamage, float newAttackForce, bool isPerfectPunch)
    {
        int counter = 0;

        while (counter < numberOfPunchFrames)
        {
            counter++;
            PerformOverlapSphere(input, newAttackDamage, newAttackForce);
            yield return new WaitForFixedUpdate();
        }
    }

    public void PerformOverlapSphere(bool input, int newAttackDamage, float newAttackForce)
    {
        Transform punchTransform = input ? leftPunchTransform : rightPunchTransform;

        Vector3 sphereCenter = punchTransform.position;

        int hitCount = Physics.OverlapSphereNonAlloc(sphereCenter, sphereRadius, colliders, layerMask);

        List<GameObject> objList = new List<GameObject>();

        for (int i = 0; i < hitCount; i++)
        {
            if(!objList.Contains(colliders[i].gameObject))
                objList.Add(colliders[i].gameObject);
        }
        
        if(hitCount>0)
                AnnouncePerfectPunch?.Invoke(input);

        foreach (GameObject obj in objList)
        {
            HealthComponent health = obj.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.ChangeHP(newAttackDamage);
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 pushDirection = gameObject.transform.position - punchTransform.position;
                    pushDirection.Normalize();
                    Vector3 finalDirection = new Vector3(pushDirection.x, 0, pushDirection.z);
                    rb.AddForce(finalDirection * newAttackForce, ForceMode.Impulse);

                    flingAndRotate = SingletonTools.Instance.flingAndRotate;
                    flingAndRotate.Explode(rb, newAttackForce);

                    SingletonTools.Instance.powerGauge.IncreasePower();
                }
            }
        }
    }
            
    void OnDisable()
    {
        playerControls.AnnounceLeftPunch -= HandleLeftPunch;
    }
}