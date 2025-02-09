using UnityEngine;
using UnityEngine.SceneManagement;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject pauseMenuObject;
        private bool isPaused = false;

        private static MenuManager instance;

        public static MenuManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<MenuManager>(); // Buscar instancia en la escena
                    if (instance == null)
                    {
                        Debug.LogError("No hay un MenuManager en la escena.");
                    }
                }
                return instance;
            }
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject); // Evitar duplicados en la misma escena
                return;
            }
        }

        void Start()
        {
            if (pauseMenuObject == null)
            {
                pauseMenuObject = GameObject.Find("PauseMenu");
            }

            if (pauseMenuObject != null)
            {
                pauseMenuObject.SetActive(false);
            }
        }

        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        TogglePause();
        //    }
        //}

        public void TogglePause()
        {
            isPaused = !isPaused;
            if (pauseMenuObject != null)
            {
                pauseMenuObject.SetActive(isPaused);
            }
            Time.timeScale = isPaused ? 0 : 1;
        }

        public void NewGame()
        {
            SceneManager.LoadScene("Muulicious Harvest", LoadSceneMode.Single);
            isPaused = false;
            Time.timeScale = 1;
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            isPaused = false;
            Time.timeScale = 1;
        }

        public void GameOverScene()
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            isPaused = false;
            Time.timeScale = 1;
        }

        public void ResumeGame()
        {
            isPaused = false;
            if (pauseMenuObject != null)
            {
                pauseMenuObject.SetActive(false);
            }
            Time.timeScale = 1;
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Detiene el juego en el Editor
#else
        Application.Quit(); // Cierra la aplicación en compilación
#endif
        }

    }
}
