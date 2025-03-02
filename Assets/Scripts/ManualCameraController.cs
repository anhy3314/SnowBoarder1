using UnityEngine;

public class ManualCameraController : MonoBehaviour
{
    public Transform target;  // Nhân vật cần theo dõi
    public Vector3 offset = new Vector3(5, 2, -30);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.LookAt(target);  // Luôn nhìn về nhân vật
        }
    }
}
