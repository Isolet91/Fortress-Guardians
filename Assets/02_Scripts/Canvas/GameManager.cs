using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager instance;

    // 배경 음악을 재생하는 AudioSource
    public AudioSource backGround;

    // 화살 소리를 재생하는 AudioSource들의 리스트
    public List<AudioSource> arrowSounds;

    // 배경 음악 볼륨을 조절하는 슬라이더
    public Slider backGroundControl;

    // 효과음 볼륨을 조절하는 슬라이더
    public Slider effectControl;

    // 플레이어의 상태를 표시하는 UI 오브젝트
    public GameObject stat;

    private void Awake()
    {
        // 싱글톤 패턴 구현: 인스턴스가 이미 존재하지 않는 경우 현재 인스턴스를 할당
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // 배경 음악의 초기 볼륨을 슬라이더의 값으로 설정
        backGroundControl.value = backGround.volume;

        // 모든 화살 소리의 초기 볼륨을 슬라이더의 값으로 설정
        foreach (var arrowSound in arrowSounds)
        {
            effectControl.value = arrowSound.volume;
        }
    }

   

    // 배경 음악,효과음 음소거
    public void Mute()
    {
        backGround.mute = !backGround.mute;
        
        // 화살 소리 음소거 설정을 배경 음악과 동일하게
        foreach (var arrowSound in arrowSounds)
        {
            arrowSound.mute = backGround.mute;
        }

    }

    public void Update()
    {
        // 효과음 슬라이더의 값으로 모든 화살 소리의 볼륨을 설정
        foreach (var arrowSound in arrowSounds)
        {
            arrowSound.volume = effectControl.value;
        }
    }

    // 배경 음악의 볼륨을 슬라이더의 값으로 업데이트하는 함수
    public void UpdateBackgroundVolume()
    {
        backGround.volume = backGroundControl.value;
    }

    // 플레이어의 상태 창을 열거나 닫는 함수
    public void OpenPlayerStat()
    {
        if (stat.activeSelf)
        {
            stat.SetActive(false); // 현재 활성화 상태면 비활성화
        }
        else
        {
            stat.SetActive(true); // 현재 비활성화 상태면 활성화
        }
    }
}