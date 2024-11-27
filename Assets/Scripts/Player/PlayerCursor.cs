using UnityEngine;

namespace Scarcity
{
    public class PlayerCursor : MonoBehaviour
    {
        public Texture2D cursorTexture;
        public Vector2 cursorOffset;

        private void OnEnable()
        {
            Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.Auto);
        }

        private void OnDisable()
        {
            Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
        }
    }
}