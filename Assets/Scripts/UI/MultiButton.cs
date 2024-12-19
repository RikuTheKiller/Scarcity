using UnityEngine;
using UnityEngine.UI;

namespace Scarcity
{
    public class MultiButton : Button
    {
        public Graphic[] m_TargetGraphics;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            var color = Color.black;

            switch (state)
            {
                case SelectionState.Normal:
                    color = colors.normalColor;
                    break;
                case SelectionState.Highlighted:
                    color = colors.highlightedColor;
                    break;
                case SelectionState.Pressed:
                    color = colors.pressedColor;
                    break;
                case SelectionState.Selected:
                    color = colors.selectedColor;
                    break;
                case SelectionState.Disabled:
                    color = colors.disabledColor;
                    break;
            }

            foreach (var graphic in m_TargetGraphics)
            {
                StartColorTween(graphic, color, instant);
            }
        }

        private void StartColorTween(Graphic targetGraphic, Color targetColor, bool instant)
        {
            if (targetGraphic != null)
            {
                targetGraphic.CrossFadeColor(targetColor, instant ? 0 : colors.fadeDuration, true, true);
            }
        }
    }
}