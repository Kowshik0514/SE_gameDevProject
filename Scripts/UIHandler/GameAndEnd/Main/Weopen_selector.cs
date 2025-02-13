using UnityEngine;
using System.Collections.Generic;  // Required for List
using UnityEngine.SceneManagement;
using System.Collections;
public class SelectorFunctions : MonoBehaviour
{
    // Public list of GameObjects with variable size
    public List<GameObject> WeaponObjectList = new List<GameObject>();
    public List<GameObject> BuffObjectList = new List<GameObject>();

    public List<GameObject> PanelList = new List<GameObject>();

    public GameObject Selector;
    public GameObject Settings;

    int index = 0;
    public GameObject password;
    public GameObject info;


    private int EvaluatePasswordStrength(string password)
    {
        if (string.IsNullOrEmpty(password)) return 0;

        int strength = 0;

        // Length-related scoring
        int maxLengthThreshold = 16;
        int maxStrengthFromLength = 40;
        int lengthScore = Mathf.Min(password.Length, maxLengthThreshold) * 3;
        strength += Mathf.Min(lengthScore, maxStrengthFromLength);

        // Character diversity checks
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-z]")) strength += 10; // Lowercase
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]")) strength += 10; // Uppercase
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]")) strength += 15; // Digits
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[!@#$%^&*()\\,\\.?\:{}|<>~`]")) strength += 20; // Special characters
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"\s")) strength -= 10; // Penalize spaces

        // Sequential patterns
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"(.)\1{2,}")) strength += 10; // Avoid repeated characters
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"123|abc|password|qwerty", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            strength += 15; // Avoid common patterns

        // Mixed-case and diversity bonus
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-z]") && System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]")) 
            strength += 10;
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-zA-Z]") && System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]"))
            strength += 10;

        // Penalty for short length
        if (password.Length < 8) strength -= 13;
        strength = (int)(((float)strength/120f)*100);
        // Ensure the strength is within the valid range
        return Mathf.Clamp(strength, 0, 100);
    }
    public void SetActivePanel(int activePanelIndex)
    {
        // Loop through all panels and deactivate them
        
        foreach (var panel in PanelList)
        {
            panel.SetActive(false);
        }

        // Activate the panel at the specified index
        if (activePanelIndex >= 0 && activePanelIndex < PanelList.Count)
        {
            PanelList[activePanelIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Invalid panel index");
        }
    }

    
    // Function to hide the canvas
    public void CloseSelector()
    {
        if (Selector != null)
        {
            GlobalObject.myGlobalObject.GetComponent<Util>().EnableMovement();
            Selector.gameObject.SetActive(false); // Hides the canvas by disabling its GameObject
        }
        else
        {
            Debug.LogWarning("Target Canvas is not assigned!");
        }
    }
    public void OpenSelector()
    {
        info.SetActive(false);
        CloseSettings();
        if (Selector != null)
        {
            GlobalObject.myGlobalObject.GetComponent<Util>().DisableMovement();
            Selector.gameObject.SetActive(true); // Hides the canvas by disabling its GameObject
        }
        else
        {
            Debug.LogWarning("Target Canvas is not assigned!");
        }
    }
    public void ClosePassword()
    {
        if (password != null)
        {
            GlobalObject.myGlobalObject.GetComponent<Util>().EnableMovement();
            password.gameObject.SetActive(false); // Hides the canvas by disabling its GameObject
        }
        else
        {
            Debug.LogWarning("Target Canvas is not assigned!");
        }
        
    }

    public void OpenSettings()
    {
        Time.timeScale = 0f;
        CloseSelector();
        info.SetActive(false);
        if (Settings != null)
        {
            GlobalObject.myGlobalObject.GetComponent<Util>().DisableMovement();
            Settings.gameObject.SetActive(true); // Hides the canvas by disabling its GameObject
        }
        else
        {
            Debug.LogWarning("Target Canvas is not assigned!");
        }
    }
    public void CloseSettings()
    {
        Time.timeScale = 1f;
        if (Settings != null)
        {
            GlobalObject.myGlobalObject.GetComponent<Util>().EnableMovement();
            Settings.gameObject.SetActive(false); // Hides the canvas by disabling its GameObject
        }
        else
        {
            Debug.LogWarning("Target Canvas is not assigned!");
        }
    }
    public void OpenPassword()
    {
        info.SetActive(false);
        if (password != null)
        {
            GlobalObject.myGlobalObject.GetComponent<Util>().DisableMovement();
            password.gameObject.SetActive(true); // Hides the canvas by disabling its GameObject
        }
        else
        {
            Debug.LogWarning("Target Canvas is not assigned!");
        }
    }

    // This function sets the GameData.building to the object at index 'i' in the WeaponObjectList
    public void SetBuilding(int i)
    {
        
        
        if(GameData.Money < GameData.price[i]){
            GlobalObject.myGlobalObject.GetComponent<Util>().Notify(GameData.Notify[0]);
        }else{
            GameData.sender = i;
            if(i>=0 && i<=4){
                CloseSelector();
                OpenPassword();
                index = i;
            }
            else{
                if(i==0){
                    GameData.buildingToPlace = WeaponObjectList[i];
                }
                if (i >= 0 && i < WeaponObjectList.Count)  // Ensure the index is within range
                {
                    GameData.buildingToPlace = WeaponObjectList[i];
                }
                else
                {

                    Debug.Log("Weapon selector Index out of range!");
                    Debug.Log(i);
                }
                CloseSelector();
            }
            
            
        }
        
        
    }

    public void SetBuff(int i){
        if( GameData.Money >= GameData.BuffPrice[i]){
        GameData.buffToPlace = BuffObjectList[i];
        GameData.buffId = i;
        CloseSelector();
        }
        else{
            GlobalObject.myGlobalObject.GetComponent<Util>().Notify(GameData.Notify[0]);
        }
    }


    public void SetPassword(){
        GameData.buildingToPlace = WeaponObjectList[index];
        GameData.strength = EvaluatePasswordStrength(GameData.password);
        GlobalObject.myGlobalObject.GetComponent<Calling>().set(8);
        GlobalObject.myGlobalObject.GetComponent<Calling>().Notification("Password Set to your gun Learn more...");
        ClosePassword();

    }
    public void play(){
        StartCoroutine(LoadSceneAsync(1));
    }
    public void home(){
        StartCoroutine(LoadSceneAsync(0));
    }
    IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                yield return null;
            }
        }
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    
    void Start()
    {
        // Example usage of adding objects to the list
        // WeaponObjectList.Add(someGameObject);  // Uncomment this line to add objects dynamically
    }

    void Update()
    {
        // Example of how to use SetBuilding function:
        // You can call SetBuilding from UI buttons or based on some condition
        // Example: if (Input.GetKeyDown(KeyCode.Alpha1)) SetBuilding(0);  // Set building to first object in list
    }
}
