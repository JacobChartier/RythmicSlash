using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void SwitchScene(int scene)
    {
        GameManager.Instance.SwitchScene(scene);
    }

    public void ShowPauseMenu()
    {

    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
