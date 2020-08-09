using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// これ自体はフィールドに出すクラス
/// タッチ関係のクラスから直接は出したくない
/// ゲーム処理がハードを仲介してこれをだす
/// </summary>
public class Whistle : MonoBehaviour
{
    // ハード依存コード
    [SerializeField] private float touchPhaseMoveMagunitude = 1.0f;
    TouchPhase touchPhase = TouchPhase.Ended;

    private Vector2 position;
    private Collidor collidor = new Collidor();
    private Dog dog = null;
    public bool IsSounding
    {
        get; set;
    }

    public Vector3 Position
    {
        get
        {
            return this.transform.localPosition;
        }
        set
        {
            this.transform.localPosition = value;
        }
    }

    void Start()
    {
        collidor.owner = this.gameObject;
        // できれば外部に持って行きたい
        dog = GameObject.Find("Dog").GetComponent<Dog>();
    }

    // Update is called once per frame
    void Update()
    {
        EditorInput();
        TouchPhase touchPhase = this.touchPhase;
        collidor.Collision(dog);
        if (touchPhase == TouchPhase.Canceled || touchPhase == TouchPhase.Ended)
        {
            return;
        }

        Position = this.Position;
        if (touchPhase == TouchPhase.Began)
        {
            collidor.isCollision = false;
            Message(dog);
            //IsSounding = true;
        }
    }

    public void Message(Dog dog)
    {
        dog.CollisionEvent(this);
    }

    private void EditorInput()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            // 離れた状態の時の管理
            if (this.touchPhase == TouchPhase.Canceled)
            {
                this.touchPhase = TouchPhase.Ended;
            }

            if (this.touchPhase != TouchPhase.Ended)
            {
                this.touchPhase = TouchPhase.Canceled;
            }

            return;
        }

        Vector2 nowMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (this.touchPhase == TouchPhase.Canceled || this.touchPhase == TouchPhase.Ended)
        {
            this.Position = nowMousePosition;


            this.touchPhase = TouchPhase.Began;

            return;
        }

        Vector2 deltaMousePosition = nowMousePosition - new Vector2(this.Position.x, this.Position.y);

        this.Position = nowMousePosition;
        if (this.touchPhaseMoveMagunitude < deltaMousePosition.magnitude)
        {
            this.touchPhase = TouchPhase.Moved;
        }
        else
        {
            this.touchPhase = TouchPhase.Stationary;
        }
    }

    public class Collidor
    {
        public GameObject owner;
        public bool isCollision = true;
        public void Collision(Dog dog)
        {
            float x = (dog.transform.localPosition.x - owner.transform.localPosition.x) * (dog.transform.localPosition.x - owner.transform.localPosition.x);
            float y = (dog.transform.localPosition.y - owner.transform.localPosition.y) * (dog.transform.localPosition.y - owner.transform.localPosition.y);
            float ownerSize = (owner.transform.localScale.x * owner.transform.localScale.y);

            if (x + y <= ownerSize)
            {
                isCollision = true;
                dog.Collision(this);
            }
        }
    }
}
