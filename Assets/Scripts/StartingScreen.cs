using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScreen : MonoBehaviour
{
    #region Constants


    #endregion

    #region Variables

    [SerializeField]
    string m_sceneToLoad = null;

    #endregion

    #region Properties


    #endregion

    #region Construction

    void Start()
    {
        
    }

    #endregion

    #region Update

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else
        if (Input.anyKey)
        {
            SceneManager.LoadScene(m_sceneToLoad);
        }
    }

    #endregion

    #region Management


    #endregion

}
