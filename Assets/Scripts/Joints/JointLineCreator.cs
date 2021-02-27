using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointLineCreator : MonoBehaviour {

    JointDeleter jointDeleter;
    LineRenderer lineRenderer;

	void Start ()
    {
        lineRenderer = transform.gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        jointDeleter = transform.gameObject.GetComponent<JointDeleter>();
	}

	void Update ()
    {
        if (jointDeleter.currentAttractor != null)
        {
            if (!lineRenderer.enabled)
                lineRenderer.enabled = true;
            CreateLine(transform.gameObject, jointDeleter.currentAttractor);
        }
        else
        {
            if (lineRenderer.enabled)
                lineRenderer.enabled = false;
        }
	}
    
    void CreateLine(GameObject from, GameObject to)
    {
        lineRenderer.SetPosition(0, from.transform.position);
        lineRenderer.SetPosition(1, to.transform.position);
    }
}
