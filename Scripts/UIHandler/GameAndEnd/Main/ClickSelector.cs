using UnityEngine;
using UnityEngine.UI;
public class ClickSelector : MonoBehaviour
{
    public GameObject selected;
    public GameObject info;
    public GameObject content;
    public GameObject BuildClick;
    public GameObject InfoLoader;
    int buildActive = 1;

    float upd = 0f;
    float upd_max = 0.2f;



    BuildSelect b;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selected = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)||(Input.GetMouseButtonDown(2)||Input.GetMouseButtonDown(0))){
            if(selected!=null){
                selected.SendMessage("UnSelect", SendMessageOptions.DontRequireReceiver);
            }
            if(buildActive==1)
            Invoke("UnSelectBuild", 0.2f);
        }
        if(GameData.Movement == 1)
        if (Input.GetMouseButtonDown(0))
        {
            
            if(selected!=null){
                selected.SendMessage("UnSelect", SendMessageOptions.DontRequireReceiver);
            }
            // Create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {

                // Check if the hit collider is a trigger, and continue the raycast if so
                if (hit.collider.isTrigger)
                {
                    // Continue the raycast to see if there's an object behind the trigger collider
                    RaycastHit[] allHits = Physics.RaycastAll(ray);

                    // Loop through all the hits and find the first non-trigger collider
                    foreach (RaycastHit h in allHits)
                    {
                        if (!h.collider.isTrigger)
                        {
                            // Log or handle the clicked object behind the trigger
                            Debug.Log("Clicked on: " + h.collider.gameObject.name);

                            selected = h.collider.gameObject;
                            break;  // Stop once you find the first non-trigger object
                        }
                    }
                }
                else
                {
                    // If the hit collider is not a trigger, handle it normally
                    Debug.Log("Clicked on: " + hit.collider.gameObject.name);
                    selected = hit.collider.gameObject;
                }
                if(selected!=null){
                    if(GameData.buildingToPlace != null && selected.GetComponent<ReplaceObjectOnClick>()==null){
                        GlobalObject.myGlobalObject.GetComponent<Util>().Notify(GameData.Notify[3]);
                    }
                    selected.SendMessage("Select", SendMessageOptions.DontRequireReceiver);
                    LoadInfo();
                    
                    SelectProcess(selected);
                }
                
            }
            
        }
        // upd += Time.deltaTime;
        // if(upd >= upd_max){
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit)){
        //         hit.collider.gameObject.SendMessage("Hower", SendMessageOptions.DontRequireReceiver);

        //     }
        //     upd = 0f;
        // }
    }
    void UnSelectBuild(){
        buildActive = 0;
        BuildClick.SetActive(false);
    }

    void SelectProcess(GameObject S){
        if (S.GetComponent<BuildSelect>() != null){
            b = S.GetComponent<BuildSelect>();
            buildActive = 1;
            BuildClick.SetActive(true);
        }
    }

    public void Info(){
        info.SetActive(true);
        // Text itemText = content.GetComponentInChildren<Text>();
        // if (itemText != null)
        // {
        //     itemText.text = "abc";
        // }
    }
    public void Sell(){
        b.Sell();
    }
    public void infoClose(){
        info.SetActive(false);
    }

    public void LoadInfo(){
        InfoLoader.GetComponent<BuildClick>().UpdatePanelInfo(selected);
    }


    
}
