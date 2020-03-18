
using UnityEngine;

public class CrateController : MonoBehaviour
{
    [SerializeField]
    bool shotgunAmmo = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            var controller = collision.collider.gameObject.GetComponent<PlayerAimWeapon>();

            if (!shotgunAmmo)
                controller.AddAmmo(200);
            else
                controller.AddShotgunAmmo(10);

            Destroy(gameObject);
        }
    }
}
