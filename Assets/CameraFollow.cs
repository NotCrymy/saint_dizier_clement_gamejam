using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 5f, -8f);

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = player.position + player.transform.TransformDirection(offset);

        transform.LookAt(player);
    }
}