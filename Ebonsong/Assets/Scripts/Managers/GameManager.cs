using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set;
    }
    public int playerLives = 3;

    [SerializeField] private GameObject pausedMenuObject;

    private bool isCurrentlyPasued;
    private PlayerInputActions inputActions;

    private void Start()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        inputActions = new PlayerInputActions();
        inputActions.GlobleKeys.Enable();
        inputActions.GlobleKeys.PauseUnpause.performed += PauseUnpause_performed;
    }

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(scene.buildIndex);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    private void PauseUnpause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isCurrentlyPasued)
        {
            Time.timeScale = 1.0f;
            pausedMenuObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0.0f;
            pausedMenuObject.SetActive(true);
        }

        isCurrentlyPasued = !isCurrentlyPasued;
    }

}
