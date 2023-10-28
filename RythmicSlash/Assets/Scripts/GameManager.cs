using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameMode gameMode { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchScene("MainMenu");
        }
    }


    public void ChangeGameMode(GameMode mode)
    {
        gameMode = mode;
    }

    public void SwitchScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void SwitchScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}

public enum GameMode
{
    PLAY,
    PAUSE
}
