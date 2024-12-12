using DefaultNamespace.Components.Interfaces;
using Unity.Entities;
using UnityEngine;

public class CharacterJerkSystem : ComponentSystem
{
    private EntityQuery _jerkQuery;

    protected override void OnCreate()
    {
        _jerkQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<JerkData>(),
            ComponentType.ReadOnly<UserInputData>());

    }

    protected override void OnUpdate()
    {
        Entities.With(_jerkQuery).ForEach(
            (Entity entity, UserInputData inputData, ref InputData input) =>
            {
                if (input.Jerk > 0f && inputData.JerkAction != null && inputData.JerkAction is IAbility ability)
                {
                    ability.Execute();
                }
            });
    }
}
