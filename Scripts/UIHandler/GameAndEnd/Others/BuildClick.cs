using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildClick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject SellInfoButtonsPannel;
    [SerializeField] private GameObject InfoPanel;

    [SerializeField] private GameObject QandAPannel;
    


    
    void Start(){
        if(SellInfoButtonsPannel.IsUnityNull() || InfoPanel.IsUnityNull()){
            Debug.LogWarning("Panel Is Not Attached to Static Game Object.. Please Attach It..");
            return;
        }
        SellInfoButtonsPannel.SetActive(false);
        InfoPanel.SetActive(false);
        
        if (SellInfoButtonsPannel.transform.Find("Info").TryGetComponent<Button>(out var infoButton))
        {
            Debug.Log("Info Button Found");
            infoButton.onClick.AddListener(OnInfoButtonClick);
        }
        else
        {
            Debug.LogError("Info button not found! in BuildClick Script");
        }
    }
    public void OnInfoButtonClick()
    {
        InfoPanel.SetActive(true);
        SellInfoButtonsPannel.SetActive(false);
    }

    public void OnCloseInfoButtonClick()
    {
        InfoPanel.SetActive(false);
        SellInfoButtonsPannel.SetActive(false);
    }

    public void OnCloseCyberSecClick()
    {
        QandAPannel.SetActive(false);
    }

    internal void OnBuildingClicked(GameObject clickedObject)
    {
        if(SellInfoButtonsPannel.IsUnityNull() || InfoPanel.IsUnityNull()){
            Debug.LogWarning("Panel Is Not Attached to Static Game Object.. Please Attach It..");
            return;
        }

        if(clickedObject.IsUnityNull()){
            SellInfoButtonsPannel.SetActive(false);
            return;
        }

        SellInfoButtonsPannel.SetActive(true);

        Camera.main.GetComponent<ClickScript>().enabled = false;

        UpdatePanelInfo(clickedObject);
        // UpdatePanelInfo2(L);

    }

    public void UpdatePanelInfo(GameObject clickedObject)
    {
        // Retrieve BuildInfo from the clicked object
        Building buildInfo = clickedObject.GetComponent<Building>();
        if (buildInfo == null)
        {
            return;
        }
        string horizontalLine = "<size=50>────────────────────────────────</size>"; 

        string Name = buildInfo.name;
        int buildId = buildInfo.id;

        // Retrieve the defense description using the ID
        string defenseDescription = JsonLoader.Instance.GetDescriptionById(buildId, "defense");

        // Get the TMP_Text component from the InfoPanel
        TMP_Text infoText = InfoPanel.transform
            .Find("Body/Scroll View/Viewport/Content")
            ?.GetComponent<TMP_Text>();

        if (infoText == null)
        {
            Debug.LogError("InfoText component not found in the InfoPanel.");
            return;
        }

        // Check if the description is available
        if (string.IsNullOrEmpty(defenseDescription))
        {
            Debug.LogWarning("Defense description is empty or null.");
            return;
        }

        // Build the final display text with larger font sizes and spacing
        string displayText = 
            $"\n"+
            $"<size=56><b>Name</b></size>\n{horizontalLine}\n<size=40>{Name}</size>\n\n" +  // Large font for Name
            $"<size=56><b>Description</b></size>\n{horizontalLine}\n<size=40>{defenseDescription}</size>"; // Large font for Description

        // Update the InfoPanel text
        infoText.text = displayText;

        // Log the update status
        Debug.Log("InfoPanel updated successfully with enhanced styling.");
    }

    public void UpdatePanelInfo(int id)
    {
        // Retrieve BuildInfo from the clicked object
        // if (buildInfo == null)
        // {
        //     return;
        // }
        string horizontalLine = "<size=50>────────────────────────────────</size>"; 
        int buildId = id;
        string type = "attack";
        if(id>=100){
            buildId-=100;
            type = "defense";
        }
        
        // Retrieve the defense description using the ID
        string defenseDescription = JsonLoader.Instance.GetDescriptionById(buildId, type);
        string Name = JsonLoader.Instance.GetNameById(buildId, type);

        // Get the TMP_Text component from the InfoPanel
        TMP_Text infoText = InfoPanel.transform
            .Find("Body/Scroll View/Viewport/Content")
            ?.GetComponent<TMP_Text>();

        if (infoText == null)
        {
            Debug.LogError("InfoText component not found in the InfoPanel.");
            return;
        }

        // Check if the description is available
        if (string.IsNullOrEmpty(defenseDescription))
        {
            Debug.LogWarning("Defense description is empty or null.");
            return;
        }

        // Build the final display text with larger font sizes and spacing
        string displayText = 
            $"\n"+
            $"<size=56><b>Name</b></size>\n{horizontalLine}\n<size=40>{Name}</size>\n\n" +  // Large font for Name
            $"<size=56><b>Description</b></size>\n{horizontalLine}\n<size=40>{defenseDescription}</size>"; // Large font for Description

        // Update the InfoPanel text
        infoText.text = displayText;

        // Log the update status
        Debug.Log("InfoPanel updated successfully with enhanced styling.");
    }
    private void UpdatePanelInfo2(List<string> headers, List<string> descriptions)
    {
        // List<string> headers,descriptions;
        if (headers == null || descriptions == null || headers.Count != descriptions.Count)
        {
            Debug.LogError("Headers and descriptions must be non-null and of the same length.");
            return;
        }
        // headers = new List<string>();
        // headers.Add("Head1");
        // headers.Add("Head2");
        // headers.Add("Head3");
        // headers.Add("Head4");
        // headers.Add("Head5");
        // headers.Add("Head6");
        // headers.Add("Head7");
        // headers.Add("Head8");
        // headers.Add("Head9");
        // headers.Add("Head10");
        // descriptions = new List<string>();
        // descriptions.Add("Descrp1");
        // descriptions.Add("Descrp2");
        // descriptions.Add("Descrp3");
        // descriptions.Add("Descrp4");
        // descriptions.Add("Descrp5");
        // descriptions.Add("Descrp6");
        // descriptions.Add("Descrp7");
        // descriptions.Add("Descrp8");
        // descriptions.Add("Descrp9");
        // descriptions.Add("Descrp10");
        // Simulated horizontal line
        string horizontalLine = "<size=50>───────────────────────────────────</size>";

        // Get the TMP_Text component from the InfoPanel
        TMP_Text infoText = InfoPanel.transform
            .Find("Body/Scroll View/Viewport/Content")
            ?.GetComponent<TMP_Text>();

        if (infoText == null)
        {
            Debug.LogError("InfoText component not found in the InfoPanel.");
            return;
        }

        // Build the display text dynamically
        string displayText = "";

        for (int i = 0; i < headers.Count; i++)
        {
            // Add a newline at the top only for the first header-description pair
            if (i == 0)
            {
                displayText += "\n";
            }

            // Append header and description with formatting
            displayText += 
                $"<size=56><b>{headers[i]}</b></size>\n{horizontalLine}\n<size=40>{descriptions[i]}</size>\n\n";
        }

        // Update the InfoPanel text
        infoText.text = displayText;

        // Log the update status
        Debug.Log("InfoPanel updated successfully with dynamic headers and descriptions.");
    }



}
