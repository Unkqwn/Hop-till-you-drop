using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed = 0.3f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (player != null)
        {
            Vector3 cameraPosition = player.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref velocity, followSpeed);
            transform.LookAt(player);
        }
    }
}
