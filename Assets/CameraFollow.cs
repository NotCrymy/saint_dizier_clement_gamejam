using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 5f, -8f);

    void LateUpdate()
    {
        if (player == null) return;

        // La caméra garde toujours la même position relative
        transform.position = player.position + player.transform.TransformDirection(offset);

        // Regarde toujours le joueur
        transform.LookAt(player);
    }
}