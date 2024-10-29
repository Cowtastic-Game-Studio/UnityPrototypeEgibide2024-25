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
    public void WipeBoard()
    {
        // Find all GameObjects tagged as "card"
        GameObject[] cardsOnBoard = GameObject.FindGameObjectsWithTag("Carta");

        // Move each card to Descartes
        foreach (GameObject card in cardsOnBoard)
        {
            // Set Descartes as the new parent
            card.transform.SetParent(descartes.transform);
            // Optional: Reset local position, rotation, etc., as needed
            card.transform.localPosition = Vector3.zero;  // Position at the center of Descartes
            card.transform.localRotation = Quaternion.identity;
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
