using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

// キャラクターが所持するクラス。
public class FieldObject : MonoBehaviour
{
    static public void SaveStateMachine()
    {
        // FieldObjectInitializeProperty initializeProperty = new FieldObjectInitializeProperty();
        // initializeProperty.allActionMethods = new ActionMethod[]{ActionMethod.Idle, ActionMethod.Move};
        // initializeProperty.allStatusOperations = new ObjectStatusOperationMethod[]{ObjectStatusOperationMethod.Acceleration, ObjectStatusOperationMethod.Deceleration};

        // // Stateのセット
        // // 0
        // ObjectStateInitializeProperty objectStateInitProp1 = new ObjectStateInitializeProperty();
        // objectStateInitProp1.actionMethods = new ActionMethod[]{ActionMethod.Idle};
        // objectStateInitProp1.statusOperations = new ObjectStatusOperationMethod[]{};
        // TransitionDeciderProperty prop1DeciderProp1 = new TransitionDeciderProperty();
        // prop1DeciderProp1.transitionIndex = 1;
        // prop1DeciderProp1.conditionJudgeMethods = new JudgeConditionMethod[]{JudgeConditionMethod.Timer};
        // objectStateInitProp1.transitionProperties = new TransitionDeciderProperty[]{prop1DeciderProp1};
        // // 1
        // ObjectStateInitializeProperty objectStateInitProp2 = new ObjectStateInitializeProperty();
        // objectStateInitProp2.actionMethods = new ActionMethod[]{ActionMethod.Move};
        // objectStateInitProp2.statusOperations = new ObjectStatusOperationMethod[]{ObjectStatusOperationMethod.Acceleration};
        // TransitionDeciderProperty prop2DeciderProp1 = new TransitionDeciderProperty();
        // prop2DeciderProp1.transitionIndex = 2;
        // prop2DeciderProp1.conditionJudgeMethods = new JudgeConditionMethod[]{JudgeConditionMethod.Timer};
        // objectStateInitProp2.transitionProperties = new TransitionDeciderProperty[]{prop2DeciderProp1};
        // // 2
        // ObjectStateInitializeProperty objectStateInitProp3 = new ObjectStateInitializeProperty();
        // objectStateInitProp3.actionMethods = new ActionMethod[]{ActionMethod.Move};
        // objectStateInitProp3.statusOperations = new ObjectStatusOperationMethod[]{ObjectStatusOperationMethod.Deceleration};
        // TransitionDeciderProperty prop3DeciderProp1 = new TransitionDeciderProperty();
        // prop3DeciderProp1.transitionIndex = 0;
        // prop3DeciderProp1.conditionJudgeMethods = new JudgeConditionMethod[]{JudgeConditionMethod.Timer};
        // objectStateInitProp3.transitionProperties = new TransitionDeciderProperty[]{prop3DeciderProp1};
        // // Stateの終わり
        // initializeProperty.stateInitializeProperties = new ObjectStateInitializeProperty[]{objectStateInitProp1, objectStateInitProp2, objectStateInitProp3};

        
        // var serializedObject = MessagePack.MessagePackSerializer.Serialize<FieldObjectInitializeProperty>(initializeProperty);
        // Debug.LogFormat("{0}", System.IO.Path.GetFullPath("./statemachine.binary"));
        // System.IO.FileStream stream = new System.IO.FileStream("./statemachine.binary", System.IO.FileMode.Create, System.IO.FileAccess.Write);
        // using(var binaryWriter = new System.IO.BinaryWriter(stream))
        // {
        //     binaryWriter.Write(serializedObject);
        // }

        System.IO.FileStream stream = new System.IO.FileStream("./statemachine.binary", System.IO.FileMode.Open, System.IO.FileAccess.Read);
        using(var binaryReader = new System.IO.BinaryReader(stream))
        {
        }
    }
    [SerializeField]
    private ObjectStatus objectStatus;
    private FieldObjectRepository fieldObjectRepository = null;
    private ActionInternalStatus actionStatus = new ActionInternalStatus();
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
        SaveStateMachine();
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
