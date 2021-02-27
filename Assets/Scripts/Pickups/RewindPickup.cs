using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindPickup : ReusableItem
{
    public GameObject playerRespawnerPrefab;
    Respawner respawner;
    GameObject respawnerObject;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(45, 45, 45);
        transform.Rotate(Vector3.up, Random.Range(0, 90), Space.World);
    }

    public override void OnPickup()
    {
        if (respawner == null)
        {
            var playerController = FindObjectOfType<PlayerController>();
            respawnerObject = Instantiate(playerRespawnerPrefab, playerController.transform);
            respawner = respawnerObject.GetComponent<Respawner>();
        }
    }

    public override void Activate()
    {
        respawner.RespawnPlayer();
    }

    private void OnDestroy()
    {
        Destroy(respawner);
    }
}
