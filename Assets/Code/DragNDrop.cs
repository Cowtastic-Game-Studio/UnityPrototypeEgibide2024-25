using UnityEngine;

namespace CowtasticGameStudio.UI
{

    /// <summary>
    /// Drag and drop + deteccion de puntos de anclaje
    /// </summary>
    public class DragNDrop : MonoBehaviour
    {
        #region Variables Drag & Drop

        /// <summary>
        ///  Bandera para saber si se está arrastrando un objeto
        /// </summary>
        private bool isDragging = false;
        /// <summary>
        /// El objeto que está siendo arrastrado
        /// </summary>
        private Transform draggedObject;
        /// <summary>
        /// El offset de la posición del objeto en relación con la posición del ratón
        /// </summary>
        private Vector3 dragOffset;
        /// <summary>
        /// La profundidad del objeto en el eje Z en el espacio de la pantalla
        /// </summary>
        private float objectZDepth;
        /// <summary>
        /// El valor fijo de la posición en el eje Y del objeto durante el arrastre
        /// </summary>
        private float fixedYPosition;

        #endregion


        #region Deteccion de ptos anclaje
        /// <summary>
        /// Altura adicional para colocar el objeto sobre el "Place"
        /// </summary>
        public float placementHeightOffset = 1f;
        /// <summary>
        /// Guarda el color original para su posterior restauracion
        /// </summary>
        private Color originalColor;
        /// <summary>
        /// Indica si el objeto esta sobre un "Place"
        /// </summary>
        private bool isOverPlace = false;
        #endregion



        void Update()
        {
            // Si se presiona el botón izquierdo del ratón
            if (Input.GetMouseButtonDown(0))
            {
                // Inicia el proceso de arrastre
                HandleMouseDown();
            }
            // Actualiza la posición del objeto mientras se arrastra
            if (isDragging)
            {
                HandleMouseDrag();
            }
            // Si se suelta el botón del ratón
            if (Input.GetMouseButtonUp(0))
            {
                // Detiene el arrastre y coloca el objeto en su nueva posición o lo devuelve a su lugar original "PENDIENTE DE INCLUIR"
                HandleMouseUp();
            }
        }
        /// <summary>
        /// Maneja lo que sucede mientras el boton del raton esta presionado
        /// </summary>
        private void HandleMouseDown()
        {
            RaycastHit hit;
            // Verifica si el objeto que el rayo toca es una "Carta"
            if (RaycastFromMouse(out hit) && hit.collider.CompareTag("Carta"))
            {
                //if true --> Comienza el arrastre
                StartDragging(hit.collider.transform);
            }
        }
        /// <summary>
        /// Maneja el movimiento del objeto mientras se esta arrastrando
        /// </summary>
        private void HandleMouseDrag()
        {
            // Convierte la posición del ratón a coordenadas del mundo
            Vector3 mousePosition = GetMouseWorldPosition();

            // Verifica si el objeto está sobre un "Place"
            if (IsMouseOverPlace(out RaycastHit hitBelow))
            {
                // Si no estaba sobre un "Place" antes
                if (!isOverPlace)
                {
                    // Cambia el color del objeto a negro
                    ChangeColor(Color.black);
                    // Marca que ahora está sobre un "Place"
                    isOverPlace = true;
                }
            }
            else
            {
                // Si estaba sobre un "Place" pero ya no lo está
                if (isOverPlace)
                {
                    // Restaura el color original del objeto
                    RevertColor();
                    // Indica que ya no está sobre un "Place"
                    isOverPlace = false;
                }
            }
            // Actualiza la posición del objeto mientras se arrastra
            UpdateObjectPosition(mousePosition);
        }

        /// <summary>
        /// Maneja lo que sucede cuando se suelta el boton del raton
        /// </summary>
        private void HandleMouseUp()
        {
            // Verifica si hay un objeto siendo arrastrado
            if (draggedObject != null)
            {
                // Si el objeto está sobre un "Place"
                if (IsMouseOverPlace(out RaycastHit hitBelow))
                {
                    // Coloca el objeto sobre el "Place"
                    SnapToPlace(hitBelow);
                }
                // Detiene el arrastre
                StopDragging();
                //Aquí anadir la logica para que vuelva a su posicion original
            }
        }

