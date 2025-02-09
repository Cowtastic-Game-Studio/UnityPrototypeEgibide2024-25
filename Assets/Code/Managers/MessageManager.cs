using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MessageManager : MonoBehaviour
    {
        // Arrastra en el inspector el prefab de la notificación que creaste
        public GameObject messagePrefab;
        // Referencia al Canvas (o a un contenedor específico dentro del Canvas)
        public Transform canvasTransform;

        // Llama a este método para mostrar una notificación
        public void ShowMessage(string text)
        {
            // Instancia el prefab dentro del Canvas
            GameObject messageGO = Instantiate(messagePrefab, canvasTransform);

            // Obtiene el componente Message del prefab
            Message messageComponent = messageGO.GetComponent<Message>();
            if (messageComponent != null)
            {
                messageComponent.SetText(text);
            }

            // Posiciona la notificación en la esquina superior derecha
            RectTransform rectTransform = messageGO.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                // Configura el anclaje a la parte superior derecha
                rectTransform.anchorMin = new Vector2(1, 1);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.pivot = new Vector2(1, 1);

                // Ajusta la posición (por ejemplo, 20 píxeles de margen desde los bordes)
                rectTransform.anchoredPosition = new Vector2(-20, -20);
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                ShowMessage("Este es el mensaje de notificación");
            }

            
        }
    }
}
