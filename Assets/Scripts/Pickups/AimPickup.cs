using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPickup : LastingItem
{
    public GameObject aimLaserPrefab;
    GameObject aimLaser;

    private void Start()
    {
        transform.Rotate(Vector3.up, Random.Range(0f, 180f));
    }

    public override void Activate()
    {
        state = !state;
        if (aimLaser == null)
        {
            var playerController = FindObjectOfType<PlayerController>();
            aimLaser = Instantiate(aimLaserPrefab, playerController.transform);
        }
        aimLaser.SetActive(state);
    }
    private void OnDestroy()
    {
        Destroy(aimLaser);
    }
}
