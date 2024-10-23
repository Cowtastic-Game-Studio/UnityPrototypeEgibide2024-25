using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "Storage")]
public class IStorage : ScriptableObject
{
    private int maxRes;
    private int level;
    private int resource;
}
