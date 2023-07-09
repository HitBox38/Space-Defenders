using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    private GameObject currentPath;
    private List<GameObject> allUnits = new List<GameObject>();

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
        currentPath = path;
    }

    public GameObject getPath()
    {
        return currentPath;
    }
}
