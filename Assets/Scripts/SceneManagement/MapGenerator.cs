using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance;

    public GameObject attractorPrefab;
    public GameObject player;

    public int attractorCount;
    
    public List<GameObject> attractors = new List<GameObject>();

    PickupGenerator pickUpGenerator;

    Color[] colors;

    void Awake ()
    {
        colors = new Color[] { Color.red, Color.magenta, Color.green, Color.blue, Color.yellow, Color.cyan };
        //
        int highscore = PlayerPrefs.GetInt("highscore");
        if (highscore > 900)
            attractorCount = 100;
        else
            attractorCount = (highscore / 100) * 10 + 10;
        //
        pickUpGenerator = FindObjectOfType<PickupGenerator>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        GeneratePlanets();
        pickUpGenerator.GeneratePickups(attractors);
        
	}
	
    void GeneratePlanets()
    {
        Vector3 currentPosition = player.transform.position 
            + new Vector3
            (
                Random.Range(2.5f, AttractorManager.soiRadiusMin)*((Random.value > 0.5f) ? 1 : -1), 
                20f
            );
        // генерация в линию
        float previousRadius = 0f;
        for (int i = 0; i < attractorCount; i++)
        {
            float directionAngle = Random.Range(20, 160);
            directionAngle = Mathf.Deg2Rad * directionAngle;
            Vector3 generatedDirection = new Vector3(Mathf.Cos(directionAngle), Mathf.Sin(directionAngle));
            float generatedRadius = Random.Range(AttractorManager.soiRadiusMin, AttractorManager.soiRadiusMax);

            if (i != 0)
                currentPosition += generatedDirection * (generatedRadius + previousRadius + Random.Range(3f,6f));

            previousRadius = generatedRadius;

            GameObject currentAttractor = Instantiate(attractorPrefab, currentPosition, Quaternion.identity);
            currentAttractor.GetComponent<PlanetFuel>().player = player;
            currentAttractor.GetComponent<AttractorManager>().UpdateRadius(generatedRadius);
            //currentAttractor.GetComponentInChildren<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
            currentAttractor.name = "Attractor" + i;
            attractors.Add(currentAttractor);
        }
    }
}
