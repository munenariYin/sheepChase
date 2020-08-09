using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

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

public struct FieldObjectInitializeProperty
{
    ActionMethod[] allActionMethods;
    ObjectStatusOperationMethod[] allStatusOperations;
    ObjectStateInitializeProperty stateInitializeProperty;
} 

public struct ObjectStateInitializeProperty
{
    ActionMethod[] actionMethods;
    ObjectStatusOperationMethod[] statusOperations;
}

public struct TransitionDeciderProperty
{
    public int[] conditionJudgeMethods;
    public int judgeType;
    public int transitionIndex;
}

/// <summary>
/// 状態を配列で所持する。
/// </summary>
public sealed class FieldObjectStateMachine
{
    private int currentStateIndex = 0;
    private FieldObjectState[] states = null;

    /// <summary>
    ///  状態遷移
    /// </summary>
    /// <param name="actionStatus"></param>
    /// <param name="externalStatus"></param>
    /// <returns></returns>
    public bool TransitionUpdate(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus)
    {
        int nextStateIndex = -1;
        if(states[currentStateIndex].CheckTransition(ref nextStateIndex, actionStatus, externalStatus))
        {
            currentStateIndex = nextStateIndex;
            return true;
        }
        return false;
    }

    public FieldObjectState GetCurrentObjectState()
    {
        return states[currentStateIndex];
    }
}

/// <summary>
/// ステートマシンが持つ1つの状態
/// </summary>
public sealed class FieldObjectState
{
    private StateTransitionDecider[] transitionDeciders = null;
    private ActionMethod[] actionMethods = null;
    private ObjectStatusOperationMethod[] objectStatusOperations = null;

    public void Init(ActionMethod[] actionMethods, ObjectStatusOperationMethod[] operations)
    {
        this.actionMethods = actionMethods;
        this.objectStatusOperations = operations;
    }
    public bool CheckTransition(ref int nextStateIndex, ActionInternalStatus actionStatus, ActionExternalStatus externalStatus)
    {
        foreach(var decider in transitionDeciders)
        {
            if(decider.CheckTransition(actionStatus, externalStatus))
            {
                nextStateIndex = decider.transitionIndex;
                return true; 
            }
        }
        return false;
    }
}

/// <summary>
/// 状態遷移を行う
/// </summary>
public class StateTransitionDecider
{
    private int judgeType = 0; // sequenceなら全判定
    private JudgeConditionInterface[] judgeConditions = null;
    public int transitionIndex = 0;

    public void Init(TransitionDeciderProperty property)
    {
        this.judgeType = property.judgeType;
        //this.judgeConditions = property.conditionJudgeMethods;
        this.transitionIndex = property.transitionIndex;
    }

    public bool CheckTransition(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus)
    {
        foreach(var judge in judgeConditions)
        {
            if(! judge.CheckTransition(actionStatus, externalStatus))
            {
                return false;
            }
        }
        return true;
    }
}


// キャラクターが所持するクラス。
public class FieldObject : MonoBehaviour
{
    [SerializeField]
    private ObjectStatus objectStatus;
    private FieldObjectRepository fieldObjectRepository = null;
    private ActionInternalStatus actionStatus =  new ActionInternalStatus();
    private ActionExternalStatus externalStatus = new ActionExternalStatus();
    private ActionExecuter actionExecuter = null;
    private ObjectStatusOperator statusOperator = null;
    public void SetRepository(FieldObjectRepository repository)
    {
        this.fieldObjectRepository = repository;
    }
    
    private void Awake()
    {
        actionExecuter = new ActionExecuter();
        actionExecuter.Init(new ActionMethod[]{ActionMethod.Move});
        actionExecuter.SetExecuteActions(new ActionMethod[]{ActionMethod.Move});

        statusOperator = new ObjectStatusOperator();
        statusOperator.Init(new ObjectStatusOperationMethod[]{ObjectStatusOperationMethod.Acceleration});
        statusOperator.SetStatusOperations(new ObjectStatusOperationMethod[]{ObjectStatusOperationMethod.Acceleration});

        actionStatus.objectStatus = objectStatus;

        actionStatus.targetDirection = Vector2.up;
        actionStatus.speed = 0.5f;
    }

    private void Update()
    {
        statusOperator.OperationStatus(actionStatus, externalStatus);
        actionExecuter.Action(actionStatus, externalStatus);

        transform.localPosition = externalStatus.Position;
        ref Quaternion quat = ref externalStatus.Rotation;
        transform.localRotation.Set(quat.x, quat.y, quat.z, quat.w);
    }

    private void OnDestroy()
    {
        
    }
}
