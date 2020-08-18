using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{

    // Dog chaser = null;
    // [SerializeField]
    // float turnAngleMax = 5.0f;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     GameObject dog = GameObject.Find("Dog");
    //     this.chaser = dog.GetComponent<Dog>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (chaser == null)
    //     {
    //         Debug.LogWarning("Not Found Dog.");
    //     }

    //     // 範囲判定(視界とかも欲しい気がする)
    //     float distance = (chaser.transform.localPosition - this.transform.localPosition).magnitude;
    //     if (1.0f < distance)
    //     {
    //         //Debug.Log(distance);
    //         return;
    //     }



    //     // 適当に犬の処理を流用して作った。直さないと死ぬ
    //     this.transform.localPosition += this.transform.up * 1.0f * Time.deltaTime;

    //     Vector3 upDirection = this.transform.up;

    //     Vector3 vec3SheepPos = new Vector3(chaser.transform.position.x, chaser.transform.position.y);
    //     Vector3 direction = (this.transform.localPosition - vec3SheepPos).normalized;

    //     float dot = Vector3.Dot(upDirection, direction);
    //     dot = Mathf.Clamp(dot, -1.0f, 1.0f);

    //     if (0.99f < dot)
    //     {
    //         return;
    //     }
    //     float angle = Mathf.Rad2Deg * Mathf.Acos(dot);
    //     if (turnAngleMax < angle)
    //     {
    //         angle = turnAngleMax;
    //     }

    //     float cross = upDirection.x * direction.y - upDirection.y * direction.x;

    //     if (cross < 0.0f)
    //     {
    //         angle *= -1.0f;
    //     }

    //     // 回転の確定
    //     this.transform.localRotation *= Quaternion.AngleAxis(angle, Vector3.forward);
    // }
}
