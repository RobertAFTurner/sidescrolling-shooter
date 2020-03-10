
using UnityEngine;

public class CrateController : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.gameObject.GetComponent<PlayerController>().UpgradeWeapon();
            Debug.Log("Pickup");
            Destroy(gameObject);
        }
    }
}
