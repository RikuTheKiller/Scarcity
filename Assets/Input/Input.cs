using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scarcity
{
    public class Input : MonoBehaviour
    {
        public static readonly InputActions Actions = InitActions();

        private static InputActions InitActions()
        {
            InputActions actions = new();
            actions.Enable();
            return actions;
        }

        public static readonly InputValue<Vector2> Move = Actions.Player.Move;
        public static readonly InputValue<Vector2> Point = Actions.UI.Point;
    }

    public class InputValue<T> where T : struct
    {
        public static implicit operator InputValue<T>(InputAction action) => new(action);
        public static implicit operator T(InputValue<T> inputValue) => inputValue.Value;

        public readonly InputAction Action;

        public T Value;

        public Action<T> Started;
        public Action<T> Performed;
        public Action<T> Canceled;

        public InputValue(InputAction action)
        {
            Action = action;

            action.started += OnStarted;
            action.performed += OnPerformed;
            action.canceled += OnCanceled;
        }

        private void OnStarted(InputAction.CallbackContext context)
        {
            Started?.Invoke(context.ReadValue<T>());
        }

        private void OnPerformed(InputAction.CallbackContext context)
        {
            Performed?.Invoke(context.ReadValue<T>());
            Value = context.ReadValue<T>();
        }

        private void OnCanceled(InputAction.CallbackContext context)
        {
            Canceled?.Invoke(context.ReadValue<T>());
            Value = context.ReadValue<T>();
        }
    }
}
