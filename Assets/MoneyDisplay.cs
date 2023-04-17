using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    public int money = 0;
    private TextMeshProUGUI moneyText;

    void Start()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
        UpdateMoneyText();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyText();
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "$" + money.ToString();
    }
}
