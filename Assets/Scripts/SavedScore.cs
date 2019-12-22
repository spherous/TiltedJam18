using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SavedScore : MonoBehaviour
{
    #region Constants

    const string k_savedScoreKey = "SavedScore";
    
    #endregion

    #region Variables

    #endregion

    #region Properties


    #endregion

    #region Construction

    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt(k_savedScoreKey).ToString();
    }

    #endregion
    
    #region Management

    public static void SaveScore(int score)
    {
        PlayerPrefs.SetInt(k_savedScoreKey, score);
    }

    #endregion

}
