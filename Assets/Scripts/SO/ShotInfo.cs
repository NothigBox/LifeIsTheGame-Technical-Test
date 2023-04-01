using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotInfo", menuName = "ScriptableObjects/Shots", order = 1)]
public class ShotInfo : ScriptableObject
{
    
}

public enum EShotType { Parabolic, Area, Free }