using UnityEngine;

public class PickupRedux : MonoBehaviour
{
    public new string name;
    
    public GameObject particleObject;
    public int value;
    public AudioClip onPickUpClip;

    public GameObject boundAttractor;

    public virtual void Activate() { }
    public virtual void OnPickup() { }

    private float rotSpeed = 180;
    
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime,Space.World);
    }

}
