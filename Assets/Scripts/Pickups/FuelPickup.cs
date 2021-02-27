using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickup : PickableItem
{

    FuelManager fuelManager;
    PickUpManagerRedux pickupManager;

    public override void Activate()
    {
        fuelManager.AddFuel(value);
    }

    void Start()
    {
        fuelManager = FindObjectOfType<FuelManager>();
        pickupManager = FindObjectOfType<PickUpManagerRedux>();
    }

    void Update()
    {
        // Возможно стоит убрать этот функционал в pickupmanager
        if (fuelManager == null)
            return;
        if (fuelManager.fuel - fuelManager.consumptionRate*Time.deltaTime < 0)
        {
            if (pickupManager.currentPickup == this)
                pickupManager.ActivatePickup();
        }
    }
}
