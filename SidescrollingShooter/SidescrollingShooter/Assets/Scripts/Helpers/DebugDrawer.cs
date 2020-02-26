
using UnityEngine;

public class DebugDrawer
{
    public static void DrawDebugCast(BoxCollider2D collider, RaycastHit2D cast, float extraHeight)
    {
        Color rayColor;
        if (cast.collider != null)
            rayColor = Color.green;
        else
            rayColor = Color.red;

        Debug.DrawRay(collider.bounds.center + new Vector3(collider.bounds.extents.x, 0), Vector3.down * (collider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(collider.bounds.center - new Vector3(collider.bounds.extents.x, 0), Vector3.down * (collider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(collider.bounds.center - new Vector3(collider.bounds.extents.x, collider.bounds.extents.y + extraHeight), Vector3.right * (collider.bounds.extents.x), rayColor);

        Debug.Log(cast.collider);
    }
}