using UnityEngine;

public class CleaningTable : MonoBehaviour
{

    /// <summary>
    /// Mazo de descartes
    /// </summary>
    public GameObject descartes;
    /// <summary>
    /// Cuando se le llama, quita las cartas colocadas en el tablero y las introduce en el mazo de descartes
    /// </summary>
    private void WipeBoard()
    {
        // Busca todas las cartas
        GameObject[] cardsInScene = GameObject.FindGameObjectsWithTag("Carta");

        foreach (GameObject card in cardsInScene)
        {
            // Usa un raycast desde cada carta para verificar si estan colocadas en el tablero
            Ray ray = new Ray(card.transform.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag("Place"))
            {

                // Les pone Descartes como nuevo padre
                card.transform.SetParent(descartes.transform);
                // Las mueve
                card.transform.localPosition = Vector3.zero;
                card.transform.localRotation = Quaternion.identity;

                // Si se requiere se pueden ocultar
                // card.SetActive(false);
            }
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        // Al presionar L limpia el teblero
        if (Input.GetKeyDown(KeyCode.L))
        {
            WipeBoard();
        }
#endif
    }
}
