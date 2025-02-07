using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ButtonSoundManager : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private AudioSource audioSource;
        [SerializeField]private AudioClip audioClip,audioMulligan,audioCardPlace,audioShuffleCard;
        [SerializeField]private List<AudioClip> phaseListAudio;
        


        public void PhaseAudioPlayer()
        {
            int indexAudio = Random.Range(0,phaseListAudio.Count);
                audioClip = phaseListAudio[indexAudio];
                audioSource.PlayOneShot(audioClip);
                
        }


        public void ShuffleAudioPlayer()
        {
            audioSource.PlayOneShot(audioShuffleCard);            
        }

        public void CardPlaceAudioPlayer()
            {
                
                 audioSource.PlayOneShot(audioCardPlace);
        
            }

         public void MulliganAudioPlayer()
                {
                    audioSource.PlayOneShot(audioMulligan);
                }

            
    }
}
