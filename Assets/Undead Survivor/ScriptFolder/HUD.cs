    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class HUD : MonoBehaviour
    {
        // enum: 서로 관련 있는 상수들의 집합을 정의하는 타입
        public enum InfoType { Exp, Level, Kill, Time, Health }

        public InfoType type; // enum으로 선언된 InfoType형 변수

        Text myText;
        Slider mySlider;

        private void Awake()
        {
            myText = GetComponent<Text>();
            mySlider = GetComponent<Slider>();
        }

        private void LateUpdate()
        {
            // 여러 스크립트에서 사용을 위해
            switch (type)
            {
                case InfoType.Exp:
                    float curExp = GameManager.Instance.exp; // 현재 경험치
                    float maxExp = GameManager.Instance.nextExp[GameManager.Instance.level]; // 채워야할 경험치
                    mySlider.value = curExp / maxExp;
                    break;
                case InfoType.Level:
                    myText.text = string.Format("Lv.{0:F0}", GameManager.Instance.level); // F는 소숫점 자릿수
                    break;
                case InfoType.Kill:
                    myText.text = string.Format("{0:F0}", GameManager.Instance.kill);
                    break;
                case InfoType.Time:
                    // 남은 시간
                    float remainTime = GameManager.Instance.maxGameTime - GameManager.Instance.gameTime;
                    int min = Mathf.FloorToInt(remainTime / 60);
                    int sec = Mathf.FloorToInt(remainTime % 60);
                    myText.text = string.Format("{0:D2} : {1:D2}", min, sec); // D는 자릿수 지정
                    break;
                case InfoType.Health:
                    float curHealth = GameManager.Instance.health; 
                    float maxHealth = GameManager.Instance.maxHealth;
                    mySlider.value = curHealth / maxHealth;
                    break;
            }
        }

    }
