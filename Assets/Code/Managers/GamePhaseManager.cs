public class GamePhaseManager
{
    private IGamePhase currentPhase;

    public void SetPhase(IGamePhase newPhase)
    {
        if (currentPhase != null)
        {
            currentPhase.EndPhase();
        }
        currentPhase = newPhase;
        currentPhase.EnterPhase();
    }

    public void NextPhase()
    {
        
    }

    public void Update()
    {
        if (currentPhase != null)
        {
            currentPhase.ExecutePhase();
        }
    }
}
