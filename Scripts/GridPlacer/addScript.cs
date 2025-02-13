using UnityEngine;

public class ReplaceObjectOnClick : MonoBehaviour
{
    // Variables to be set in the Inspector
    public GameObject objectToPlace;  // The object that will replace the current one
    public GameObject PlaceEffect;
    Treasury t;
    public int CanBePlaced = 1;
    // Function that is called when the mouse is clicked on the GameObject
    void Start(){
        t = GlobalObject.myGlobalObject.GetComponent<Treasury>();
    }
    

    void Select()
    {
        
        if(CanBePlaced==1 && GameData.sender >= 0 && GameData.sender <= 6 && GameData.buildingToPlace != null){
        // Check if the object to place is set
            if(GameData.Money < GameData.price[GameData.sender]){
                
                GlobalObject.myGlobalObject.GetComponent<Util>().Notify(GameData.Notify[0]);
            }
            else{
                CanBePlaced = 0;
                objectToPlace = GameData.buildingToPlace;
                GameData.Money -= GameData.price[GameData.sender];
                if (objectToPlace != null)
                {
                    // Log that the object is being replaced
                    Debug.Log("Replacing object: " + gameObject.name);

                    // Call the function to replace the object
                    ReplaceObject();
                    GameData.buildingToPlace = null;
                }
                else
                {
                    Debug.Log("Object to place is not assigned in GameData.");
                }
            }
        }else{
            if(CanBePlaced==0)
            GlobalObject.myGlobalObject.GetComponent<Util>().Notify(GameData.Notify[3]);
        }
    }

    // Function to remove the current object and instantiate the new one
    void ReplaceObject()
    {
        // Get the position and rotation of the current object
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // Create an empty GameObject as the parent of the new object
        GameObject emptyParent = new GameObject("EmptyParent");

        // Set the position and rotation of the empty parent GameObject to match the original object's position and rotation
        emptyParent.transform.position = position;
        emptyParent.transform.rotation = rotation;
        var Place = Instantiate(PlaceEffect, emptyParent.transform);
        
        // Instantiate the new object as a child of the empty parent
        GameObject newObject = Instantiate(objectToPlace, emptyParent.transform);
            // Debug.Log(GameData.sender);
        if(GameData.sender>4){
            if(GameData.sender==6){
                t.AddReceiver(newObject);
                
            }else{
                t.AddSender(newObject);
            }
        }
        else{
            // Map.AddMapping(GameData.password, newObject);

        }
        // Ensure the new object (the child) inherits the correct transform
        newObject.transform.localPosition = Vector3.zero;  // Reset local position so it matches the parent
        newObject.transform.localRotation = Quaternion.identity;  // Reset local rotation so it matches the parent
        newObject.GetComponent<BuildSelect>().buildingHolder = gameObject;
        newObject.GetComponent<BuildSelect>().id = GameData.sender;
        emptyParent.transform.rotation = new Quaternion(0, 0, 0, 0);
        if(GameData.sender <=4){
            Map.AddMapping(GameData.password, newObject);
            newObject.GetComponent<Building>().password = GameData.password;
        }
        Destroy(Place, 0.4f);
        // Destroy the current object (this removes the original object from the scene)
        // Destroy(gameObject);
    }
}
  