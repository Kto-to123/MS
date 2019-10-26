﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Прокачка скилов происходит за счет использования темной и светлой энергии. 
// Некоторые способности можно прокачивать за счет любой энергии.
// Некоторые способности можно прокачивать только имея отношение к темной или светлой магии.
// Активные способности будут распологаться в дереве прокачки, для открытия более высоких, нужно исследовать все нижние.
// Дерево прокачки так-же будет делится на темную ветвь, светлую ветвь и нейтральную ветвь.

public class SkillProgress : MonoBehaviour
{
    public static SkillProgress instance;

    // Гафика
    [SerializeField] Text lightPointUIText;
    [SerializeField] Text darkPointUIText;
    [SerializeField] Text moveSpeedliteUIText;
    [SerializeField] Text moveSpeedDarkUIText;
    [SerializeField] Text moveSpeedUIText;
    [SerializeField] Slider generalIndicatorSlider;
    [SerializeField] Text generalIndicatorUIText;
    [SerializeField] Text steelDamageUIText;
    [SerializeField] Text SteelSpeedUIText;

    public int lightPoint; // Количество балов светлой магии
    public int darkPoint; // Количество балов темной магии
    public float generalIndicator = 0f; // Индикатор принадлежности к стороне магии (Отрицательный - темная, положительный - светлая)

    public float moveSpeedStart = 5f;
    public float moveSpeed = 0f;
    public float moveSpeedLite = 0f;
    public float moveSpeedDark = 0f;

    public double steelDamage = 1;
    public double steelSpeed = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    // Обновление информации в интерфейсе
    public void UpdateUI() 
    {
        lightPointUIText.text = "Светлая магия " + lightPoint.ToString();
        darkPointUIText.text = "Темная магия " + darkPoint.ToString();
        moveSpeedliteUIText.text = moveSpeedLite.ToString();
        moveSpeedDarkUIText.text = moveSpeedDark.ToString();
        moveSpeedUIText.text = "Общая скорость: " + MoveSpeedConversion().ToString();
        generalIndicatorSlider.value = generalIndicator;
        generalIndicatorUIText.text = generalIndicator.ToString();
        steelDamageUIText.text = steelDamage.ToString();
        SteelSpeedUIText.text = steelSpeed.ToString();
    }

    // Просчет скорости перемещения с учетом прокачки
    float MoveSpeedConversion()
    {
        moveSpeed = moveSpeedStart + moveSpeedLite + moveSpeedDark;
        return moveSpeed;
    }

    // Установка скорости перемещения
    void MoveSpeedInstans()
    {
        PlayerControllerScript.instance.SetMoveSpeed(MoveSpeedConversion());
        UpdateUI();
    }

    #region Функции кнопок в графическом интерфейсе

    public void MoveSpeedUpWhite()
    {
        if (lightPoint > 0)
        {
            lightPoint--;
            moveSpeedLite = moveSpeedLite + 1f;
            generalIndicator++;
            MoveSpeedInstans();
        }
    }

    public void MoveSpeedDownWhite()
    {
        if (moveSpeedLite > 0)
        {
            lightPoint++;
            moveSpeedLite = moveSpeedLite - 1f;
            generalIndicator--;
            MoveSpeedInstans();
        }
    }

    public void MoveSpeedUpDark()
    {
        if (darkPoint > 0)
        {
            darkPoint--;
            moveSpeedDark = moveSpeedDark + 1f;
            generalIndicator--;
            MoveSpeedInstans();
        }
    }

    public void MoveSpeedDownDark()
    {
        if (moveSpeedDark > 0)
        {
            darkPoint++;
            moveSpeedDark = moveSpeedDark - 1f;
            generalIndicator++;
            MoveSpeedInstans();
        }
    }

    public void SteelDamageUp()
    {
        if (darkPoint > 0)
        {
            darkPoint--;
            steelDamage = steelDamage + 0.1;
            generalIndicator--;
            UpdateUI();
        }
    }

    public void SteelDamageDown()
    {
        if (steelDamage > 1)
        {
            darkPoint++;
            steelDamage = steelDamage - 0.1;
            generalIndicator++;
            UpdateUI();
        }
    }

    public void SteelSpeedUp()
    {
        if (darkPoint > 0)
        {
            darkPoint--;
            steelSpeed = steelSpeed + 0.1;
            generalIndicator--;
            UpdateUI();
        }
    }

    public void SteelSpeedDown()
    {
        if (steelSpeed > 1)
        {
            darkPoint++;
            steelSpeed = steelSpeed - 0.1;
            generalIndicator++;
            UpdateUI();
        }
    }

    #endregion
}
