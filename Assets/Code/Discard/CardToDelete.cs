namespace CowtasticGameStudio.MuuliciousHarvest
{
    // Clase auxiliar para almacenar la informaci�n de las cartas a eliminar
    public class CardToDelete
    {
        public CardType CardType { get; set; }
        public int Quantity { get; set; }
        public CardTemplate CardTemplate { get; set; }
    }
}
