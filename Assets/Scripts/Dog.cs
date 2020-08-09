using Unity.Transforms;
using Unity.Entities;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField]
    float turnAngleMax = 5.0f;

    GameObject target = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            //target = GameObject.Find("Whistle");
            //if (target == null) return;
            return;
        }

        this.transform.localPosition += this.transform.up * 1.0f * Time.deltaTime;

        Vector3 upDirection = this.transform.up;
        Vector3 direction = (new Vector3(target.transform.localPosition.x, target.transform.localPosition.y) - this.transform.localPosition).normalized;
        float dot = Vector3.Dot(upDirection, direction);
        dot = Mathf.Clamp(dot, -1.0f, 1.0f);

        if (0.99f < dot)
        {
            return;
        }
        float angle = Mathf.Rad2Deg * Mathf.Acos(dot);
        if (turnAngleMax < angle)
        {
            angle = turnAngleMax;
        }

        float cross = upDirection.Cross(in direction);

        if (cross < 0.0f)
        {
            angle *= -1.0f;
        }

        // 回転の確定
        this.transform.localRotation *= Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void CollisionEvent(Whistle collider)
    {
        this.target = collider.gameObject;
    }

    public void Collision(Whistle.Collidor collidor)
    {
        this.target = null;
    }

}
