using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Go to messages Button (Function undecided)
    public void OnClickMessages()
    {
        Debug.Log("Messages");
    }

    // Go to git Button
    public void OnClickGit()
    {
        Debug.Log("Git");
    }

    // Exit Button
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
