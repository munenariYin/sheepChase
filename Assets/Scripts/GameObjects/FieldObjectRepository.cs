using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjectRepository
{
    private Dictionary<int, List<FieldObject>> objects = new Dictionary<int, List<FieldObject>>();

    public FieldObjectRepository()
    {
    }

    public void AddFieldObject(CharacterType charaType, FieldObject fieldObject)
    {
        List<FieldObject> addTargetList = null;
        int intCharaType = (int)charaType;
        if(!objects.TryGetValue(intCharaType, out addTargetList))
        {
            addTargetList = new List<FieldObject>();
            objects.Add(intCharaType, addTargetList);
        }
        
        if(!addTargetList.Contains(fieldObject))
        {
            addTargetList.Add(fieldObject);
        }
    }

    public bool GetFieldObjects(CharacterType getType, out List<FieldObject> fieldObjects)
    {
        if (!objects.TryGetValue((int)getType, out fieldObjects))
        {
            return false;
        }
        return true;
    }

}
