using UnityEngine;

public class Respawner : MonoBehaviour
{
    JointDeleter jointDeleter;
    FuelManager fuelManager;
    Transform player;

    private void Start()
    {
        jointDeleter = FindObjectOfType<JointDeleter>();
        fuelManager = FindObjectOfType<FuelManager>();
        player = jointDeleter.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        if (jointDeleter.lastAttractor != null)
        {
            jointDeleter.DeleteJointSilent();
            jointDeleter.lastAttractor.GetComponent<JointCreator>().jointDeleted = false;
            RespawnAt(jointDeleter.lastAttractor);
        }
    }

    public void RespawnAt(GameObject attractor)
    {
        if (attractor == null)
            return;

        fuelManager.AddFuel(Mathf.Clamp(fuelManager.maxFuel / 3, 0f, fuelManager.maxFuel - fuelManager.fuel));

        player.position = attractor.transform.position
            + new Vector3
            (
                Random.Range(2.5f, AttractorManager.soiRadiusMin) * ((Random.value > 0.5f) ? 1 : -1),
                -3f
            );
        player.rotation = Quaternion.identity;
        player.GetComponent<Rigidbody2D>().angularVelocity = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

}
