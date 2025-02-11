using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Message : MonoBehaviour
    {
        // Asigna estas referencias en el inspector (o bien, b�squelas en Start)
        public TextMeshProUGUI messageText;       // Referencia al componente Text que muestra el mensaje
        public Image backgroundImage;  // Referencia al componente Image del fondo (para si deseas cambiar su color v�a c�digo)


        // Tiempo que la notificaci�n permanecer� en pantalla (en segundos)
        public float displayDuration = 3f;

        // M�todo para asignar el mensaje
        public void SetText(string text)
        {
            if (messageText != null)
            {
                messageText.text = text;
            }
        }

        // Inicia la cuenta regresiva para destruir la notificaci�n
        void Start()
        {
            StartCoroutine(HideAfterDelay());
        }

        IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(displayDuration);
            // Puedes agregar aqu� animaciones de salida si lo deseas
            Destroy(gameObject);
        }
    }

}
