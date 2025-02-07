using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions;
using TMPro;
using UnityEngine;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class GlobalMissionSheetManager : MonoBehaviour
    {
        #region Properties

        #region Property: Mission

        /// <summary>
        /// Misiones que se van a representar
        /// </summary>
        private List<Mission> _Missions;

        public List<Mission> Missions
        {
            get { return _Missions; }
            set { 
                _Missions = value;
                if(value != null)
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
        /// <param name="mission"></param>
        private void Initialize(List<Mission> missions)
        {
            this.TitleText.text = "Logros";
            this.UpdateGoals(missions);

            foreach(Mission mission in missions)
            {
                mission.Updated.AddListener(OnUpdated);
            }
            

        }
        /// <summary>
        /// Actualiza la lista de objetivos
        /// </summary>
        /// <param name="mission"></param>
        private void UpdateGoals(List<Mission> missions)
        {
            GameObject newtext;
            TMP_Text tmp;

            // Primero, limpia cualquier texto viejo en la UI.
            foreach (GameObject goalText in GoalTexts)
            {
                Destroy(goalText);
            }
            GoalTexts.Clear();

            // Ahora, actualiza los objetivos con los nuevos.
            foreach (Mission mission in missions)
            {
                newtext = Instantiate(GoalTextPrefab, GoalsContent.transform);

                newtext.name = mission.Name;
                tmp = newtext.GetComponent<TMP_Text>();

                tmp.text = mission.Goals[0].Description;
                tmp.color = new Color(0,0,0);

                if(mission.Goals[0].IsCompleted)
                    tmp.fontStyle = FontStyles.Strikethrough;

                GoalTexts.Add(newtext);
            }
        }

        #endregion

        #region Eventhandlers

        private void OnUpdated(Mission mission)
        {
            UpdateGoals(this.Missions);
        }

        #endregion

    }
}
