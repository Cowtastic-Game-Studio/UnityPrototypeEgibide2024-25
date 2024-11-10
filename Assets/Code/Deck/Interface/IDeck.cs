using System.Collections.Generic;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface IDeck
    {
        #region Properties

        /// <summary>
        /// Obtiene la pila de cartas como GameObjects.
        /// </summary>
        public Stack<GameObject> Cards { get; }

        #endregion

        #region Methods
        /// <summary>
        /// Barajea las cartas en el mazo.
        /// </summary>
        public void Shuffle();

        /// <summary>
        /// Roba la carta superior del mazo.
        /// </summary>
        /// <returns>Devuelve el GameObject de la carta robada.</returns>
        public GameObject Draw();

        /// <summary>
        /// Coloca una carta en la parte superior del mazo.
        /// </summary>
        /// <param name="card">El GameObject de la carta a colocar.</param>
        public void Place(GameObject card);


        /// <summary>
        /// Roba la carta superior del mazo.
        /// </summary>
        /// <returns>Devuelve una copia de la carta superior</returns>
        public GameObject Peek();

        /// <summary>
        /// Elimina una carta específica del mazo.
        /// </summary>
        /// <param name="targetCard">El GameObject de la carta a eliminar.</param>
        /// <returns>Devuelve true si la carta fue eliminada exitosamente; de lo contrario, false.</returns>
        public bool RemoveCard(GameObject targetCard);


        #endregion
    }
}
