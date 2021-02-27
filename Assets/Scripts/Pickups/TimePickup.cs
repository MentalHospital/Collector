using UnityEngine;

// использовать здесь TimeManager
public class TimePickup : LastingItem
{
    float fixedDeltaTime;

    private void Start()
    {
        transform.Rotate(Vector3.up, Random.Range(0f, 180f));
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    public override void Activate()
    {
        state = !state;
        if (state)
        {
            Time.timeScale = 0.4f;
            Time.fixedDeltaTime = Time.timeScale * fixedDeltaTime;
        }
        else
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = fixedDeltaTime;
        }
    }
}
