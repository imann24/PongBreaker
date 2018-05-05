/*
 * Author: Isaiah Mann
 * Description:
 */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PowerUpEditorTool : MonoBehaviour
{
    GameplayController gameplay;
    PowerUpController powerUpController;

    Queue<PaddleController> players;
    Queue<PowerUpBehaviour> powerUps;

    [SerializeField]
    PaddleController selectedPlayer;

    [SerializeField]
    PowerUpBehaviour selectedPowerUp;

    [SerializeField]
    KeyCode switchSelectedPlayerButton = KeyCode.LeftShift;

    [SerializeField]
    KeyCode switchSelectedPowerUpButton = KeyCode.Tab;

    [SerializeField]
    KeyCode activatePowerUpButton = KeyCode.LeftControl;

    IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        gameplay = GameplayController.Instance;
        powerUpController = PowerUpController.Instance;
        players = new Queue<PaddleController>(gameplay.GetRegisteredPlayers);
        powerUps = new Queue<PowerUpBehaviour>(powerUpController.GetPowerUpTemplates);
        switchSelectedPlayer();
        switchSelectedPowerUp();
    }

#if UNITY_EDITOR

    void Update()
    {
        if (Input.GetKeyDown(switchSelectedPlayerButton))
        {
            switchSelectedPlayer();
        }
        if (Input.GetKeyDown(switchSelectedPowerUpButton))
        {
            switchSelectedPowerUp();
        }
        if (Input.GetKeyDown(activatePowerUpButton))
        {
            activatePowerUp();
        }
    }

#endif

    PaddleController switchSelectedPlayer()
    {
        PaddleController newSelectedPlayer = players.Dequeue();
        players.Enqueue(newSelectedPlayer);
        selectedPlayer = newSelectedPlayer;
        return newSelectedPlayer;
    }

    PowerUpBehaviour switchSelectedPowerUp()
    {
        PowerUpBehaviour newSelectedPowerUp = powerUps.Dequeue();
        powerUps.Enqueue(newSelectedPowerUp);
        selectedPowerUp = newSelectedPowerUp;
        return newSelectedPowerUp;
    }

    void activatePowerUp()
    {
        powerUpController.ActivatePowerUp(selectedPowerUp, selectedPlayer);
    }
}
