using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    void Start()
    {
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
        //SceneManager.LoadScene(2, LoadSceneMode.Additive);
        StartCoroutine(LoadingScenes(0));
        StartCoroutine(LoadingScenes(1));
        
        //await SceneManager.LoadScene(0, LoadSceneMode.Additive);
        //await SceneManager.LoadScene(1, LoadSceneMode.Additive);
        //LightProbes.Tetrahedralize();
    }

    IEnumerator LoadingScenes(int sceneNum)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNum, LoadSceneMode.Additive);

        while (!asyncOperation.isDone)
        {
            print("Loading :"+asyncOperation.progress + "%");
            yield return null;
        }
        LightProbes.Tetrahedralize();
        yield return null;
    }
}
