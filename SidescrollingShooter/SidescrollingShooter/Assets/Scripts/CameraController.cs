using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField]
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, -10);
    }
}
