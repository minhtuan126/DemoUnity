using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class LoginSceneUI : MonoBehaviour
{
    public Slider loadingSlider;

    // Start is called before the first frame update
    void Start()
    {
        LoadHomeScene().Forget();
    }

    private async UniTask LoadHomeScene()
    {
        // fake
        await UniTask.Delay(2000);
        LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneBuildIn.Home);
    }


    private void Update()
    {
        const float time = 2;
        // fake loading
        loadingSlider.value += Time.deltaTime / time;
    }
}
