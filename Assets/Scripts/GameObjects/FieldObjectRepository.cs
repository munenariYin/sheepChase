using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjectRepository
{
    private Dictionary<CharacterType, List<FieldObject>> objects = new Dictionary<CharacterType, List<FieldObject>>();

    public FieldObjectRepository()
    {
    }

    public void AddFieldObject(CharacterType charaType, FieldObject fieldObject)
    {
        List<FieldObject> addTargetList = null;
        if(!objects.TryGetValue(charaType, out addTargetList))
        {
            addTargetList = new List<FieldObject>();
            objects.Add(charaType, addTargetList);
        }
        
        if(!addTargetList.Contains(fieldObject))
        {
            addTargetList.Add(fieldObject);
        }
    }

}
