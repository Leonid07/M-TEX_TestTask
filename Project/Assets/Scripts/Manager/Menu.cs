using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using Zenject;

public class Menu : MonoBehaviour
{
    [Inject] Player player;
    [Inject (Id = "PanelPause")] GameObject panelPause;// панель паузы

    [Inject(Id = "TextCurrentAccount")] TMP_Text textCurrentAccount;// текст для отображения количества очков
    [Inject(Id = "TextMaxAccount")] TMP_Text textMaxAccount;// текст который показывает максимально набранные очки на период одной сесии

    [Inject(Id = "ButtonPause")] Button buttonPause;// кнопка паузы
    [Inject(Id = "ButtonContinue")] Button buttonContinue;// кнопка продолжения игры
    [Inject(Id = "ButtonRestartLevel")] Button buttonRestartLevel;// кнопка для перезагрузки уровня

    int countPoints;// количество очко 
    static int maxCountPoints;// статическая переменная для сохранения максимального счёта в течении одной сесии

    void Start()
    {
        ResumeScene();
        buttonPause.OnClickAsObservable().Subscribe(_ => PauseScene()).AddTo(this);
        buttonContinue.OnClickAsObservable().Subscribe(_ => ResumeScene()).AddTo(this);
        buttonRestartLevel.OnClickAsObservable().Subscribe(_ => RestartLevel()).AddTo(this);
        textMaxAccount.text = $"Рекорд {maxCountPoints}";
    }

    public void IsPlayerDestroy()
    {
        player.GameOver();
        Observable.Timer(System.TimeSpan.FromSeconds(2)).Subscribe(_ => Over()).AddTo(this);
    }

    public void Over()
    {
        PauseScene();
        buttonContinue.gameObject.SetActive(false);
    }

    public void AccountChange(int countPoints)
    {
        this.countPoints += countPoints;
        textCurrentAccount.text = $"Текущий счёт {this.countPoints}";
        if (this.countPoints >= maxCountPoints)
        {
            maxCountPoints = this.countPoints;
            textMaxAccount.text = $"Рекорд {maxCountPoints}";
        }
    }

    void PauseScene()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
    }
    void ResumeScene()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }

    void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
