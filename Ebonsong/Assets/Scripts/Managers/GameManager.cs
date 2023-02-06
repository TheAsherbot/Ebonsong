using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerLives = 3;
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
        Application.targetFrameRate = 60;
        pausedMenuObject = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
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

        DestroyAllCollectables();
    }

    public void RestartLevel()
    {
        Collectables[] collectables = FindObjectsOfType<OneUp>();

        for (int i = 0; i < collectables.Length; i++)
        {
            Destroy(collectables[i].gameObject);
        }

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(scene.buildIndex);
    }

    public void GameOver()
    {
        Collectables[] collectables = FindObjectsOfType<Collectables>();

        for (int i = 0; i < collectables.Length; i++)
        {
            Destroy(collectables[i].gameObject);
        }

        SceneManager.LoadScene(0);
    }

    private void DestroyAllCollectables()
    {
        OneUp[] oneUps = FindObjectsOfType<OneUp>();

        for (int i = 0; i < oneUps.Length; i++)
        {
            Destroy(oneUps[i].gameObject);
        }
        
        CoinFive[] coinFive = FindObjectsOfType<CoinFive>();

        for (int i = 0; i < coinFive.Length; i++)
        {
            Destroy(coinFive[i].gameObject);
        }
        
        Coin[] Coin = FindObjectsOfType<Coin>();

        for (int i = 0; i < Coin.Length; i++)
        {
            Destroy(Coin[i].gameObject);
        }
        
        Heal[] Heal = FindObjectsOfType<Heal>();

        for (int i = 0; i < Heal.Length; i++)
        {
            Destroy(Heal[i].gameObject);
        }
        
        HealThree[] HealThree = FindObjectsOfType<HealThree>();

        for (int i = 0; i < HealThree.Length; i++)
        {
            Destroy(HealThree[i].gameObject);
        }
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
