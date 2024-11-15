using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType {Exp, Level, Kill, Time, Health};
    public InfoType Type;
    private Text _myText;
    private Slider _mySlider;

    private void Awake()
    {
        _myText = GetComponent<Text>();
        _mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch(Type)
        {
            case InfoType.Exp:
                float curExp = GameManager.Instance.Exp;
                float maxExp = GameManager.Instance.NextExp[GameManager.Instance.Level];
                _mySlider.value = curExp/maxExp;
                break;
            case InfoType.Level:
                _myText.text = $"{GameManager.Instance.Level}";
                break;
            case InfoType.Kill:
                _myText.text = $"{GameManager.Instance.Kill}";
                break;
            case InfoType.Time:
                float remainTime = GameManager.Instance.MaxGameTime - GameManager.Instance.GameTime;
                int minuate = Mathf.FloorToInt(remainTime / 60);
                int second = Mathf.FloorToInt(remainTime % 60);
                _myText.text = string.Format("{0:D2}:{1:D2}", minuate, second);
                break;
            case InfoType.Health:
                float curHealth = GameManager.Instance.Health;
                float maxHealth = GameManager.Instance.MaxHealth;
                _mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
