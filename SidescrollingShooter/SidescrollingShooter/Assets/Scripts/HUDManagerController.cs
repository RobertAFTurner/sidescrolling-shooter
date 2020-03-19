using TMPro;
using UnityEngine;

public class HUDManagerController : MonoBehaviour
{
    [SerializeField]
    GameObject playerObject;

    [SerializeField]
    GameObject shotgunObject;

    [SerializeField]
    GameObject gunObject;

    private TextMeshProUGUI shotgunText;
    private TextMeshProUGUI gunText;

    private void Start()
    {
        shotgunText = shotgunObject.GetComponent<TextMeshProUGUI>();
        gunText = gunObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Not very effecient to do this every frame.
        var playerAimComponent = playerObject.GetComponent<PlayerAimWeapon>();
        shotgunText.text = playerAimComponent.shotgunAmmoCount.ToString();
        gunText.text = playerAimComponent.ammoCount.ToString();
    }
}
