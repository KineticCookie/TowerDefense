using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Fields
    private GameObject uiMoneyText;
    private GameObject uiHealthText;
    private GameObject uiWaveText;

    private CastleController castleController;
    private GameController gameController;
    #endregion

    #region Behaviour
    void Awake()
    {
        var castle = GameObject.Find(Constants.GameObjects.Castle);
        castleController = castle.GetComponent<CastleController>();
        gameController = Camera.main.GetComponent<GameController>();

        uiMoneyText = GameObject.Find(Constants.GameObjects.MoneyText);
        uiHealthText = GameObject.Find(Constants.GameObjects.HealthText);
        uiWaveText = GameObject.Find(Constants.GameObjects.WaveText);
    }

    void Update()
    {
        uiHealthText.GetComponent<Text>().text = string.Format(Constants.TextsUI.HealthText_1, castleController.healthCurrent);
        uiMoneyText.GetComponent<Text>().text = string.Format(Constants.TextsUI.MoneyText_1, gameController.moneyCurrent);
        uiWaveText.GetComponent<Text>().text = string.Format(Constants.TextsUI.WaveText_2, gameController.waveCurrent, gameController.waveCount);
    }
    #endregion
}
