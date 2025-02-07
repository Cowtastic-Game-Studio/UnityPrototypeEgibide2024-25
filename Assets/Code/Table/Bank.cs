namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Bank : IStorage
    {
        #region Propiedades

        public GameResource Type { get; set; }
        public int MaxResources { get; set; }
        public int Level { get; set; }
        public int Resource { get; set; }

        #endregion

        #region Constructor

        public Bank()
        {
            // Le indicamos que el tipo del recurso es
            Type = GameResource.Muuney;
            MaxResources = 25;
            Level = 1;
            Resource = 100;
        }

        #endregion

        #region Metodos

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion
    }
}