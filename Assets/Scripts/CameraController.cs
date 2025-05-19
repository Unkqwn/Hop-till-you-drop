using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    public float moveSpeed = 5;

    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, player.position + offset, moveSpeed * Time.deltaTime);
    }
}
