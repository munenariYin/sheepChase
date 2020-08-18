using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public struct StatusOperationAccessProperty
{
    public int index;
    public ObjectStatusOperationMethod method;
    public bool isActive;
    
    public void Init(int index, ObjectStatusOperationMethod method)
    {
        this.index = index;
        this.method = method;
        this.isActive = false;
    }
}

/// <summary>
/// ステータス操作を行うクラス
/// ひとまず仮
/// オブジェクト生成後に使用する操作を全部見て作られる
/// </summary>
public sealed class ObjectStatusOperator
{
    private StatusOperationAccessProperty[] accessProperties;
    private ObjectStatusOperatorInterface[] operations;

    /// <summary>
    /// 受け取ったObjectStatusOperationMethodから対応する操作を生成。アクセス用の構造体を初期化
    /// </summary>
    /// <param name="operationMethods"></param>
    public void Init(ObjectStatusOperationMethod[] operationMethods)
    {
        int operationCount = operationMethods.Length;
        accessProperties = new StatusOperationAccessProperty[operationCount];
        operations = new ObjectStatusOperatorInterface[operationCount];
        for(int index = 0; index < operationCount; index++)
        {
            operations[index] = ObjectStatusOperationFactory.CreateStatusOperation(operationMethods[index]);
            var accessProperty = new StatusOperationAccessProperty();
            accessProperty.Init(index, operationMethods[index]);
            accessProperties[index] = accessProperty;
        }
    }

    /// <summary>
    /// 実行する操作の切り替え
    /// </summary>
    /// <param name="enableMethods"> 起動するOperationのリスト。 enumの番号順に揃っている事が前提。</param>
    public void SetStatusOperations(ObjectStatusOperationMethod[] enableMethods)
    {
        int enableIndex = 0;
        int length = enableMethods.Length;
        int propertiesLength = accessProperties.Length;

        // 実行する操作をenableMethodsを参照して起動。それ以外はOFF
        for(int index = 0; index < propertiesLength; index++)
        {
            if(enableIndex < length && accessProperties[index].method == enableMethods[enableIndex])
            {
                accessProperties[index].isActive = true;
                enableIndex++;
                continue;
            }
            accessProperties[index].isActive = false;
        }
    }

    /// <summary>
    /// 行動処理
    /// </summary>
    /// <param name="actionStatus"></param>
    /// <param name="externalStatus"></param>
    public void OperationStatus(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus)
    {
        foreach(var property in accessProperties)
        {
            if(property.isActive)
            {
                operations[property.index].OperationStatus(actionStatus, externalStatus);
            }
        }
    }
}
