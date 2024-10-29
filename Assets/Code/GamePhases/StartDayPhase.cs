    public class StartDayPhase : IGamePhase
    {
    	public DeckAndHandManager hand;
        public void EnterPhase()
        {
            // L�gica de inicializaci�n
            Debug.Log("Empieza el d�a. Roba 5 cartas.");
            hand.Draw();
        }

        public void ExecutePhase()
        {
            // L�gica principal, lo que sucede durante esta fase
        }

        public void EndPhase()
        {
            Debug.Log("Termina la fase de empezar el d�a.");
        }

