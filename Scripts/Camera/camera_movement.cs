using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;




public class CameraController : MonoBehaviour
{
    public float moveSpeed = 20f; // Speed at which the camera moves
    public float CursormoveSpeed = 200f;
    public float rotateSpeed = 200f; // Speed at which the camera rotates
    public float zoomSpeed = 10f; // Speed at which the camera zooms in and out
    public float minZoomDistance = 5f; // Minimum distance the camera can zoom in
    public float maxZoomDistance = 50f; // Maximum distance the camera can zoom out
    public LayerMask selectableLayer; // Layer to select objects from

    private float currentRotationX = 14.10413f; // Horizontal rotation (yaw)
    private float currentRotationY = 38.66193f; // Vertical rotation (pitch)
    private const float minPitch = 15f; // Minimum vertical rotation (camera tilt)
    private const float maxPitch = 60f; // Maximum vertical rotation (camera tilt)
    private GameObject selectedObject; // Currently selected object

    private Vector3 targetPosition; // Target position for the camera
    private Vector3 currentVelocity; // Used for smooth dampening
    private float currentZoomDistance = 20f; // Current zoom distance from the target point

    void Start()
    {
        // Set the initial rotation and position of the camera
        transform.rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f); 
        targetPosition = transform.position; // Start at the initial position
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.GetComponent<InputField>() != null)
        {
            // Input field is focused, ignore movement keys
            return;
        }
        if(GameData.Movement == 1){
            HandleCameraMovement();

            HandleCameraRotation();
            HandleCameraZoom();
            HandleSelection();
        }
    }

    void HandleCameraMovement()
    {
        
        // Move using WASD or Arrow Keys (keyboard input)
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down

        // Calculate movement based on  input
        float f(float x, float a)
        {
            return x * x + a * (x * x * x - x * x);
        }
        float Addition = f(transform.position.y, 0.5f) / 10;
        Vector3 movement = (transform.right * horizontal + transform.forward * vertical).normalized * (moveSpeed + Addition/5) * Time.deltaTime;

        // Set target position, keeping the current height
        targetPosition = transform.position + movement;
        targetPosition.y = transform.position.y; // Maintain current height

        // Smoothly move the camera toward the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 0.1f);
        
        if (Input.GetMouseButton(2)) // Right mouse button pressed
        {
            // Mouse movement to adjust position
            float mouseX = -Input.GetAxis("Mouse X") * (CursormoveSpeed + Addition) * Time.deltaTime;
            float mouseY = -Input.GetAxis("Mouse Y") * (CursormoveSpeed + Addition) * Time.deltaTime;

            // Apply mouse movement to camera position while maintaining height
            targetPosition = transform.position + transform.right * mouseX + transform.forward * mouseY;
            targetPosition.y = transform.position.y; // Keep the camera at the same height
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 0.1f);
        }
    }

    void HandleCameraRotation()
    {
        if (Input.GetMouseButton(1)) // Right mouse button pressed
        {
            Input.GetAxis("Mouse X") ;
            Input.GetAxis("Mouse Y");
            // Get mouse input for rotation
            float rotationX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;  // Horizontal (yaw) rotation
            float rotationY = -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime; // Vertical (pitch) rotation

            // Update rotation angles
            currentRotationX += rotationX; // Apply horizontal rotation (yaw)
            currentRotationY += rotationY; // Apply vertical rotation (pitch)

            // Clamp vertical rotation to prevent flipping
            currentRotationY = Mathf.Clamp(currentRotationY, minPitch, maxPitch);

            // Apply rotation to the camera
            transform.rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        }
    }

    void HandleCameraZoom()
    {
        // Zoom in and out using the mouse scroll wheel
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0f)
        {
            // Adjust the zoom direction: zoom in on scroll up (negative scroll) and zoom out on scroll down (positive scroll)
            float zoom = - scroll * zoomSpeed;

            // Smoothly move the camera to the new zoom level using Lerp
            // Ensure the camera moves in or out along its forward direction
            if((zoom > 0 && transform.position.y <= 40) || (zoom < 0 && transform.position.y >= 2)){
                Vector3 desiredPosition = targetPosition - transform.forward * zoom;
                transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.1f);
            }
        }
    }


    void HandleSelection()
    {
        // if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //     // Raycast to check for selectable objects
        //     if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectableLayer))
        //     {
        //         selectedObject = hit.collider.gameObject;
        //         Debug.Log("Selected: " + selectedObject.name);
        //     }
        // }
    }
}
