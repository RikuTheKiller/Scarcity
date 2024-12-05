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

        // Player Actions
        public static readonly InputValue<Vector2> Move = Actions.Player.Move;
        public static readonly InputButton Attack = Actions.Player.Attack;

        // UI Actions
        public static readonly InputValue<Vector2> Point = Actions.UI.Point;
        public static readonly InputButton Cancel = Actions.UI.Cancel;
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
            Value = context.ReadValue<T>();
            Started?.Invoke(Value);
        }

        private void OnPerformed(InputAction.CallbackContext context)
        {
            Value = context.ReadValue<T>();
            Performed?.Invoke(Value);
        }

        private void OnCanceled(InputAction.CallbackContext context)
        {
            Value = context.ReadValue<T>();
            Canceled?.Invoke(Value);
        }
    }

    public class InputButton
    {
        public static implicit operator InputButton(InputAction action) => new(action);
        public static implicit operator bool(InputButton inputButton) => inputButton.Pressed;

        public readonly InputAction Action;

        public bool Pressed;

        public Action Started;
        public Action Performed;
        public Action Canceled;

        public InputButton(InputAction action)
        {
            Action = action;

            action.started += OnStarted;
            action.performed += OnPerformed;
            action.canceled += OnCanceled;
        }

        private void OnStarted(InputAction.CallbackContext context)
        {
            Pressed = true;
            Started?.Invoke();
        }

        private void OnPerformed(InputAction.CallbackContext context)
        {
            Pressed = true;
            Performed?.Invoke();
        }

        private void OnCanceled(InputAction.CallbackContext context)
        {
            Pressed = false;
            Canceled?.Invoke();
        }
    }
}
