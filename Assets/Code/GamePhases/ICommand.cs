namespace CowtasticGameStudio.MuuliciousHarvest
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}