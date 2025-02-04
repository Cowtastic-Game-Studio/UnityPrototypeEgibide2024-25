using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MessageManager : MonoBehaviour
    {
        [SerializeField] private GameObject messagePanel;
        [SerializeField] private List<Message> messagesList;


        // Start is called before the first frame update
        void Start()
        {
            messagePanel.SetActive(false);
            messagesList = new List<Message>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddMessage(string message, float duration)
        {
            messagesList.Add(new Message(message, duration));
            if (!messagePanel.activeInHierarchy)
            {
                DisplayMessage();
            }
        }

        public void DisplayMessage()
        {
            if (messagesList.Count > 0)
            {
                messagePanel.SetActive(true);
                messagePanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = messagesList[0].message;
                Invoke("HideMessage", messagesList[0].duration);
            }
        }
    }
}
