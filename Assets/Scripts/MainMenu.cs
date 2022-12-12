using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickStart()
    {
        Debug.Log("Start");
    }

    public void OnClickInfo()
    {
        Debug.Log("Info");
    }

    public void OnClickMessages()
    {
        Debug.Log("Messages");
    }

    public void OnClickGit()
    {
        Debug.Log("Git");
    }

    public void OnClickSetting()
    {
        Debug.Log("Setting");
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
