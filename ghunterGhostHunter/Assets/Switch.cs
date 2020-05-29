using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Naming conventions not applied; json naming conventions overwrite .net naming conventions


[System.Serializable]
public class Switch
{
    public List<SwitchResult> result;
}

[System.Serializable]
public class SwitchResult
{
    // "Off" or "On"
    public string Data;
    public string idx;
}
