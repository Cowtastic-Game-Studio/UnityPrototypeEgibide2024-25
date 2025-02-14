using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    // Referencia al componente TextMeshProUGUI que mostrará el mensaje.
    public TextMeshProUGUI messageText;

    // (Opcional) Referencia al componente Image del fondo, para modificar su color vía código.
    public Image backgroundImage;

    // Tiempo (en segundos) que la notificación permanecerá en pantalla.
    public float displayDuration = 3f;

    // Método para asignar el texto del mensaje.
    public void SetText(string text)
    {
        if (messageText != null)
        {
            messageText.text = text;
        }
    }

    // Método para cambiar el color del fondo según el valor recibido.
    // 0: Rojo, 1: Azul, 2: Verde.
    // Además, se establece el alfa a 150 (150/255 en Unity).
    public void SetBackgroundColorByValue(int value)
    {
        if (backgroundImage == null)
        {
            Debug.LogError("¡El componente Image del fondo no está asignado!");
            return;
        }

        // Calculamos el valor del alfa en el rango [0,1]
        float alpha = 100f / 255f;

        switch (value)
        {
            case 0:
                backgroundImage.color = new Color(1f, 0f, 0f, alpha); // Rojo
                break;
            case 1:
                backgroundImage.color = new Color(0f, 0f, 1f, alpha); // Azul
                break;
            case 2:
                backgroundImage.color = new Color(0f, 1f, 0f, alpha); // Verde
                break;
            default:
                Debug.Log("Valor no reconocido: " + value);
                break;
        }
    }

    // Se inicia la cuenta regresiva para ocultar la notificación.
    void Start()
    {
        StartCoroutine(HideAfterDelay());
    }

    // Corutina que espera 'displayDuration' segundos y luego destruye el objeto.
    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        // Aquí podrías agregar animaciones de salida antes de destruir el objeto.
        Destroy(gameObject);
    }
}
