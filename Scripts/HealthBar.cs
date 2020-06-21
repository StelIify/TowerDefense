using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foreGroundImage;

    [SerializeField]
    private float updateHealthSpeed = 0.2f;

    private void Awake()
    {
        GetComponentInParent<EnemyDamage>().OnHealthPctChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }
    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foreGroundImage.fillAmount;
        float elapsed = 0f;

        while(elapsed < updateHealthSpeed)
        {
            elapsed += Time.deltaTime;
            foreGroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateHealthSpeed);
            yield return null;
        }
        foreGroundImage.fillAmount = pct;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
