using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    public void SwitchScene(int scene)
    {
        GameManager.Instance.SwitchScene(scene);
    }

    public void ShowPauseMenu()
    {
        if(gameObject.CompareTag("PauseMenu"))
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
