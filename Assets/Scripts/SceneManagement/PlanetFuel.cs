using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetFuel : MonoBehaviour
{
    public const int planetPoints = 5;
    public float fuel;

    public event Action OnPlanetEmpty;

    float maxFuel;
    Color maxColor;
    float deltaFuel;
    float deltaPlayerFuel;
    public GameObject player;
    public GameObject model;
    JointDeleter jointDeleter;
    FuelManager fuelManager;

    bool hasEnergy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hasEnergy = true;
        fuelManager = player.GetComponent<FuelManager>();
        jointDeleter = player.GetComponent<JointDeleter>();
        fuel = UnityEngine.Random.Range(50, 75);
        maxFuel = fuel;
        maxColor = model.GetComponent<Renderer>().material.color;
        deltaFuel = 5 * fuelManager.consumptionRate;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (jointDeleter.currentAttractor == transform.gameObject)
            {
                if (fuel >= deltaFuel*Time.deltaTime && hasEnergy)//added hasenergy
                {
                    if (fuelManager.fuel < 100 - deltaFuel * Time.deltaTime)
                    {
                        fuelManager.AddFuel(deltaFuel * Time.deltaTime);
                        fuel -= deltaFuel * Time.deltaTime;
                    }
                    else
                    {
                        deltaPlayerFuel = fuelManager.maxFuel - fuelManager.fuel;
                        fuelManager.AddFuel(deltaPlayerFuel);
                        fuel -= deltaPlayerFuel;
                    }
                    model.GetComponent<Renderer>().material.color = maxColor * fuel/maxFuel + new Color(0,0,0,255);
                }
                else
                {
                    if (hasEnergy)
                    {
                        CallPlanetEmpty();
                        hasEnergy = false;
                    }
                }
            }             
        }
    }

    void CallPlanetEmpty()
    {
        OnPlanetEmpty();
        model.GetComponent<Renderer>().material.color = Color.grey;
    }
}
