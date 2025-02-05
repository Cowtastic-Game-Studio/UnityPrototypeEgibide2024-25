using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class PauseUnPause : MonoBehaviour
    {
        public GameObject pauseMenuObject;
        // Update is called once per frame
        public static bool pause = false;



        public void PauseMenuOff()
        {

            pauseMenuObject.SetActive(false);

        }
        public void PauseMenuOn()
        {

            pauseMenuObject.SetActive(true);

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {


                if (pause == false)
                {
                    PauseMenuOn();
                    pause = true;


                }


            }
            
            
        }
    }
}
