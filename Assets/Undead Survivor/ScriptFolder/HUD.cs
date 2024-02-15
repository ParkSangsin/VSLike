    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class HUD : MonoBehaviour
    {
        // enum: ���� ���� �ִ� ������� ������ �����ϴ� Ÿ��
        public enum InfoType { Exp, Level, Kill, Time, Health }

        public InfoType type; // enum���� ����� InfoType�� ����

        Text myText;
        Slider mySlider;

        private void Awake()
        {
            myText = GetComponent<Text>();
            mySlider = GetComponent<Slider>();
        }

        private void LateUpdate()
        {
            // ���� ��ũ��Ʈ���� ����� ����
            switch (type)
            {
                case InfoType.Exp:
                    float curExp = GameManager.Instance.exp; // ���� ����ġ
                    float maxExp = GameManager.Instance.nextExp[GameManager.Instance.level]; // ä������ ����ġ
                    mySlider.value = curExp / maxExp;
                    break;
                case InfoType.Level:
                    myText.text = string.Format("Lv.{0:F0}", GameManager.Instance.level); // F�� �Ҽ��� �ڸ���
                    break;
                case InfoType.Kill:
                    myText.text = string.Format("{0:F0}", GameManager.Instance.kill);
                    break;
                case InfoType.Time:
                    // ���� �ð�
                    float remainTime = GameManager.Instance.maxGameTime - GameManager.Instance.gameTime;
                    int min = Mathf.FloorToInt(remainTime / 60);
                    int sec = Mathf.FloorToInt(remainTime % 60);
                    myText.text = string.Format("{0:D2} : {1:D2}", min, sec); // D�� �ڸ��� ����
                    break;
                case InfoType.Health:
                    float curHealth = GameManager.Instance.health; 
                    float maxHealth = GameManager.Instance.maxHealth;
                    mySlider.value = curHealth / maxHealth;
                    break;
            }
        }

    }
