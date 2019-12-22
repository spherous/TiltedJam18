using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    #region Display

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;

        if(Application.isPlaying)
        {
            if(GUILayout.Button("End"))
            {
                gameManager.EndGame();
            }
        }

    }

    #endregion
}
