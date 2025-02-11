using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public class CursorOverbehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]private Texture2D cursor;

        private void Awake()
        {
            cursor = Resources.Load("cursor") as Texture2D;
        }


        //private void OnMouseEnter()
        //{
        //    ActivateCursor();
        //}

        //private void OnMouseExit()
        //{
        //    DeActivateCursor();
        //}

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
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }

        private void DeActivateCursor()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        #endregion


        }
}
