using UnityEngine;

public class BuildingCickSelector : MonoBehaviour
{
    public GameObject h;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        // UnSelect();
        Invoke("UnSelect", 0.5f);

    }
    void Select(){
        h.SetActive(true);
        if(GameData.buffToPlace != null){
            GlobalObject.myGlobalObject.GetComponent<Util>().PlaceBuff(gameObject);
        }
    }
    void UnSelect(){
        h.SetActive(false);
    }
}

