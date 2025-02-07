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