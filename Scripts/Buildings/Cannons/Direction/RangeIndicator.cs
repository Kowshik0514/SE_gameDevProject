using System.Collections;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    private LevelManager manager;
    private int range;
    public int id = 0;
    // Sets range value as specified
    public void SetLevel(int level)
    {
        if (level <= 4 && level >= 0)
        {
            this.range = manager.gameDataList[level].gunRadius;
        }
        else
        {
            Debug.Log("Level Index out of bounds in Range Indicator Script!!");
        }

        gameObject.transform.localScale = new Vector3(range, gameObject.transform.localScale.y, range);
    }

    public void enableCanvas() {
    // Get the child object and access its Canvas component
        Canvas canvas = gameObject.transform.GetChild(0).GetComponent<Canvas>(); // 0 is the index of the child
        if (canvas != null) {
            canvas.enabled = true;
        }
    }

    public void disableCanvas() {
        // Get the child object and access its Canvas component
        Canvas canvas = gameObject.transform.GetChild(0).GetComponent<Canvas>(); // 0 is the index of the child
        if (canvas != null) {
            canvas.enabled = false;
        }
    }


    // At start, initializes range and sets scale
    void Start()
    {
        gameObject.transform.position = new Vector3(transform.parent.position.x,transform.parent.position.y-1.333f,transform.parent.position.z);
        Vector3 parentScale = gameObject.transform.parent.localScale;
        gameObject.transform.localScale = new Vector3(20 / parentScale.x, 1, 20 / parentScale.z);
        

        this.range = 100;
        // gameObject.transform.localScale = new Vector3(this.range, gameObject.transform.localScale.y, this.range);
        gameObject.SetActive(false);
    }

    // void Update(){
    //     gameObject.transform.localScale = new Vector3(this.range, gameObject.transform.localScale.y, this.range);
    // }
    public int SetRange(int value){
        gameObject.SetActive(true);
        this.range = value;  // Set the range value
        // gameObject.transform.localScale = new Vector3(this.range, gameObject.transform.localScale.y, this.range); // Update the scale
        
        Debug.Log($"This is range -==================={this.range}"); // Log the updated range
        
        // Optionally, if you need the object to stay active temporarily to see changes, you can use:
        
        
        // If the object needs to be deactivated after updating, you can keep this line at the end
        
        Invoke("DisableRange", 0.5f);
        return 1;  // Return 1 as an indicator (if needed)
    }
    void DisableRange(){
         gameObject.SetActive(false);
    }

    // Continuously updates the position to follow the target object

}
