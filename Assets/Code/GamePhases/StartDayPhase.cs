using UnityEngine;

public class StartDayPhase : IGamePhase
{
    public void EnterPhase()
    {
        // Lógica de inicialización
        Debug.Log("Empieza el día. Roba 5 cartas.");
    }

    public void ExecutePhase()
    {
        // Lógica principal, lo que sucede durante esta fase
    }

    public void EndPhase()
    {
        Debug.Log("Termina la fase de empezar el día.");
    }
}

