using System.Collections.Generic;
using System.Linq;
using CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions;
using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MissionSheetManager : MonoBehaviour
    {
        #region Properties

        #region Property: Mission

        /// <summary>
        /// Mision que se va a representar
        /// </summary>
        private Mission _Mission;

        public Mission Mission
        {
            get { return _Mission; }
            set { 
                _Mission = value;
                Initialize(value);
            }
        }

        #endregion

        #region Property: TitleText

        /// <summary>
        /// Caja de texto para el titulo
        /// </summary>
        [SerializeField] private TMP_Text TitleText;

        #endregion

        #region Property: GoalsContent

        /// <summary>
        /// Contenedor donde apareceran los objetivos
        /// </summary>
        [SerializeField] private GameObject GoalsContent;

        #endregion

        #region Property: GoalTexts

        /// <summary>
        /// Listado de las cajas de texto de los objetivos
        /// </summary>
        private List<GameObject> GoalTexts = new List<GameObject>();

        #endregion

        #region Property: GoalTextPrefab

        /// <summary>
        /// Prefab de la caja de texto del objetivo
        /// </summary>
        [SerializeField] private GameObject GoalTextPrefab;

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Inicializa la hoja de mision
        /// </summary>
        /// <param name="mision"></param>
        private void Initialize(Mission mision)
        {
            this.TitleText.text = mision.Description;
            this.UpdateGoals(mision.Goals.ToList());
            
        }

        /// <summary>
        /// Actualiza la lista de objetivos
        /// </summary>
        /// <param name="mission"></param>
        public void UpdateGoals(List<Goal> goals)
        {
            GameObject newtext;
            TMP_Text tmp;

            // Primero, limpia cualquier texto viejo en la UI.
            foreach (GameObject goalText in GoalTexts)
            {
                Destroy(gameObject);
            }
            GoalTexts.Clear();

            // Ahora, actualiza los objetivos con los nuevos.
            foreach (Goal goal in goals)
            {
                newtext = Instantiate(GoalTextPrefab, GoalsContent.transform);

                newtext.name = goal.Name;
                tmp = newtext.GetComponent<TMP_Text>();

                tmp.text = goal.Description;

                if(goal.IsComplete)
                    tmp.fontStyle = FontStyles.Strikethrough;

                GoalTexts.Add(newtext);
            }
        }

        #endregion

    }
}
