using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager manager;

    public InputActionMap inputs = new InputActionMap();

    private void Awake()
    {
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        }
        else if(manager != this)
        {
            Destroy(gameObject);
        }
        inputs.Enable();
    }

    public void AddInput(string inputName, OnRecieveInput onRecieveInput, InputType type = InputType.OnStarted)
    {
        switch (type)
        {
            case InputType.OnPerformed:
                inputs.FindAction(inputName).performed += ctx => onRecieveInput(ctx);
                break;
            case InputType.OnStarted:
                inputs.FindAction(inputName).started += ctx => onRecieveInput(ctx);
                break;
            case InputType.OnCancelled:
                inputs.FindAction(inputName).canceled += ctx => onRecieveInput(ctx);
                break;
        }
    }

    public void RemoveInput (string inputName, OnRecieveInput onRecieveInput, InputType type = InputType.OnStarted)
    {
        switch (type)
        {
            case InputType.OnPerformed:
                inputs.FindAction(inputName).performed -= ctx => onRecieveInput(ctx);
                break;
            case InputType.OnStarted:
                inputs.FindAction(inputName).started -= ctx => onRecieveInput(ctx);
                break;
            case InputType.OnCancelled:
                inputs.FindAction(inputName).canceled -= ctx => onRecieveInput(ctx);
                break;
        }
    }

    public void AddEvent(string inputName, System.Action onRecieveInput, InputType type = InputType.OnStarted)
    {
        switch (type)
        {
            case InputType.OnPerformed:
                inputs.FindAction(inputName).performed += ctx => onRecieveInput();
                break;
            case InputType.OnStarted:
                inputs.FindAction(inputName).started += ctx => onRecieveInput();
                break;
            case InputType.OnCancelled:
                inputs.FindAction(inputName).canceled += ctx => onRecieveInput();
                break;
        }
    }

    public void RemoveEvent(string inputName, System.Action onRecieveInput, InputType type = InputType.OnStarted)
    {
        switch (type)
        {
            case InputType.OnPerformed:
                inputs.FindAction(inputName).performed -= ctx => onRecieveInput();
                break;
            case InputType.OnStarted:
                inputs.FindAction(inputName).started -= ctx => onRecieveInput();
                break;
            case InputType.OnCancelled:
                inputs.FindAction(inputName).canceled -= ctx => onRecieveInput();
                break;
        }
    }

    public InputAction GetInput (string inputName)
    {
        return inputs.FindAction(inputName);
    }
}

public delegate void OnRecieveInput (InputAction.CallbackContext ctx);

public enum InputType
{
    OnPerformed,
    OnStarted,
    OnCancelled
}