using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleController : MonoBehaviour
{
    // private Whistle whistle = null;
    // private InputManager inputManagerCache = null;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     CreateWhistle();
    //     this.inputManagerCache = Core.instance.inputManager;
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     TouchPhase touchPhase = this.inputManagerCache.TouchPhase;
    //     if (touchPhase == TouchPhase.Canceled || touchPhase == TouchPhase.Ended)
    //     {
    //         return;
    //     }

    //     whistle.Position = this.inputManagerCache.Position;

    //     if (touchPhase == TouchPhase.Began)
    //     {
    //         if (this.whistle == null)
    //         {
    //             Debug.LogWarning("whistle is null !!");
    //             this.CreateWhistle();
    //         }
    //         whistle.IsSounding = true;
    //     }
    // }

    // private void CreateWhistle()
    // {
    //     GameObject newGameObject = GameObject.Instantiate(Resources.Load("Prefabs/whistle")) as GameObject;
    //     newGameObject.name = newGameObject.name.Replace("(Clone)", "");
    //     this.whistle = newGameObject.GetComponent<Whistle>();
    // }

}
