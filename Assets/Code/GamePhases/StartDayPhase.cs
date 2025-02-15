namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class StartDayPhase : IGamePhase
    {

        #region Properties

        #region Property: Phase

        public GamePhaseTypes Phase { get { return GamePhaseTypes.StartDay; } }

        #endregion

        #endregion


        //TODO: Gestionar el numero de veces que se puede hacer mulligan(Maximo 4 por fase)


        /// <summary>
        /// Acciones a ejecutar al inicio de la fase.
        /// </summary>
        public void EnterPhase()
        {
            // Limpiar el tablero
            GameManager.Instance.Tabletop.CardManager.WipeBoard();

            //pasar al siguiente dia
            GameManager.Instance.GameCalendar.NextDay();

            // Llama a Robo
            GameManager.Instance.Tabletop.CardManager.DrawFromDeck();
            GameManager.Instance.Tabletop.UpdateEmptyPlaces();
            GameManager.Instance.Tabletop.UpdateAcivePlaces();

            GameManager.Instance.Tabletop.CardManager.ActivateHandDeckCards();
        }

        /// <summary>
        /// Acciones a Realizar durante la fase
        /// </summary>
        public void ExecutePhase()
        {

        }

        /// <summary>
        /// Funcion que llama al mulligan
        /// </summary>
        public void Mulligan()
        {
            //Llama a Mulligan
            GameManager.Instance.Tabletop.CardManager.Mulligan();
        }

        /// <summary>
        /// Acciones a ejecutar al finalizar la fase
        /// </summary>
        public void EndPhase()
        {

        }
    }
}