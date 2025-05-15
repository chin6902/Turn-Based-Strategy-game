using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointText;
    [SerializeField] private Unit unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        healthSystem.OnDamage += HealthSystem_OnDamage;
        healthSystem.OnHeal += HealthSystem_OnHeal;

        UpdateActionPointText();
        UpdateHealthBar();
    }

    private void UpdateActionPointText()
    {
        actionPointText.text = unit.GetActionPoints().ToString();
    }

    private void HealthSystem_OnDamage(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    private void HealthSystem_OnHeal(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    private void Unit_OnAnyActionPointsChanged(object sender, System.EventArgs e)
    {
        UpdateActionPointText();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealhNormalized();
    }
}
