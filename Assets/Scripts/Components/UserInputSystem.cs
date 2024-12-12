using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using UnityEngine;

public class UserInputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;
    private InputAction _moveAction;
    private InputAction _shootAction;
    private InputAction _jerkAction;

    private float2 _moveInput;
    private float _shootInput;
    private float _jerkInput;
    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        _moveAction = new InputAction (name:"move", binding: "<Gamepad>/rightStick");
        _moveAction.AddCompositeBinding("Dpad")
        .With(name: "Up", binding: "<Keyboard>/w")
        .With(name:"Down", binding:"<Keyboard>/s")
        .With(name:"Left", binding:"<Keyboard>/a")
        .With(name: "Right", binding:"<Keyboard>/d");

        _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.Enable();

        _shootAction = new InputAction(name: "shoot", binding: "<Keyboard>/space");
        _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.Enable();

        _jerkAction = new InputAction(name: "jerk", binding: "<Keyboard>/shift");
        _jerkAction.performed += context => { _jerkInput = context.ReadValue<float>(); };
        _jerkAction.started += context => { _jerkInput = context.ReadValue<float>(); };
        _jerkAction.canceled += context => { _jerkInput = context.ReadValue<float>(); };
        _jerkAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _shootAction.Disable();
        _jerkAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach(
            (Entity entity, ref InputData inputData) => 
            {
                inputData.Move = _moveInput;
                inputData.Shoot = _shootInput;
                inputData.Jerk = _jerkInput;
            });
    }
}
