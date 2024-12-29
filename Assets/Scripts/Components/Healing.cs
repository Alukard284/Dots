using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour, IAbilityTarget
{
    public int healingPoints = 25;

    public List<GameObject> Targets { get ; set; }
    [SerializeField] private string[] allowedTags;

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target != null && IsTagAllowed(target.tag)) // Проверяем тег
            {
                var health = target.GetComponent<CharacterHealth>();
                if (health != null && health.Health < 100)
                {
                    health.Health += healingPoints;
                    Debug.Log($"Dealt {healingPoints} healing to {target.name}");
                }
            }
        }
        Destroy(gameObject); // Уничтожаем объект после применения урона
    }
    private bool IsTagAllowed(string tag)
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
