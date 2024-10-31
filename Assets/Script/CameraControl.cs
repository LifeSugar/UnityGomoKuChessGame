using UnityEngine;

public class CameraController : MonoBehaviour
{
public float moveSpeed = 0.1f; // 平移速度
public float zoomSpeed = 1f; // 缩放速度
public float minZoom = 5f; // 最小缩放值
public float maxZoom = 20f; // 最大缩放值

private Camera cam;
private Vector3 lastMousePosition;

void Start()
{
    cam = Camera.main;
}

void Update()
{
    HandleMovement();
    HandleZoom();
}

void HandleMovement()
{
    if (Input.GetMouseButton(1)) 
    {
        Vector3 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(lastMousePosition);
        transform.position -= delta; 
    }

    lastMousePosition = Input.mousePosition;
}

void HandleZoom()
{
    float scroll = Input.GetAxis("Mouse ScrollWheel");
    if (scroll != 0f)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom); // 缩放摄像机
    }
}
}