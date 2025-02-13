using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Util : MonoBehaviour
{
    public GameObject NotificationPanel; // Drag your panel here in the Inspector
    public TextMeshProUGUI NotificationText; // Drag your TextMeshProUGUI here in the Inspector
    public GameObject BuildingHolder;
    [SerializeField] private GameObject InfoLoader;

    public GameObject computer;
    public GameObject canvas;
    public GameObject Enemy;
    public GameObject Explosion;



    

    private Coroutine fadeCoroutine; // Reference to the running coroutine

    public void Notify(string s)
    {
        NotificationPanel.SetActive(true);
        NotificationText.text = s;

        // Stop the existing coroutine if it's running
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        // Start a new fade coroutine
        fadeCoroutine = StartCoroutine(FadeOutNotification());
    }

    private IEnumerator FadeOutNotification()
    {
        CanvasGroup canvasGroup = NotificationPanel.GetComponent<CanvasGroup>();

        // Ensure the panel has a CanvasGroup for fading
        if (canvasGroup == null)
        {
            canvasGroup = NotificationPanel.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 1; // Start fully visible
        float fadeDuration = 1.5f;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - (t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
        NotificationPanel.SetActive(false); // Hide the panel after fading out

        // Clear the reference to the coroutine
        fadeCoroutine = null;
    }
    public void PlaceBuff(GameObject g){
        int i = GameData.buffId;
        if( GameData.Money >= GameData.BuffPrice[i]){
            GameData.Money -= GameData.BuffPrice[i];
            var Place = Instantiate(GameData.buffToPlace, g.transform);
            Destroy(Place, GameData.BuffTime[GameData.buffId]);
            if(i==0 || i==2 || i==4){
                g.GetComponent<Building>().ApplyShield(GameData.BuffTime[GameData.buffId]);
            }else{
                g.GetComponent<Building>().ApplyHealthBuff(GameData.BuffEffect[GameData.buffId], GameData.BuffTime[GameData.buffId]);
            }
            GameData.buffToPlace = null;
            GameData.buffId = -1;
        }
       
    }
    
    public void DisableMovement(){
        GameData.Movement = 0;
    }
    public void EnableMovement(){
        GameData.Movement = 1;
        
    }
    public void NotifyClick(int id){
        Debug.Log(id);
        InfoLoader.GetComponent<BuildClick>().UpdatePanelInfo(id);
        gameObject.GetComponent<ClickSelector>().Info();
    }

    public void DestroyAll(){
        SoundManager.Instance.PlayAudioByIndex(11);
        Instantiate(Explosion);
        canvas.SetActive(false);
        computer.SetActive(false);
        Enemy.SetActive(false);
        Invoke("LoadEnd", 4f);
    }
    void LoadEnd(){
        StartCoroutine(LoadSceneAsync(2));
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


}
