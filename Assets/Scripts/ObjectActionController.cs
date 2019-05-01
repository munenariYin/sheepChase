using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActionController : MonoBehaviour
{
    private List<ObjectActionInfo> actionList = new List<ObjectActionInfo>();

    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// 行動情報を入れたクラス。
    /// 行動をinterfacwで定義したため、こちら側で情報を定義
    /// </summary>
    public class ObjectActionInfo
    {
        ObjectActionInfo()
        {

        }
        public ObjectActionInterface action;
        public bool isActive = false;

        public void Update()
        {
            action.Update();
        }
    }
}
