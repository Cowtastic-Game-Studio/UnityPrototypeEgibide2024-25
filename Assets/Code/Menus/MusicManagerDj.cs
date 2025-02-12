using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MusicManagerDj : MonoBehaviour
    {


        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private List<AudioClip> listOfThemes;
        // Start is called before the first frame update
        [SerializeField] int themeSelector = 0;

        // Update is called once per frame
        void Update()
        {


            MusicPlayerTheme(themeSelector);
        }

        public void MusicPlayerTheme(int song)
        {

            if (!audioSource.isPlaying)
            {
                song = Random.Range(0, listOfThemes.Count);
                audioClip = listOfThemes[song];
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}
