using UnityEngine;
using DefaultNamespace.Components.Interfaces;
using System.Collections.Generic;

public class AppllyDamage : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> Targets { get ; set ; }

    public void Execute()
    {
        Debug.Log("DAMAGE");
    }
}
