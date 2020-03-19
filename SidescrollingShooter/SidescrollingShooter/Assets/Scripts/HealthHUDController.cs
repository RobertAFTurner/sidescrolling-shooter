using UnityEngine;

public class HealthHUDController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    private HealthController playerHealthController;
    private int hitPointsRemaining;

    void Start()
    {
        playerHealthController = player.GetComponent<HealthController>();
        hitPointsRemaining = playerHealthController.HitPointsRemaining();

        for (int i = 0; i < hitPointsRemaining; i++)
        {
            AddHealthIcon(i);
        }
    }

    private void AddHealthIcon(int number)
    {
        var resource = (GameObject)Resources.Load("Prefabs\\HealthIcon");
        var icon = Instantiate(resource, transform);
        icon.name = $"HealthIcon_{number}";
        icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(number * 110, 0);
    }

    private void RemoveHealthIcon(int number)
    {
        var icon = transform.Find($"HealthIcon_{number}");
        Destroy(icon.gameObject);
    }

    private void Update()
    {
        if(playerHealthController.HitPointsRemaining() < hitPointsRemaining)
        {
            hitPointsRemaining = playerHealthController.HitPointsRemaining();
            RemoveHealthIcon(hitPointsRemaining);
        }
    }
}
