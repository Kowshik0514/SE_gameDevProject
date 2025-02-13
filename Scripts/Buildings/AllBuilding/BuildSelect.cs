using UnityEngine;

public class BuildSelect : MonoBehaviour
{
    public int id = 0;
    public GameObject buildingHolder;
    public void Sell(){
        GameData.Money += (int)(GameData.price[id] * 0.9f);
        GlobalObject.myGlobalObject.GetComponent<Util>().Notify(AddRandomValue(GameData.Notify[1]));
        buildingHolder.GetComponent<ReplaceObjectOnClick>().CanBePlaced = 1;
        Destroy(gameObject);
    }
    public void Destroy_(){
        GlobalObject.myGlobalObject.GetComponent<Util>().Notify(GameData.Notify[2]);
        buildingHolder.GetComponent<ReplaceObjectOnClick>().CanBePlaced = 1;
        Destroy(gameObject);
    }
    public string AddRandomValue(string input)
    {
        float randomValue = Random.Range(0.01f, 0.5f); // Generate random float between 0.01 and 0.5
        return input + randomValue.ToString("F2") + "s"; // Append the random value and "s"
    }
}
