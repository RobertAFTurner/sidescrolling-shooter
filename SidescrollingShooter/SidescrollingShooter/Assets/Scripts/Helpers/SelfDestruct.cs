using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    private float timer;

    void Start()
    {
        Destroy(gameObject, timer);
    }
}
