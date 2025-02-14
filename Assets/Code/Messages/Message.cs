using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    // Referencia al componente TextMeshProUGUI que mostrar� el mensaje.
    public TextMeshProUGUI messageText;

    // (Opcional) Referencia al componente Image del fondo, por si deseas modificar su color v�a c�digo.
    public Image backgroundImage;

    // Tiempo (en segundos) que la notificaci�n permanecer� en pantalla.
    public float displayDuration = 3f;

    // M�todo para asignar el texto del mensaje.
    public void SetText(string text)
    {
        if (messageText != null)
        {
            messageText.text = text;
        }
    }

    // Se inicia la cuenta regresiva para ocultar la notificaci�n.
    void Start()
    {
        StartCoroutine(HideAfterDelay());
    }

    // Corutina que espera 'displayDuration' segundos y luego destruye el objeto.
    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        // Aqu� podr�as agregar animaciones de salida antes de destruir el objeto.
        Destroy(gameObject);

    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
