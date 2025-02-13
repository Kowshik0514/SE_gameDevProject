using UnityEngine;

public class ClickScript : MonoBehaviour
{
    // Reference to the static GameObject to handle clicks
    public GameObject StaticGameObject;

    void Update()
    {
        // Check for mouse left-click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Get the clicked GameObject
                GameObject clickedObject = hit.collider.gameObject;

                Debug.Log("Clicked on: " + clickedObject.name);

                // Pass the clicked object to StaticGameObject
                if (StaticGameObject != null)
                {
                    if (StaticGameObject.TryGetComponent<BuildClick>(out var buildClick))
                    {
                        buildClick.OnBuildingClicked(clickedObject);
                    }
                    else
                    {
                        Debug.LogWarning("StaticGameObject does not have a BuildClick component.");
                    }
                }
                else
                {
                    Debug.LogWarning("StaticGameObject is not set.");
                }
            }
            else
            {
                if (StaticGameObject.TryGetComponent<BuildClick>(out var buildClick))
                {
                    Debug.Log("Came here ray cast did not detect any click");
                    buildClick.OnBuildingClicked(null);
                }
                else
                {
                    Debug.LogWarning("StaticGameObject does not have a BuildClick component.");
                }
            }
        }
    }
}
