using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : SingletonPersistent<LoadSceneManager>
{
    public enum SceneBuildIn
    {
        Login = 0,
        Home = 1,
        Game = 2
    }
    /// <summary>
    /// string -> GUID of scene AssetReference
    /// </summary>
    private Dictionary<int, AsyncOperation> loadedScenes = new Dictionary<int, AsyncOperation>();

    private void Start()
    {
        // gameObject.SetActive(false);
    }

    public void LoadScene(SceneBuildIn targetScene)
    {
        gameObject.SetActive(true);
        var unloadScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneAsync((int)targetScene));
    }

    private IEnumerator LoadSceneAsync(int scenetargetID)
    {
        Loading.Instance.enabled = true;
        var delay = Loading.Instance.FadeIn();
        yield return new WaitForSeconds(delay);

        var sceneInstanceOperation = SceneManager.LoadSceneAsync(scenetargetID);
        sceneInstanceOperation.allowSceneActivation = true;

        loadedScenes[scenetargetID] = sceneInstanceOperation;

        // await sceneInstanceOperation;
        while (sceneInstanceOperation.progress < 0.9f)
        {
            Loading.Instance.UpdateStatus(sceneInstanceOperation.progress);
            yield return null;
        }

        Loading.Instance.UpdateStatus(1);

        yield return new WaitForSeconds(Loading.Instance.FadeOut());
        Loading.Instance.enabled = false;

        OnFinishedLoading();
    }

    private void OnFinishedLoading()
    {
        // gameObject.SetActive(false);
    }

}

