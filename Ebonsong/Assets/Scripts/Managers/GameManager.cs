using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerLives = 3;

    public bool DamageWithSwordMethed1
    {
        get;
        private set;
    }
    public static GameManager Instance
    {
        get;
        private set;
    }

    private bool isCurrentlyPasued;
    private GameObject pausedMenuObject;
    private PlayerInputActions inputActions;

    private void Start()
    {
        pausedMenuObject = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        DamageWithSwordMethed1 = false;
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

    public void ChoseMethed1()
    {
        DamageWithSwordMethed1 = true;
    }

    public void ChoseMethed2()
    {
        DamageWithSwordMethed1 = false;
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
