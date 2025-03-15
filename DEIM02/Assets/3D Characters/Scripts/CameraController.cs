using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform follow;
    public float maxDistance;
    private Vector2 angle = new Vector2 (90 * Mathf.Deg2Rad, 0);
    public Vector2 sensitivity;
    private new Camera camera;
    private Vector2 nearPlaneSize;
    public GameManager gameManager;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponent<Camera>();

        CalculateNearPlaneSize();
    }

    

    private void CalculateNearPlaneSize()
    {
        float height = Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * camera.nearClipPlane;
        float width = height * camera.aspect;

        nearPlaneSize = new Vector2(width, height);
    }

    private Vector3[] GetCameraCollisionPoints(Vector3 direction)
    {
        Vector3 position = follow.position;
        Vector3 center = position + direction * (camera.nearClipPlane + 0.2f);

        Vector3 right = transform.right * nearPlaneSize.x;
        Vector3 up = transform.up * nearPlaneSize.y;

        return new Vector3[] {
            center - right + up,
            center + right + up,
            center + right - up,
            center - right - up
        };
    }

    private void Update()
    {
        if (gameManager.paused)
        {
           // Cursor.lockState = CursorLockMode.None; // Cursor completamente libre
        }
        else
        {
            //Cursor.lockState = CursorLockMode.Locked;

        }
        float hor = Input.GetAxis("Mouse X");

        if (hor != 0)
        {
            angle.x += hor * Mathf.Deg2Rad * sensitivity.x;
        }

        float ver = Input.GetAxis("Mouse Y");

        if (ver != 0)
        {
            angle.y += ver * Mathf.Deg2Rad * sensitivity.y;
            angle.y = Mathf.Clamp(angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = new Vector3(
            Mathf.Cos(angle.x) * Mathf.Cos(angle.y),
            -Mathf.Sin(angle.y),
            -Mathf.Sin(angle.x) * Mathf.Cos(angle.y)
        );

        RaycastHit hit;
        float distance = maxDistance;

        Vector3[] points = GetCameraCollisionPoints(direction);

        foreach (Vector3 point in points)
        {

            if (Physics.Raycast(follow.position, direction, out hit, maxDistance))
            {
                distance = Mathf.Min((hit.point - follow.position).magnitude, distance);
            }
        }
        transform.position = follow.position + direction * distance; 
        transform.rotation = Quaternion.LookRotation(follow.position - transform.position);
    }
}
