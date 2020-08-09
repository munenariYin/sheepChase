using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// これ自体をプールに入れるのも視野に
/// </summary>
static public class ObjectStatusOperationFactory
{
    static public ObjectStatusOperatorInterface CreateStatusOperation(ObjectStatusOperationMethod operationMethod)
    {
        ObjectStatusOperatorInterface statusOperator = null;
        switch(operationMethod)
        {
            case ObjectStatusOperationMethod.Acceleration:
                statusOperator = new Acceleration();
                break;
        }
        return statusOperator;
    }
}
