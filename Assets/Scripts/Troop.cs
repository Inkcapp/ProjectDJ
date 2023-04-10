using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Troop", menuName = "SOs/Troop", order = 1)]
public class Troop : ScriptableObject
{
    public GameObject[] battlers;
}
