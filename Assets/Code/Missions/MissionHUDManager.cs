using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CowtasticGameStudio.MuuliciousHarvest.Assets.Code.Missions;
using UnityEngine;
using UnityEngine.UI;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class MissionHUDmanager : MonoBehaviour
    {
        [SerializeField] private GameObject TutorialBoard;


        
        private List<Text> objectiveTexts = new List<Text>();


        #region Unity methods

        private void Start()
        {            
            UpdateHUD(MissionsManager.Instance.Tutorial);
        }

        #endregion



        #region Public methods

        public void UpdateHUD(Mission mission)
        {
            // Primero, limpia cualquier texto viejo en la UI.
            foreach (var objText in objectiveTexts)
            {
                Destroy(objText.gameObject);
            }
            objectiveTexts.Clear();

            // Ahora, actualiza los objetivos con los nuevos.
            foreach (Goal goal in mission.Goals)
            {
                // Crea un nuevo Text para cada objetivo y lo configura.
                Text newText = new GameObject(goal.Description).AddComponent<Text>();
                newText.transform.SetParent(TutorialBoard.transform);
                //newText.font = objectivesText.font;
                newText.fontSize = 12;
                newText.color = Color.black;
                newText.alignment = TextAnchor.UpperLeft;

                // Asigna el texto del objetivo.
                newText.text = goal.Description;

                // Ajusta la posición para que los objetivos no se sobrepongan.
                RectTransform rectTransform = newText.GetComponent<RectTransform>();
                rectTransform.localPosition = new Vector3(0, -30 * objectiveTexts.Count, 0); // Ajusta la distancia entre cada objetivo.

                objectiveTexts.Add(newText);
            }
        }


        #endregion
    }
}
