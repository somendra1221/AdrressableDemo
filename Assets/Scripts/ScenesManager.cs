using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using System;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    //--------------------------------------------------------------------------------------------------------------------------------------

    #region references

    [SerializeField]
    TextMeshProUGUI headingText;

    [SerializeField]
    Button loadScene1Button, loadScene2Button, loadScene3Button, loadScene4Button;

    [SerializeField]
    public string sceneOnePath, sceneTwoPath, sceneThreePath, sceneFourPath;

    AsyncOperationHandle<SceneInstance> handle;

    AsyncOperationHandle<SceneInstance> sceneLoadingInstance;
    bool isSceneLoading = false;

    #endregion

    //--------------------------------------------------------------------------------------------------------------------------------------

    #region intialization

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        headingText.text = "Splash scene";
        loadScene1Button.onClick.AddListener(() => { LoadSceneOne(); });
        loadScene2Button.onClick.AddListener(() => { LoadSceneTwo(); });
        loadScene3Button.onClick.AddListener(() => { LoadSceneThree(); });
        loadScene4Button.onClick.AddListener(() => { LoadSceneFour(); });
    }

    private void Update()
    {
        if (isSceneLoading)
        {
            print($"sceneLoadingInstance.PercentComplete : {sceneLoadingInstance.PercentComplete}");
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------------------------------

    #region scene loading and unloading

    public void LoadSceneOne()
    {
        LoadScene(sceneOnePath);
    }

    public void LoadSceneTwo()
    {
        LoadScene(sceneTwoPath);
    }

    public void LoadSceneThree()
    {
        LoadScene(sceneThreePath);
    }

    public void LoadSceneFour()
    {
        LoadScene(sceneFourPath);
    }

    private void LoadScene(string sceneName)
    {
        UnLoadScene();
        sceneLoadingInstance = Addressables.LoadSceneAsync(sceneName);
        isSceneLoading = true;
        sceneLoadingInstance.Completed += SceneLoadCompleted;
    }

    private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        isSceneLoading = false;
        handle = obj;
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            headingText.text = obj.Result.Scene.name;
            print($"<color=green> {obj.Result.Scene.name} successfully loaded. </color>");
        }
    }

    private void UnLoadScene()
    {
        try
        {
            Addressables.UnloadSceneAsync(handle, true).Completed += operation =>
            {
                if (operation.Status == AsyncOperationStatus.Succeeded)
                {
                    print($"Successfully unloaded {handle.Result.Scene.name} scene");
                }
            };
        }
        catch (Exception exception)
        {
            print($"UnLoadScene :: exception : {exception}");
        }

    }


    #endregion

    //--------------------------------------------------------------------------------------------------------------------------------------
}
