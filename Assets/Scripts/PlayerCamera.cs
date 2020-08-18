using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Transform targetTransCache = null;
    Transform cameraTransCache = null;
    float chaseSpeed = 0.0f;
    
    [SerializeField]
    float maxSpeed = 6.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.cameraTransCache = this.transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        // とりあえず位置合わせるだけ
        if(this.targetTransCache == null)
        {
            GameObject target = GameObject.Find("Dog");
            if (target == null) return;
            targetTransCache = target.transform;
        }

        Vector3 targetPos = this.targetTransCache.localPosition;
        Vector3 cameraPos = this.cameraTransCache.localPosition;

        Vector2 direction =  targetPos - cameraPos;

        float distance = direction.magnitude;

        direction.Normalize();


        this.chaseSpeed -= (0.05f * Time.deltaTime);
        if(this.chaseSpeed < 0.0f)
        {
            this.chaseSpeed = 0.0f;
        }


        if(1.0f < distance)
        {
            this.chaseSpeed += (0.1f * Time.deltaTime);
            if(this.maxSpeed < this.chaseSpeed)
            {
                this.chaseSpeed = this.maxSpeed;
            }
        }


        if(distance < this.chaseSpeed)
        {
            this.chaseSpeed = distance;
        }

        Vector2 velocity = direction * this.chaseSpeed;
        cameraPos += new Vector3(velocity.x, velocity.y);
        this.transform.localPosition = cameraPos;
        

    }
}
