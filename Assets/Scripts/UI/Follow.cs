using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // playerÀÇ world ÁÂÇ¥¸¦ screen ÁÂÇ¥·Î º¯°æ
        _rect.position = Camera.main.WorldToScreenPoint(GameManager.Instance.Player.transform.position);
    }
}
