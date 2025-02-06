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
            //Time.timeScale = 1;

        }
        public void PauseMenuOn()
        {

            pauseMenuObject.SetActive(true);
            //Time.timeScale = 0;

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pause ==false) 
            {


                
                    PauseMenuOn();
                    pause = true;


                }
            else if (Input.GetKeyDown(KeyCode.Escape)&& pause==true)
            {



                PauseMenuOff();
                pause = false;


            }


        }
            
            
        }
    }

