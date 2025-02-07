using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class ButtonSoundManager : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private AudioSource audioSource;
        [SerializeField]private AudioClip audioClip,audioMulligan;
        [SerializeField]private List<AudioClip> phaseListAudio;
        private int indexAudioPhase = 0;

        void Update()
        {
        indexAudioPhase = Random.Range(0,phaseListAudio.Count);
            PhaseAudioPlayer(indexAudioPhase);
        }
    
        public void PhaseAudioPlayer(int indexAudio)
        {
                audioClip = phaseListAudio[indexAudio];
                audioSource.PlayOneShot(audioClip);
                
        }
         public void MulliganAudioPlayer()
                {
                    audioSource.PlayOneShot(audioMulligan);
                }
    
    }
}
