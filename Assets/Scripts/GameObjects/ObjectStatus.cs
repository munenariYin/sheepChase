﻿using System.Collections;
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

// 行動系統の処理に必要
// 行動クラスで変更せず、判断クラスでだけ値をいじれるようにしたい
public sealed class ActionInternalStatus
{
    public float speed;
    public Vector2 targetDirection;
    public ObjectStatus objectStatus;
}

/// <summary>
/// 
/// </summary>
public sealed class ActionExternalStatus
{
    public Vector2 Position;
    public Quaternion Rotation;
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
