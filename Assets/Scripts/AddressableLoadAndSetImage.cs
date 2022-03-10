using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;


public class AddressableLoadAndSetImage : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------------------

    #region references

    [SerializeField]
    Image imageComponent;

    [SerializeField]
    string assetPath;

    #endregion

    //--------------------------------------------------------------------------------------------------------------------------------------

    #region logic

    private void Start()
    {
        Addressables.LoadAssetAsync<Sprite>(assetPath).Completed += OnLoadDone;
    }

    private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> obj)
    {
        if (obj.Result != null)
        {
            // In a production environment, you should add exception handling to catch scenarios such as a null result.
            imageComponent.sprite = obj.Result;
            imageComponent.preserveAspect = true;
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------------------------------
}
