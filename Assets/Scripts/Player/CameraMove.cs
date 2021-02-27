using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    readonly Vector3 cameraOffset = Vector3.back * 10;
    public const float ZOOM_LERP_SPEED = 1.2f;
    public const float ZOOM_MINVALUE = 10f;
    public const float ZOOM_MAXVALUE = 15f;
    public const float FOLLOWING_LERP_SPEED = 6f;
    const float FIXED_LERP_SPEED = 1.2f;

    public GameObject player;
    Vector3 targetPosition;
    bool isFollowingPlayer;
    float lerpSpeed;

    void Start()
    {
        transform.position = player.transform.position;
        FollowPlayerPosition();
    }

    void Update()
    {
        if (player != null)
        {
            if (player.GetComponent<JointDeleter>().currentAttractor != null)
                FixAtPosition(player.GetComponent<JointDeleter>().currentAttractor.transform.position);
            else
                FollowPlayerPosition();

            if (isFollowingPlayer)
                targetPosition = player.transform.position + cameraOffset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);

            //Динамическое изменение "зума" камеры в зависимости от ее скорости
            Camera.main.orthographicSize = Mathf.Lerp(
                Camera.main.orthographicSize,
                Mathf.Clamp(ZOOM_MAXVALUE - Mathf.Clamp(Camera.main.velocity.magnitude, 0, 10) / 2,
                    ZOOM_MINVALUE,
                    ZOOM_MAXVALUE),
                ZOOM_LERP_SPEED * Time.deltaTime
                );
        }
    }

    public void FixAtPosition(Vector3 position)
    {
        isFollowingPlayer = false;
        lerpSpeed = FIXED_LERP_SPEED;
        targetPosition = position + cameraOffset;
    }
    public void FollowPlayerPosition()
    {
        isFollowingPlayer = true;
        lerpSpeed = FOLLOWING_LERP_SPEED;
    }
}
