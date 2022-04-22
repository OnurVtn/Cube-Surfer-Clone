using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance ?? (instance = FindObjectOfType<GameManager>());

    private bool isGameStart = false;
    private bool isGameFail = false;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform playerVisualTransform;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        IsGameFail();
    }

    private void IsGameFail()
    {
        var currentCubeNumber = playerVisualTransform.childCount - 2;

        if (currentCubeNumber <= 0 && isGameFail == false)
        {
            OnGameFail();
        }
    }

    public bool IsGameStart()
    {
        return isGameStart;
    }

    public void OnGameStart()
    {
        isGameStart = true;
        UIManager.Instance.DeactivateInGameStartUI();
    }

    private void OnGameFail()
    {
        isGameFail = true;
        UIManager.Instance.ActivateFailPanel();
        playerController.enabled = false;
    }

    public void OnGameFinish()
    {
        UIManager.Instance.ActivateLevelCompletedPanel();
        playerController.enabled = false;
    }
}
