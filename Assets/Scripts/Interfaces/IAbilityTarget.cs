using UnityEngine;
using DefaultNamespace.Components.Interfaces;
using System.Collections.Generic;

public interface IAbilityTarget : IAbility
{
    List<GameObject> Targets { get; set; }
}
