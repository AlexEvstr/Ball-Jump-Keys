using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private float followSpeed = 2f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;

            desiredPosition.y = Mathf.Max(desiredPosition.y, 0f);

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / followSpeed);
        }
    }
}