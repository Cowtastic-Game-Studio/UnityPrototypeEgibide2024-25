using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class SetUpPhase : IGamePhase
    {
        #region Properties

        #region Property: Phase

        public GamePhaseTypes Phase { get { return GamePhaseTypes.SetUp; } }

        #endregion

        #endregion


        #region Metodos publicos
        /// <summary>
        /// Al entrar en la fase se ejecutara NewGame() para preparar el mazo y PAs
        /// </summary>
        public void EnterPhase()
        {
            // Iniciamos el nuevo juego
            NewGame();
        }

        public void ExecutePhase()
        {

        }

        public void EndPhase()
        {
            Debug.Log("Termina la fase de empezar el d a.");
            Debug.Log("Termina la primera fase de todas, si me ves dos veces hay algo mal");
        }
        #endregion

        #region Metodos privados
        /// <summary>
        /// Inicia un nuevo juego preparando el deck (InitializeDeck())
        /// Establece los PA (RestartPA()) a los que tendria que tener el jugador al empezar la ronda
        /// </summary>
        private void NewGame()
        {
            // Inicializamos el deck
            GameManager.Instance.Tabletop.CardManager.InitializeDeck();
        }
        #endregion
    }
}