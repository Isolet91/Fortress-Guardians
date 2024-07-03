using UnityEngine;

public enum EnemyType   //몬스터의 타입을 저장
{
    Normal,   //일반 적 타입
    Flying,   //비행 적 타입
}

public class AI : MonoBehaviour 
{

    public float Speed;                //적의 이동 속도              
    private Vector2 Point;             //적의 목표 지점(성) 위치
    public GameObject ObjPoint;        //이동할 목표 지점(성) 포인트 오브젝트
    private Rigidbody2D rb;            //적의 Rigidbody2D 컴포넌트

    public EnemyType enemyType;        //적 타입
    
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        Point = ObjPoint.transform.position;  //이동할 목표 지점의 위치
	}

    //Update는 매 프레임 호출되고 , FixedUpdate는 고정된 시간 간격으로 호출됨(물리 연산 및 리지드바디 관련 코드를 배치하는데 유용)
    void FixedUpdate () 
    {   
        Motion();                      
	}

    void Motion()  //적의 움직임을 처리하는 메서드
    {
        switch (enemyType) 
        {
            case EnemyType.Normal:   //일반 적의 경우 이동 패턴 
                rb.velocity = Vector3.down; 
                rb.MovePosition(rb.position + Point * Time.fixedDeltaTime * Speed);
                break;
            case EnemyType.Flying:   //비행하는 적의 이동 패턴  
                rb.velocity = Vector3.down;
                rb.MovePosition(rb.position + new Vector2(Point.x, Point.y + 4.0f) * Time.fixedDeltaTime * Speed);
                break;
        }
    }

    void OnCollisionStay2D(Collision2D colls)
    {
        if (colls.gameObject.tag == "Enemy") //적끼리 서로 충돌하면 충돌 무시
        {
            Physics2D.IgnoreCollision(gameObject.transform.GetComponent<Collider2D>(), colls.transform.GetComponent<Collider2D>());
        }
    }
}
