using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class OverCanvas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
}
