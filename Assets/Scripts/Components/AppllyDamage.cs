using UnityEngine;
using System.Collections.Generic;

public class ApplyDamage : MonoBehaviour, IAbilityTarget

{
    public int Damage = 10;
    public List<GameObject> Targets { get; set ; }
    [SerializeField] private string[] allowedTags;

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target != null && IsTagAllowed(target.tag, allowedTags)) // Проверяем тег
            {
                var health = target.GetComponent<CharacterHealth>();
                if (health != null && health.Health > 0)
                {
                    health.Health -= Damage;
                    Debug.Log($"Dealt {Damage} damage to {target.name}");
                }
            }
        }
        Destroy(gameObject); // Уничтожаем объект после применения урона
    }

    public bool IsTagAllowed(string tag, string[] allowedTags)
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
