using UnityEngine;
using UnityEngine.UI;

public class RapidClickCounter : MonoBehaviour
{
    public Button button;  // Referencia al bot�n
    private int rapidClickCount = 0; // Contador de clics r�pidos
    private float lastClickTime = 0f; // �ltima vez que se hizo clic
    public float clickThreshold = 0.5f; // Tiempo m�ximo entre clics para considerarlos r�pidos (en segundos)

    void Start()
    {
        // Aseg�rate de que el bot�n est� asignado y configurado
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick); // A�adir el listener para el clic
        }
    }

    // Funci�n que se llama cuando se hace clic en el bot�n
    void OnButtonClick()
    {
        // Verificar si el clic est� dentro del intervalo r�pido
        if (Time.time - lastClickTime <= clickThreshold)
        {
            rapidClickCount++; // Incrementa el contador de clics r�pidos
        }
        else
        {
            rapidClickCount = 1; // Si no es r�pido, lo reinicia a 1 (primer clic en una nueva secuencia r�pida)
        }

        lastClickTime = Time.time; // Actualiza el tiempo del �ltimo clic

        if (rapidClickCount >= 10)
        {
            MessageManager.Instance.ShowMessage("MIAU MIAU MIAU");
            rapidClickCount = 0; // Opcional: reiniciar el contador a 0 despu�s de llegar a 10
        }
    }
}
