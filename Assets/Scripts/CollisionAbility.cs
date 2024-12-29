using DefaultNamespace;
using DefaultNamespace.Components.Interfaces;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
{
    public Collider Collider;
    public List<MonoBehaviour> collsionActions = new List<MonoBehaviour>();
    public List<IAbilityTarget> collisionActionAbilities = new List<IAbilityTarget>();
    [SerializeField] private string[] allowedTags;

    [HideInInspector]public List<Collider> collision;
    
    private void Start()
    {
        foreach (var action in collsionActions)
        {
            if (action is IAbilityTarget ability)
            {
                collisionActionAbilities.Add(ability);
            }
            else
            {
                Debug.LogError("Collision action must derive from IAbility!!!");
            }
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {

            float3 position = gameObject.transform.position;
            switch (Collider)
            {
                case SphereCollider sphere:
                    sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        ColliderType = ColliderType.Sphere,
                        SphereCenter = sphereCenter - position,
                        SphereRadius = sphereRadius,
                        initialTakeOff = true
                    });
                    break;
                case CapsuleCollider capsule:
                    capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        ColliderType = ColliderType.Capsule,
                        CapsuleStart = capsuleStart - position,
                        CapsuleEnd = capsuleEnd - position,
                        CapsuleRadius = capsuleRadius,
                        initialTakeOff = true
                    });
                break;
                case BoxCollider box:
                    box.ToWorldsSpaceBox(out var boxCenter, out var boxhalfExtents, out var boxOrientation);
                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        ColliderType = ColliderType.Box,
                        BoxCenter = boxCenter - position,
                        BoxHalfExtents = boxhalfExtents,
                        BoxOrientation = boxOrientation,
                        initialTakeOff = true
                    });
                break;
            }
            Collider.enabled = false;
    }

    public void Execute()
    {
            
            foreach (var action in collisionActionAbilities)
            {
                action.Targets = new List<GameObject>();
                collision.ForEach(c =>
                {
                    if(c != null && IsTagAllowed(c.tag))
                    action.Targets.Add(c.gameObject);
                });
                action.Execute();
            }
    }

    public bool IsTagAllowed(string tag)
    {
        foreach (var allowedTag in allowedTags)
        {
            if (tag == allowedTag)
            {
                return true;
            }
        }
        return false;
    }
}
public struct ActorColliderData : IComponentData
{
    public ColliderType ColliderType;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtents;
    public Quaternion BoxOrientation;
    public bool initialTakeOff;
}

public enum ColliderType
{
    Sphere = 0,
    Capsule = 1,
    Box = 2
}
