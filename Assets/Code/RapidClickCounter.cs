using UnityEngine;
using UnityEngine.UI;

public class RapidClickCounter : MonoBehaviour
{
    public Button button;  // Referencia al botón
    private int rapidClickCount = 0; // Contador de clics rápidos
    private float lastClickTime = 0f; // Última vez que se hizo clic
    public float clickThreshold = 0.5f; // Tiempo máximo entre clics para considerarlos rápidos (en segundos)

    void Start()
    {
        // Asegúrate de que el botón esté asignado y configurado
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick); // Añadir el listener para el clic
        }
    }

    // Función que se llama cuando se hace clic en el botón
    void OnButtonClick()
    {
        // Verificar si el clic está dentro del intervalo rápido
        if (Time.time - lastClickTime <= clickThreshold)
        {
            rapidClickCount++; // Incrementa el contador de clics rápidos
        }
        else
        {
            rapidClickCount = 1; // Si no es rápido, lo reinicia a 1 (primer clic en una nueva secuencia rápida)
        }

        lastClickTime = Time.time; // Actualiza el tiempo del último clic

        if (rapidClickCount >= 10)
        {
            MessageManager.Instance.ShowMessage("MIAU MIAU MIAU");
            rapidClickCount = 0; // Opcional: reiniciar el contador a 0 después de llegar a 10
        }
    }
}