        /// <summary>
        /// Comienza el arrastre de la carta
        /// </summary>
        /// <param name="objectToDrag">El Objeto a arrastrar</param>
        private void StartDragging(Transform objectToDrag)
        {
            #region Variables de StarDragging
            // Asigna el objeto que se está arrastrando
            draggedObject = objectToDrag;
            // Indica que se está arrastrando
            isDragging = true;
            // Obtiene la profundidad Z del objeto en la pantalla
            objectZDepth = Camera.main.WorldToScreenPoint(draggedObject.position).z;
            // Hace que la posicion sea siempre la original + 1
            fixedYPosition = draggedObject.position.y + 1;
            // Obtiene la posición del ratón en el mundo
            Vector3 mousePosition = GetMouseWorldPosition();
            // Esto hace que al arrastrar no se vaya "Volando" la carta, manteniendose a un nivel estable, de no estar se saldria del escenario
            dragOffset = draggedObject.position - new Vector3(mousePosition.x, 0, mousePosition.z);
            #endregion

            // Obtiene el Renderer del objeto para cambiar su color
            Renderer renderer = draggedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Guarda el color original del objeto
                originalColor = renderer.material.color;
            }
        }

        /// <summary>
        /// Detiene el arrastre del objeto
        /// </summary>
        private void StopDragging()
        {
            // Indica que ya no se está arrastrando
            isDragging = false;
            if (isOverPlace)
            {
                // Restaura el color si estaba sobre un "Place"
                RevertColor();
                // Resetea el indicador de "Place"
                isOverPlace = false;
            }
            // Libera el objeto que estaba siendo arrastrado
            draggedObject = null;
        }

        /// <summary>
        /// Actualiza la posición del objeto mientras se arrastra
        /// </summary>
        /// <param name="mousePosition">Posicion del mouse</param>
        private void UpdateObjectPosition(Vector3 mousePosition)
        {
            // Actualiza solo las posiciones X y Z, manteniendo el eje Y fijo
            draggedObject.position = new Vector3(mousePosition.x + dragOffset.x, fixedYPosition, mousePosition.z + dragOffset.z);
        }

        /// <summary>
        /// Traslada la posición del ratón en pantalla a una posición en el mundo
        /// </summary>
        /// <returns>Posicion equivalente en el mundo</returns>
        private Vector3 GetMouseWorldPosition()
        {
            // Usa la profundidad Z capturada
            Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectZDepth);
            // Convierte la posición de la pantalla a posición en el mundo
            return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        }

        /// <summary>
        /// Realiza un raycast desde la posición del ratón
        /// </summary>
        /// <param name="hit">Detector</param>
        /// <returns>Si ha detectado algun objeto</returns>
        private bool RaycastFromMouse(out RaycastHit hit)
        {
            // Crea un rayo desde la cámara hacia la posición del ratón
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Verifica si el rayo toca un objeto
            return Physics.Raycast(ray, out hit);
        }

        /// <summary>
        /// Verifica si hay un "Place"
        /// </summary>
        /// <param name="hitBelow">Detecta debajo del objeto</param>
        /// <returns>Si el objeto es "Place"</returns>
        private bool IsMouseOverPlace(out RaycastHit hitBelow)
        {
            // Crea un rayo hacia abajo desde el objeto arrastrado
            Ray downwardRay = new Ray(draggedObject.position, Vector3.down);
            // Verifica si toca un objeto con la etiqueta "Place"
            return Physics.Raycast(downwardRay, out hitBelow) && hitBelow.collider.CompareTag("Place");
        }

        /// <summary>
        /// Cambia el color del objeto
        /// </summary>
        /// <param name="color">Color</param>
        private void ChangeColor(Color color)
        {
            // Obtiene el Renderer del objeto
            Renderer renderer = draggedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Cambia el color del material del objeto
                renderer.material.color = color;
            }
        }

        /// <summary>
        /// Restaura el color al original
        /// </summary>
        private void RevertColor()
        {
            // Obtiene el Renderer del objeto
            Renderer renderer = draggedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Restaura el color original del material del objeto
                renderer.material.color = originalColor;
            }
        }

        /// <summary>
        /// Coloca el objeto sobre "Place"
        /// </summary>
        /// <param name="hitBelow">Si golpea place</param>
        private void SnapToPlace(RaycastHit hitBelow)
        {
            // Obtiene la posición del "Place"
            Vector3 placePosition = hitBelow.collider.transform.position;
            // Coloca el objeto ligeramente por encima del "Place"
            draggedObject.position = new Vector3(placePosition.x, placePosition.y + placementHeightOffset, placePosition.z);
        }
    }

}
