using DefaultNamespace.Components.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float speed;
    public MonoBehaviour ShootAction;
    public MonoBehaviour JerkAction;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData<InputData>(entity, new InputData()
        {
            Move = Vector2.zero,    // ѕо умолчанию движение в нулевую позицию
        });
        dstManager.AddComponentData<MoveData>(entity, new MoveData
        {
            Speed = speed / 10
        });
        if (ShootAction != null && ShootAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }
        if (JerkAction != null && JerkAction is IAbility)
        {
            dstManager.AddComponentData<JerkData>(entity, new JerkData()
            {
                //JerkSpeed = speed * 0.1f
            });
        }
    }
}

public struct InputData: IComponentData
{
    public float2 Move;
    public float Shoot;
    public float Jerk;
    public bool JerkTriggered;
}

public struct MoveData: IComponentData 
{
    public float Speed;
}

public struct ShootData: IComponentData
{
    public float ShootSpeed;
}

public struct JerkData: IComponentData
{
    
}
