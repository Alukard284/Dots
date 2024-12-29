using UnityEngine;


public class TrapAbility : CollisionAbility
{
    public int Damage = 10;

    public void Execute()
    {
        Debug.Log($"TrapAbility.Execute() вызван для объекта {gameObject.name}");
        foreach (var target in collision)
        {
            Debug.Log("Execute called for TrapAbility.");
            var targetHealth = target?.gameObject?.GetComponent<CharacterHealth>();
            if (targetHealth != null)
            {
                targetHealth.Health -= Damage;
                Debug.Log("Health OK" + targetHealth.gameObject.name);
            }
        }
    }
}
