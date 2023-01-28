using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        
        public void OnStartButtonClick()
        {
            SceneManager.LoadScene(1);
        }
        
    }
}
