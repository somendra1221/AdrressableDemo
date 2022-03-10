using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryOnLoadScript : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------------------

    #region logic

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------------------------------
}
