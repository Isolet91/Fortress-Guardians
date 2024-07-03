using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour {

    public Slider Slider;          //체력 슬라이더 UI
    public float HealthE = 100f;   //적의 초기 체력

    //public int ExpMin = 1;     
    //public int ExpMax = 5;

    public int CoinMin = 10;       //획득 코인 최소값
    public int CoinMax = 20;       //획득 코인 최대값

    //private int Exp;
    private int Coin;              //코인의 실제 값

    private GameObject Res;        //메인카메라 오브젝트 참조 변수

    private Animator animator;     //애니메이터

    void Start()
    {
       // Exp = Random.Range(ExpMin, ExpMax);
        Coin = Random.Range(CoinMin, CoinMax);                  //적 죽였을 때 코인 값 랜덤으로
        Slider.maxValue = HealthE;                              //적 체력 슬라이더 최대값 초기화
        Res = GameObject.Find("Main Camera");                   //메인카메라 찾아서 참조 
        animator = gameObject.GetComponent<Animator>();         //애니메이터 가져오기
    }

	void Update ()
    {
        Main();
	}
    
    void Main()
    {
        Slider.value = HealthE;

        //적 체력이 0이면 적을 없애고 보상을 줌
        if(HealthE <= 0)
        {
            if(Res.GetComponent<ResourcesCastle>() != null)           //ResourcesCastle 스크립트가 붙어있으면, 적 처치 보상 처리
            {
                if (Res.GetComponent<ResourcesCastle>().isWave)
                {
                    Res.GetComponent<ResourcesCastle>().enemyCount++; //웨이브 중이면 enemyCount 증가
                }
                Res.GetComponent<ResourcesCastle>().Coins += Coin;    //코인 보상
            }
            Destroy(gameObject);                                      //파괴
        }
    }

    public void HitEnemy(float damage)     //적이 피해를 입었을 때
    {
        HealthE -= damage;                 //체력을 감소시킴
        animator.SetTrigger("isHit");      //애니메이터 isHit 트리거 설정(피격 애니메이션)
    }
}
