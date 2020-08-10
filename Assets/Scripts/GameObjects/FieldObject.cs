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
    public ActionMethod[] allActionMethods;
    public ObjectStatusOperationMethod[] allStatusOperations;
    public ObjectStateInitializeProperty[] stateInitializeProperties;
} 

// キャラクターが所持するクラス。
public class FieldObject : MonoBehaviour
{
    [SerializeField]
    private ObjectStatus objectStatus;
    private FieldObjectRepository fieldObjectRepository = null;
    private ActionInternalStatus actionStatus =  new ActionInternalStatus();
    private ActionExternalStatus externalStatus = new ActionExternalStatus();
    private FieldObjectStateMachine stateMachine = new FieldObjectStateMachine();
    private ActionExecuter actionExecuter = null;
    private ObjectStatusOperator statusOperator = null;
    public void SetRepository(FieldObjectRepository repository)
    {
        this.fieldObjectRepository = repository;
    }
    
    private void Awake()
    {
        FieldObjectInitializeProperty initializeProperty = new FieldObjectInitializeProperty();
        initializeProperty.allActionMethods = new ActionMethod[]{ActionMethod.Idle, ActionMethod.Move};
        initializeProperty.allStatusOperations = new ObjectStatusOperationMethod[]{ObjectStatusOperationMethod.Acceleration, ObjectStatusOperationMethod.Deceleration};

        // Stateのセット
        // 0
        ObjectStateInitializeProperty objectStateInitProp1 = new ObjectStateInitializeProperty();
        objectStateInitProp1.actionMethods = new ActionMethod[]{ActionMethod.Idle};
        objectStateInitProp1.statusOperations = new ObjectStatusOperationMethod[]{};
        TransitionDeciderProperty prop1DeciderProp1 = new TransitionDeciderProperty();
        prop1DeciderProp1.transitionIndex = 1;
        prop1DeciderProp1.conditionJudgeMethods = new JudgeConditionMethod[]{JudgeConditionMethod.Timer};
        objectStateInitProp1.transitionProperties = new TransitionDeciderProperty[]{prop1DeciderProp1};
        // 1
        ObjectStateInitializeProperty objectStateInitProp2 = new ObjectStateInitializeProperty();
        objectStateInitProp2.actionMethods = new ActionMethod[]{ActionMethod.Move};
        objectStateInitProp2.statusOperations = new ObjectStatusOperationMethod[]{ObjectStatusOperationMethod.Acceleration};
        TransitionDeciderProperty prop2DeciderProp1 = new TransitionDeciderProperty();
        prop2DeciderProp1.transitionIndex = 2;
        prop2DeciderProp1.conditionJudgeMethods = new JudgeConditionMethod[]{JudgeConditionMethod.Timer};
        objectStateInitProp2.transitionProperties = new TransitionDeciderProperty[]{prop2DeciderProp1};
        // 2
        ObjectStateInitializeProperty objectStateInitProp3 = new ObjectStateInitializeProperty();
        objectStateInitProp3.actionMethods = new ActionMethod[]{ActionMethod.Move};
        objectStateInitProp3.statusOperations = new ObjectStatusOperationMethod[]{ObjectStatusOperationMethod.Deceleration};
        TransitionDeciderProperty prop3DeciderProp1 = new TransitionDeciderProperty();
        prop3DeciderProp1.transitionIndex = 0;
        prop3DeciderProp1.conditionJudgeMethods = new JudgeConditionMethod[]{JudgeConditionMethod.Timer};
        objectStateInitProp3.transitionProperties = new TransitionDeciderProperty[]{prop3DeciderProp1};
        // Stateの終わり
        initializeProperty.stateInitializeProperties = new ObjectStateInitializeProperty[]{objectStateInitProp1, objectStateInitProp2, objectStateInitProp3};

        stateMachine.Init(initializeProperty.stateInitializeProperties);

        actionExecuter = new ActionExecuter();
        actionExecuter.Init(initializeProperty.allActionMethods);
        actionExecuter.SetExecuteActions(stateMachine.GetCurrentObjectState().actionMethods);

        statusOperator = new ObjectStatusOperator();
        statusOperator.Init(initializeProperty.allStatusOperations);
        statusOperator.SetStatusOperations(stateMachine.GetCurrentObjectState().objectStatusOperations);

        objectStatus = new ObjectStatus();
        actionStatus.objectStatus = objectStatus;

        actionStatus.targetDirection = Vector2.up;
        actionStatus.speed = 0.5f;
    }

    private void Update()
    {
        if(stateMachine.TransitionUpdate(actionStatus, externalStatus))
        {
            actionExecuter.SetExecuteActions(stateMachine.GetCurrentObjectState().actionMethods);
            statusOperator.SetStatusOperations(stateMachine.GetCurrentObjectState().objectStatusOperations);
        }
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
