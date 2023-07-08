using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private GameObject selectedPath;
    private List<GameObject> allUnits;

    public void AppendUnit(GameObject unit)
    {
        allUnits.Add(unit);
    }

    public void DestroyUnit(GameObject unit)
    {
        allUnits.Remove(unit);
    }

    public void SelectedPath(GameObject path)
    {
        selectedPath = path;
    }
}
