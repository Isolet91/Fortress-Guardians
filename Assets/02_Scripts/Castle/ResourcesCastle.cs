using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourcesCastle : MonoBehaviour
{
    public float Coins;           //플레이어 코인 수
    public int Wave = 1;          //현재 웨이브 수

    public GameObject Cl1;        //성 올리기 레벨 1
    public GameObject Cl2;        //성 올리기 레벨 2
    public GameObject Cl3;        //성 올리기 레벨 3

    public Text CoinsText;        //코인 수 텍스트 UI
    public Text WaveText;         //웨이브 레벨 표시 텍스트 UI

    public bool isWave = true;   //웨이브가 진행 중인지?
    

    #region 게임 전환 텍스트
    [SerializeField]
    private GameObject startStageText;      // 웨이브 시작 텍스트
    [SerializeField]    
    private GameObject endStageText;        // 웨이브 종료 텍스트  
    [SerializeField]
    private GameObject victoryStageText;    // 승리 텍스트
    [SerializeField]
    private GameObject startButton;         // 시작 버튼
    #endregion

    [SerializeField]
    private GameObject[] upgradeButtons;    // 업그레이드 버튼

    [Space(10f)]
    [SerializeField]
    private GameObject healthCanvas;        // 체력 캔버스
    [SerializeField]
    private Transform[] sliderPos;          // 슬라이더 위치

    public int enemyCount = 0;                        // 적 수
    public int[] stageEnemyCount = new int[5];        // 각 스테이지 적 수

    public static ResourcesCastle instance; // 싱글톤
    void Start()
    {
        if (instance == null)
        {
            instance = this;                // 싱글톤 초기화
        }
    }

    void Update()
    {
        //현재 웨이브가 총 웨이브 수 이하인 경우 (배열 검사)
        if (Wave <= stageEnemyCount.Length)    
        {
            if (enemyCount >= stageEnemyCount[Wave - 1])
            {
                enemyCount = 0;
                StartCoroutine(WaveUp());
            }
        }

        Texts();          //웨이브 올라가면 텍스트 업데이트
        LvlCastle();      //성 올라가면 체력 바 위로 올려줌
    }

    IEnumerator WaveUp()
    {
        isWave = false;   //시작 버튼 비활성화
        Wave++;           //웨이브 증가

        if (Wave > 5)     //웨이브 올 클리어 시
        {
            victoryStageText.SetActive(true);     //승리 텍스트 활성화
            Time.timeScale = 0.0f;                //게임 일시정지
            yield return 0;
                                                  // 메인메뉴로 돌아가는 오브젝트 소환
        }
        else
        {
            endStageText.SetActive(true);                //웨이브 종료 텍스트 활성화
            yield return new WaitForSeconds(2.0f);       //2초 대기
            endStageText.SetActive(false);               //웨이브 종료 텍스트 비활성화

            startButton.SetActive(true);                 //시작 버튼 활성화
        }

        foreach (GameObject upgradeButton in upgradeButtons)  //모든 업그레이드 버튼 활성화
        {
            upgradeButton.SetActive(true);
        }
        //웨이브에 따라 적 스폰 타이머 조절
        if (Wave == 2)
        {
            gameObject.GetComponent<EnemySpawn>().TimerMax -= 2;
        }
        else if (Wave == 3)
        {
            gameObject.GetComponent<EnemySpawn>().TimerMax -= 3;
        }
        else if (Wave == 4)
        {
            gameObject.GetComponent<EnemySpawn>().TimerMin -= 1;
            gameObject.GetComponent<EnemySpawn>().TimerMax -= 4;
        }
        else if (Wave == 5)
        {
            gameObject.GetComponent<EnemySpawn>().TimerMin -= 2;
            gameObject.GetComponent<EnemySpawn>().TimerMax -= 4;
        }
    }

    public void WaveStartFunc()
    {
        StartCoroutine(WaveStart());   //웨이브 시작 코루틴
    }

    IEnumerator WaveStart()
    {
        startButton.SetActive(false);                           //시작버튼 비활성화

        startStageText.SetActive(true);                         //웨이브 시작 텍스트 활성화
        yield return new WaitForSeconds(2.0f);                  //2초 대기
        startStageText.SetActive(false);                        //웨이브 시작 텍스트 비활성화

        foreach (GameObject upgradeButton in upgradeButtons)    //모든 업그레이드 버튼 비활성화
        {
            upgradeButton.SetActive(false);
        }

        gameObject.GetComponent<EnemySpawn>().enemyCount = 0;   //적 수 초기화

        isWave = true;                                          //웨이브 진행 중으로 변경
    }

    void Texts()    //웨이브 클리어시 텍스트 업데이트 메서드
    {
        CoinsText.text = Coins.ToString();                      //코인 수 업데이트
        if (Wave > 5)                                           //웨이브 올 클리어시 출력
        {
            WaveText.text = "Wave All Clear";
        }
        else                                                    //아니면 현재 웨이브 업데이트
        {
            WaveText.text = "Wave:" + Wave.ToString();
        }
    }

    void LvlCastle()  //성 레벨 오를때마다 체력바 위치 조정 메서드
    {
        if (Cl1.activeInHierarchy == true)
        {
            healthCanvas.transform.position = sliderPos[0].position;
        }
        if (Cl2.activeInHierarchy == true)
        {
            healthCanvas.transform.position = sliderPos[1].position;
        }
        if (Cl3.activeInHierarchy == true)
        {
            healthCanvas.transform.position = sliderPos[2].position;
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
