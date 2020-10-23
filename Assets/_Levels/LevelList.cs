using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Words/LevelList", order = 1)]
public class LevelList : ScriptableObject
{
    public string Menu;
    public string[] levels;
    public string[] Descripcion;

    public bool[] opens;
}
