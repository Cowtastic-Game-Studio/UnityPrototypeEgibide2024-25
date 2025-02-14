using UnityEngine;
using UnityEngine.EventSystems;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CursorOverbehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Texture2D cursorPonter;
        [SerializeField] private Texture2D cursorNormal;

        private void Awake()
        {
            cursorPonter = Resources.Load("hand_point") as Texture2D;
            cursorNormal = Resources.Load("pointer_a") as Texture2D;
        }


        private void OnMouseEnter()
        {
            ActivateCursor();
        }

        private void OnMouseExit()
        {
            DeActivateCursor();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ActivateCursor();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DeActivateCursor();
        }


        #region Private methods

        private void ActivateCursor()
        {
            Cursor.SetCursor(cursorPonter, Vector2.zero, CursorMode.Auto);
        }

        private void DeActivateCursor()
        {
            Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
        }

        #endregion


    }
}
