namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class NewMemberEvent : CalendarEvent
    {
        /// <summary>
        /// Constructor para inicializar el evento generico.
        /// </summary>
        public NewMemberEvent()
            : base("Nuevo integrante ne la familia", "¡Resulta que una de tus vacas no estaba gorda, estaba embarazada! Has tenido un ternero (bueno, tú no, la vaca)", 1)
        {

        }

        public override void ApplyEffects()
        {
            GameManager.Instance.Tabletop.CardManager.BuyCard("Little Cow");
        }

        public override void EndEvent()
        {

        }

        public override void InitEvent()
        {

        }
    }
}
