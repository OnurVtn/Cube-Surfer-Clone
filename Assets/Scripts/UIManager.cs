using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance ?? (instance = FindObjectOfType<UIManager>());

    [SerializeField] private Transform kidModelTransform, stairFinishTransform;
    [SerializeField] private Slider slider;
    private float totalDistance;

    [SerializeField] private TextMeshProUGUI diamondText, failText, levelCompletedText;
    [SerializeField] private RectTransform retryButtonRectTransform, nextButtonRectTransform;
    private int diamondNumber;

    [SerializeField] private GameObject inGameStartUI, failPanel, levelCompletedPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        totalDistance = stairFinishTransform.position.z - kidModelTransform.position.z;
    }

    void Update()
    {
        CheckProgressBar();
    }

    public void CheckProgressBar()
    {
        if (GameManager.Instance.IsGameStart())
        {
            var kidModelCurrentPosition = kidModelTransform.position.z;
            var currentDistance = totalDistance - kidModelCurrentPosition;
            var newSliderValue = (totalDistance - currentDistance) / totalDistance;
            slider.value = newSliderValue;
        }
    }

    public void UpdateDiamond()
    {
        diamondNumber++;
        diamondText.text = diamondNumber.ToString();
    }

    public void DeactivateInGameStartUI()
    {
        inGameStartUI.SetActive(false);
    }

    public void ActivateFailPanel()
    {
        failPanel.SetActive(true);

        failText.rectTransform.DOLocalMoveY(failText.rectTransform.localPosition.y - 1494f, 1.5f)
            .SetDelay(0.2f)
            .SetEase(Ease.OutBack);

        retryButtonRectTransform.DOLocalMoveY(retryButtonRectTransform.localPosition.y + 978f, 1.5f)
            .SetDelay(0.2f)
            .SetEase(Ease.OutBack);
    }

    public void ActivateLevelCompletedPanel()
    {
        levelCompletedPanel.SetActive(true);

        levelCompletedText.rectTransform.DOLocalMoveY(levelCompletedText.rectTransform.localPosition.y - 1494f, 1.5f)
            .SetDelay(0.2f)
            .SetEase(Ease.OutBack);

        nextButtonRectTransform.DOLocalMoveY(nextButtonRectTransform.localPosition.y + 978f, 1.5f)
            .SetDelay(0.2f)
            .SetEase(Ease.OutBack);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(0);
    }
}
