using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealthPoint;
    [SerializeField] private float _healthPoint;

    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _healthText;

    private void Start()
    {
        _healthPoint = _maxHealthPoint;
        _healthSlider = _healthSlider.GetComponent<Slider>();
        _healthText = _healthText.GetComponent<TextMeshProUGUI>();
        UpdateHPUI();
    }

    private void GetDamage(float _damage)
    {
        _healthPoint -= _damage;
        UpdateHPUI();

        if (_healthPoint <= 0)
        {
            Death();
        }
    }

    private void UpdateHPUI()
    {
        _healthSlider.maxValue = _maxHealthPoint;
        _healthSlider.value = _healthPoint;

        _healthText.SetText("HP: " + _healthPoint.ToString());
    }

    private void Death()
    {

    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            var _enemy = collision.gameObject.GetComponentInParent<ZombieControll>();
            if (_enemy != null)
            {
                GetDamage(_enemy.damage);
            }
        }
    }
}
