using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsCtrl : MonoBehaviour
{
    [Header("Stats")]
    public int coins;


    [Header("Refs")]
    [SerializeField] private TextMeshProUGUI _coinAmountText;

    private void Start()
    {
        _coinAmountText = _coinAmountText.GetComponent<TextMeshProUGUI>();
        UpdateCoinsText();
    }


    public void GetCoins(int _amount)
    {
        coins += _amount;
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        _coinAmountText.SetText("Player's coins: " + coins.ToString());
    }
}
