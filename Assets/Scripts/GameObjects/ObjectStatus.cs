using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/ObjectStatus")]
public class ObjectStatus : ScriptableObject
{
    [SerializeField]
    public float maxSpeed = 0.0f;
    [SerializeField]
    public float accelSpeed = 0.0f;
    [SerializeField]
    public float decelSpeed = 0.0f;
    [SerializeField]
    public float maxTurnAngle = 0.0f;
    [SerializeField]
    public CharacterType characterType = CharacterType.Limit;
}

public struct ObjectExternalStatus
{
    Vector3 Position;
}

static class VectorHelper
{
    static public void Set(this Vector3 self, in Vector3 pos)
    {
        self.x = pos.x;
        self.y = pos.y;
        self.z = pos.z;
    }

    static public float Cross(this Vector3 baseDirection, in Vector3 diffDirection)
    {
        return (baseDirection.x * diffDirection.y - baseDirection.y * diffDirection.x);
    }
}

public enum CharacterType : byte
{
    Whistle,
    Dog,
    Sheep,
    Limit
}
