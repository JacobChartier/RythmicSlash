using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene(int scene)
    {
        GameManager.Instance.SwitchScene(scene);
    }
}
