using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance { get; private set; }

    public GameObject messagePrefab;
    public Transform canvasTransform;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Opcional: No destruir este objeto al cargar otra escena.
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para mostrar el mensaje y asignar el color.
    // Se espera un texto y un código de color: 0 (Rojo), 1 (Azul) o 2 (Verde).
    public void ShowMessage(string text, int colorCode)
    {
        GameObject messageGO = Instantiate(messagePrefab, canvasTransform);
        Message messageComponent = messageGO.GetComponent<Message>();
        if (messageComponent != null)
        {
            messageComponent.SetText(text);
            messageComponent.SetBackgroundColorByValue(colorCode);
        }

        RectTransform rectTransform = messageGO.GetComponent<RectTransform>();
        // Puedes descomentar y ajustar la posición si lo necesitas:
        // if (rectTransform != null)
        // {
        //     rectTransform.anchorMin = new Vector2(1, 1);
        //     rectTransform.anchorMax = new Vector2(1, 1);
        //     rectTransform.pivot = new Vector2(1, 1);
        //     rectTransform.anchoredPosition = new Vector2(-20, -20);
        // }
    }
}
