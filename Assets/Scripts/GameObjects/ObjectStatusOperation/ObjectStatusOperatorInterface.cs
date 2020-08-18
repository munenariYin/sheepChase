using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObjectStatusOperatorInterface
{
    void Init();
    void OperationStatus(ActionInternalStatus actionStatus, ActionExternalStatus externalStatus);
}