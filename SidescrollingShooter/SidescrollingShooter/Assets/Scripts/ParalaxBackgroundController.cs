
using UnityEngine;

public class ParalaxBackgroundController : MonoBehaviour
{
    private float length;
    private float startPosition;

    [SerializeField]
    private float paralaxEffectMultiplyer;

    [SerializeField]
    private GameObject camera;

    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        var distanceToMove = camera.transform.position.x * paralaxEffectMultiplyer;
        transform.position = new Vector2(startPosition + distanceToMove, camera.transform.position.y);

        var tempValue = camera.transform.position.x * 1 - paralaxEffectMultiplyer;

        if (tempValue > startPosition + length)
        {
            startPosition += length;
        }
        else if (tempValue < startPosition - length)
        {
            startPosition -= length;
        }
    }
}
