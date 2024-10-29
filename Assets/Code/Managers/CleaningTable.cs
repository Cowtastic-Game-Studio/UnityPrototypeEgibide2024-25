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
        // Get all GameObjects with the "Card" tag in the scene
        GameObject[] cardsInScene = GameObject.FindGameObjectsWithTag("Carta");

        foreach (GameObject card in cardsInScene)
        {
            // Perform a raycast downward from each card to check if it's over a "Place"
            Ray ray = new Ray(card.transform.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag("Place"))
            {

                // Set Descartes as the new parent
                card.transform.SetParent(descartes.transform);
                // Optional: Reset local position, rotation, etc., as needed
                card.transform.localPosition = Vector3.zero;  // Position at the center of Descartes
                card.transform.localRotation = Quaternion.identity;

                // Optionally disable or hide the card from the deck
                // card.SetActive(false);
            }
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        // Check if "L" key is pressed and call WipeBoard
        if (Input.GetKeyDown(KeyCode.L))
        {
            WipeBoard();
        }
#endif
    }
}
