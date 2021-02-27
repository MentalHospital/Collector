using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : PickupRedux
{
    ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        transform.Rotate(Vector3.up, Random.Range(0f, 180f));
    }

    public override void OnPickup()
    {
        scoreManager.AddPoints(value);
    }
}

