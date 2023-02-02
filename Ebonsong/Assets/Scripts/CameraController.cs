using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = playerTransform.position.x;
        cameraPosition.y = playerTransform.position.y;
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, 0, 145);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, -29, 50);
        transform.position = cameraPosition;
    }
}
