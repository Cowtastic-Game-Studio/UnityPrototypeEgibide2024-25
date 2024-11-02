using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class FarmZone : MonoBehaviour, IDropZone<Cow>
    {
        #region Propiedades

        public bool IsActive { get; }
        public Cow Type { get; }

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