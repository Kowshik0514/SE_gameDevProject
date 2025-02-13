using UnityEngine;
using System.Collections;
public class Building : MonoBehaviour
{

    public int health = 10000;
    public int maxHealth = 10000;
    public string name;
    public int id = 1;
    public string password = "";
    public GameObject explosion;
    public int shield = 0; 
    HelthHolder childScript;
    public int Computer = 0;
    public void attacked(int damage){
        
        if(shield == 0)
        health -= damage;
        if(this.health<=0){
        // Destroy(gameObject);
        
        

        if(Computer == 1){
            GlobalObject.myGlobalObject.GetComponent<Util>().DestroyAll();
        }
        var end = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        Debug.Log(password);
        if(id<=4)
        Map.DestroyAll(password);
        if(gameObject != null)
        gameObject.GetComponent<BuildSelect>().Destroy_();
        Destroy(end, 2f);
        }
        if(childScript==null){
            childScript = GetComponentInChildren<HelthHolder>();
        }
        childScript.UpdateHealth((health * 100)/maxHealth);
        
        
    }
    public void Destroy_Ani(){
        var end = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        gameObject.GetComponent<BuildSelect>().Destroy_();
        Destroy(end, 2f);
    }
    public void ApplyShield(float x){
        shield = 1;
        Invoke("RemoveShield", x);
        GlobalObject.myGlobalObject.GetComponent<Calling>().set(100+(int)x);
        GlobalObject.myGlobalObject.GetComponent<Calling>().Notification("Buff Applied (Click me To know more!)");
    }
    public void RemoveShield(){
        shield = 0;
    }

    public void ApplyHealthBuff(int healthIncreasePerSecond, float duration)
{
    // Start the buff coroutine
    StartCoroutine(HealthBuffCoroutine(healthIncreasePerSecond, duration));
}

private IEnumerator HealthBuffCoroutine(int healthIncreasePerSecond, float duration)
{
    float elapsedTime = 0;

    while (elapsedTime < duration)
    {
        // Increase health, ensuring it does not exceed maxHealth
        health = Mathf.Min(health + healthIncreasePerSecond, maxHealth);

        // Update health UI or other components
        if (childScript != null)
        {
            childScript.UpdateHealth((health * 100) / maxHealth);
        }

        // Wait for one second
        yield return new WaitForSeconds(1);
        elapsedTime += 1f;
    }
}
    public void init(int h){
        health = h;
        if(maxHealth!=0)
        maxHealth = h;
        else
        maxHealth = 1;
        if(childScript==null){
            childScript = GetComponentInChildren<HelthHolder>();
        }
        childScript.UpdateHealth((health * 100)/maxHealth);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Computer==1){
            health = GameData.Health;
            maxHealth = GameData.Health;
        }
        childScript = GetComponentInChildren<HelthHolder>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Computer==1) GameData.Health = health;
    }
}
