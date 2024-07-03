using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CastleHealth : MonoBehaviour
{

    // 체력 슬라이더 
    public Slider Sliders;
    // 성의 초기 체력
    public float HealthCastle = 1000f;
    // 패배 시 활성화될 오브젝트
    public GameObject YouLose;

    void Start()
    {
        // 슬라이더의 최대값을 성의 초기 체력으로 설정
        Sliders.maxValue = HealthCastle;
    }

    void Update()
    {
        // 매 프레임마다 슬라이더 값을 현재 성의 체력으로 설정
        Sliders.value = HealthCastle;

       
        StartCoroutine(HealthNull());  // 체력 0이 되면 패배 처리 코루틴
    }

    // 성의 체력이 0 이하일 때 패배 처리를 하는 코루틴
    IEnumerator HealthNull()
    {
        if (HealthCastle <= 0)
        {
            // 플레이어가 Esc 키를 눌렀을 때 시간 흐름을 재개
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Time.timeScale = 1;
            }

            // 패배 메시지 오브젝트 활성화
            YouLose.SetActive(true);
            // 5초 대기
            yield return new WaitForSeconds(5);
            // 메인 메뉴로 씬 변경
            SceneManager.LoadScene(0);
        }
    }
}