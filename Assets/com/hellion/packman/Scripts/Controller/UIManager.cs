using com.hellion.packman;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialogPrefab;
    [SerializeField] private Button _resumeBtn, _restartBtn;
    [SerializeField] TMPro.TMP_Text _resumeBtnText, _titleText;

    private static UIManager _instance;

    private void Awake()
    {
        _instance = this;
        _resumeBtn.onClick.AddListener(ResumeGame);
        _restartBtn.onClick.AddListener(RestartGame);

    }
    public static void ShowPauseMenu()
    {
        GameManager.Instance.PauseGame();
        _instance._dialogPrefab.SetActive(true);
        _instance._titleText.text = "Paused";
        _instance._resumeBtn.gameObject.SetActive(true);
    }

    public static void ShowGameOverMenu()
    {
        GameManager.Instance.PauseGame();
        _instance._dialogPrefab.SetActive(true);
        _instance._titleText.text = "Game Over";
        _instance._resumeBtn.gameObject.SetActive(false);
    }
    public static void ShowGameWonMenu()
    {
        GameManager.Instance.PauseGame();
        _instance._dialogPrefab.SetActive(true);
        _instance._titleText.text = "Game Won";
        _instance._resumeBtn.gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        _dialogPrefab.SetActive(false);
        GameManager.Instance.ResetGame();
    }

    private void ResumeGame()
    {
        _dialogPrefab.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
