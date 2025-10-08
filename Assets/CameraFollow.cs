using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // le transform du joueur
    public Vector3 offset = new Vector3(0f, 5f, -8f); // position de la caméra par rapport au joueur
    public float smoothSpeed = 5f; // vitesse de suivi fluide

    void LateUpdate()
    {
        if (player == null)
            return;

        // position désirée = position du joueur + décalage
        Vector3 desiredPosition = player.position + player.transform.TransformDirection(offset);

        // mouvement fluide vers la position désirée
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // orientation de la caméra (optionnel)
        transform.LookAt(player);
    }
}