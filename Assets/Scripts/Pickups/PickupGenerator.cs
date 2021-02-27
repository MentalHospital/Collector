using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : MonoBehaviour
{
    public MapGenerator mapGenerator;

    [SerializeField] List<GameObject> pickUpPrefabs;
    [SerializeField] int minPickUps = 1;
    [SerializeField] int maxPickUps = 3;

    public void GeneratePickups(List<GameObject> attractors)
    {
        foreach (var attractor in attractors)
        {
            int count = Random.Range(minPickUps, maxPickUps);
            for (int i = 0; i < count; i++)
            {
                float radius = Random.Range(1.5f, attractor.GetComponent<AttractorManager>().soiRadius);
                Vector3 offset = Random.insideUnitCircle.normalized * radius;
                var currentPickUp = Instantiate(
                    pickUpPrefabs[Random.Range(0, pickUpPrefabs.Count)],
                    attractor.transform.position + offset,
                    Quaternion.identity
                    );
                currentPickUp.GetComponent<PickupRedux>().boundAttractor = attractor;
            }
        }
    }
}

