using UnityEngine;

namespace Scarcity
{
    public class PlayerCursor : MonoBehaviour
    {
        public Texture2D cursorTexture;
        public Vector2 cursorOffset;

        private void OnEnable()
        {
            RoundManager.GameOver += OnGameOver;
            ApplyCursor();
        }

        private void OnDisable()
        {
            RoundManager.GameOver -= OnGameOver;
            RemoveCursor();
        }

        private void ApplyCursor() => Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.Auto);

        private void RemoveCursor() => Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);

        private void OnGameOver()
        {
            RemoveCursor();
        }
    }
}