using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameStartScreen;
    [SerializeField] private GameObject gameClearScreen;
    [SerializeField] private Button startButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button retryButton2;
    [SerializeField] private List<Unit> enemyUnits;

    private void Start()
    {
        enemyUnits = new List<Unit>();

        gameClearScreen.SetActive(false);

        startButton.onClick.AddListener(() =>
        {
            HideStartScreen();
        });

        retryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });

        retryButton2.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });

        Unit[] allUnits = FindObjectsOfType<Unit>(true);
        foreach (Unit unit in allUnits)
        {
            if (unit.IsEnemy())
            {
                enemyUnits.Add(unit);
                Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
            }
        }
    }

    private void Unit_OnAnyUnitDead(object sender, System.EventArgs e)
    {
        Unit deadUnit = sender as Unit;

        if (deadUnit != null && deadUnit.IsEnemy())
        {
            enemyUnits.Remove(deadUnit);

            Debug.Log("Enemy died. Current number of enemies: " + enemyUnits.Count);

            if (enemyUnits.Count == 0)
            {
                ShowGameClearScreen();
            }
        }
    }

    private void ShowGameClearScreen()
    {
        gameClearScreen.SetActive(true);
    }

    private void HideStartScreen()
    {
        gameStartScreen.SetActive(false);
    }
}
