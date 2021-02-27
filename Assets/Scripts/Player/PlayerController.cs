using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerDeath;
    public GameObject explosion;
    public Transform playerModel;
    public Vector2 speed;
    Rigidbody2D playerRb;
    PickUpManagerRedux pickUpManager;
    public float rotationSpeedInDeg = 90f;

    void Start()
    {
        pickUpManager = FindObjectOfType<PickUpManagerRedux>();
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerRb.velocity = speed;
    }

    void FixedUpdate()
    {
        playerRb.velocity = transform.up * 10f;
        playerModel.Rotate(new Vector3(0, rotationSpeedInDeg * Time.deltaTime, 0), Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (pickUpManager.currentPickup != null && pickUpManager.currentPickup.name == "Rewind")
        {
            pickUpManager.ActivatePickup();
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        OnPlayerDeath();
        Destroy(gameObject);
        GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity, null);
        newExplosion.GetComponent<ParticleSystem>().Play();
        newExplosion.GetComponent<AudioSource>().enabled = true;
        newExplosion.GetComponent<AudioSource>().Play();
    }
}
