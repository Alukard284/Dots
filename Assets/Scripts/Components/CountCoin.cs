using DefaultNamespace;
using DefaultNamespace.Components.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class CountCoin : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> Targets { get; set; }
    [SerializeField] private string[] allowedTags;
    public int Score;

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target != null && IsTagAllowed(target.tag, allowedTags)) // ��������� ���
            {
                Score++;
            }
        }
        Destroy(gameObject); // ���������� ������ ����� ���������� �����
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

    private void Start()
    {
       
    }
}
