using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public sealed class Core : MonoBehaviour
{
    // =========================================================================
    // Property
    // ...
    static public Core instance
    {
        get;
        private set;
    }

    public InputManager inputManager
    {
        get;
        private set;
    }

    public FieldObjectRepository fieldObjectRepository
    {
        get;
        private set;
    }

    // =========================================================================


    // =========================================================================
    // Unity Message Methods
    // ...

    /// <summary>
    /// 初めて作られた場合は単一instanceとして登録する
    /// そうでなければ警告して削除
    /// </summary>
    private void Awake()
    {
        if(instance == null)
        {
            CreateInstance();
        }
        else
        {
            DestroyDuplicatebject();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        inputManager.Update();
    }

    // =========================================================================


    // =========================================================================
    // private Methods
    // ...

    private void DestroyDuplicatebject()
    {
        Destroy(gameObject);

        Debug.LogWarning("Singletonオブジェクトが複製されようとしました");
    }

    private void CreateInstance()
    {
        DontDestroyOnLoad(gameObject);

        instance = this;

        inputManager = new InputManager();

        fieldObjectRepository = new FieldObjectRepository();
    }

    //=========================================================================


}
