using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{

    public float maxFuel = 100f;
    public float consumptionRate = 10f; // per second
    public float fuel
    {
        get;
        private set;
    }

    public Slider slider;

	void Start ()
    {
        fuel = maxFuel;
	}
	
	void Update ()
    {
        slider.value = fuel / maxFuel;

        fuel -= consumptionRate * Time.unscaledDeltaTime;
        if (fuel < 0)
        {
            if (GetComponent<PickUpManagerRedux>().currentPickup != null &&
                GetComponent<PickUpManagerRedux>().currentPickup.name == "Fuel")
            {
                GetComponent<PickUpManagerRedux>().ActivatePickup();
            }
            else
            {
                fuel = 0;
                if (GetComponent<PlayerController>() != null)
                    GetComponent<PlayerController>().GameOver();
            }
        }
	}

    public void AddFuel(float value)
    {
        fuel += value;
        if (fuel > maxFuel)
            fuel = maxFuel;
    }


}
