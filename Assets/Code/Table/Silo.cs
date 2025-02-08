namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class Silo : IStorage
    {
        #region Propiedades

        public GameResource Type { get; set; }
        public int MaxResources { get; set; }
        public int MaxLevel { get; set; }
        public int Level { get; set; }
        public int Resource { get; set; }

        #endregion

        #region Constructor
        public Silo()
        {
            // Le indicamos que el tipo del recurso es
            Type = GameResource.Cereal;
            MaxResources = 6;
            MaxLevel = 6;
            Level = 1;
            Resource = 0;
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