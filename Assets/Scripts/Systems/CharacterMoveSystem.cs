using Unity.Entities;
using UnityEngine;

public class CharacterMoveSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(
            ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<MoveData>(),
            ComponentType.ReadOnly<JerkData>(),
            ComponentType.ReadOnly<Transform>(),
            ComponentType.ReadOnly<Rigidbody>()
        );
    }

    protected override void OnUpdate()
    {
        float currentTime = (float)World.Time.ElapsedTime;

        Entities.With(_moveQuery).ForEach(
            (Entity entity, Transform transform, ref InputData inputData, ref MoveData move, ref JerkData jerkData) =>
            {
                    float speed = move.Speed;

                    // Расчет нового положения
                    var direction = new Vector3(inputData.Move.x, 0, inputData.Move.y).normalized;
                    var pos = transform.position + direction * speed * Time.DeltaTime;
                    transform.position = pos;

                    // Поворот в сторону движения
                    if (direction != Vector3.zero)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.05f);
                    }
                
            }
        );
    }
}