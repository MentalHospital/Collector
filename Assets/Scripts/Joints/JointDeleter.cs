using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class JointDeleter : MonoBehaviour
{
    ScoreManager scoreManager;
    public GameObject currentAttractor;
    
    public GameObject lastAttractor;

    public GameObject previousAttractor;
    public GameObject nextAttractor;

    public event Action OnJointDeleted;
    public event Action<GameObject> PassAttractorInfo;

    private void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
    }

    public void DeleteJoint()
    {
        if (currentAttractor == null)
            return;
        //
        PassAttractorInfo(currentAttractor);
        //
        OnJointDeleted();
        DeleteJointSilent();
    }

    public void DeleteJointSilent()
    {
        if (currentAttractor == null)
            return;

        lastAttractor = currentAttractor;
        
        currentAttractor.GetComponent<Joint2D>().connectedBody = null;
        currentAttractor.GetComponent<JointCreator>().jointDeleted = true;
        currentAttractor = null;
        transform.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }
}
