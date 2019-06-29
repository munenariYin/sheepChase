using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActionController : MonoBehaviour
{
    private List<ObjectActionInfo> actionList = new List<ObjectActionInfo>();

    private void Start()
    {
        
    }

    private void Update()
    {
        foreach(var action in actionList)
        {
            if(!action.isActive)
            {
                continue;
            }

            action.Update();
        }
    }

    /// <summary>
    /// 行動情報を入れたクラス。
    /// 行動をinterfacwで定義したため、こちら側で情報を定義
    /// </summary>
    private class ObjectActionInfo
    {
        ObjectActionInfo()
        {

        }
        private ObjectActionInterface action = null;

        public bool isActive = false;

        public void Update()
        {
            action.Update();
        }
    }
}
