using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scarcity
{
    public class Input : MonoBehaviour
    {
        public static InputActions Actions;

        private static InputActions InitActions()
        {
            InputActions actions = new();
            actions.Enable();
            return actions;
        }

        private void Awake()
        {
            Actions = InitActions();

            Move = Actions.Player.Move;
            Attack = Actions.Player.Attack;

            Point = Actions.UI.Point;
            Cancel = Actions.UI.Cancel;
            Enter = Actions.UI.Enter;
        }

        private void OnDestroy()
        {
            Actions.Disable();
        }

        // Player Actions
        public static InputValue<Vector2> Move;
        public static InputButton Attack;

        // UI Actions
        public static InputValue<Vector2> Point;
        public static InputButton Cancel;
        public static InputButton Enter;
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
