using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoiManager : MonoBehaviour {

    CircleCollider2D soi;

    public void UpdateLocalScale()
    {
        var circleColliders = transform.gameObject.GetComponentsInParent<CircleCollider2D>();
        foreach (var collider in circleColliders)
        {
            if (collider.isTrigger)
                soi = collider;
        }
        if (soi != null)
        {
            transform.localScale = new Vector3(soi.radius * 2f, soi.radius * 2f, 1f);
        }
    }
}
