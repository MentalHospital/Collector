using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorManager : MonoBehaviour
{
    public const float soiRadiusMin = 4f;
    public const float soiRadiusMax = 7f;

    public float soiRadius;

    public void UpdateRadius(float value)
    {
        soiRadius = value;
        gameObject.GetComponent<CircleCollider2D>().radius = soiRadius;
        transform.GetComponentInChildren<SoiManager>().UpdateLocalScale();
    }
}
