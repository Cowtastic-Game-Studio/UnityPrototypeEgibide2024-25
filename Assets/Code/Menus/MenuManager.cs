using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CowtasticGameStudio.MuuliciousHarvest
{

    public class MenuManager : MonoBehaviour
    {
        public GameObject pauseMenuObject;
        // Start is called before the first frame update
        private Boolean isPaused = true; 
        PauseUnPause pauseUnPause;



        public void NewGame() 
        {
            SceneManager.LoadSceneAsync("Pruebas3dMenus", LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync("MainMenu", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            PauseUnPause.pause = false;
            
        }
        public void MainMenu()
        {
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync("Pruebas3dMenus", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }
    


        public void GameOverScene() 
        {

            SceneManager.LoadSceneAsync("GameOvers", LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync("MainMenu", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            PauseUnPause.pause = false;
           
        }
        public void MainMenuGameOver()
        {

            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync("GameOver", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            
        }

        public void ResumeGame()
            {
            pauseMenuObject.SetActive(false);            
            PauseUnPause.pause = false;
            }
     
        public void ExitGame()
        {
            Application.Quit();

        }
     
    }
}
