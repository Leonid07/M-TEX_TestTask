using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
// Класс UIInstaller наследуется от MonoInstaller и используется для установки привязок в контейнере Zenject.
public class UIInstaller : MonoInstaller
{
    // Объявление публичных переменных для различных элементов пользовательского интерфейса.
    public GameObject panelPause;// Панель паузы
    public TMP_Text textCurrentAccount;// Текстовое поле для текущего счета
    public TMP_Text textMaxAccount;// Текстовое поле для максимального счета
    public TMP_Text textRoundCounter;// Текстовое поле для счетчика раундов

    public Button buttonContinue;// Кнопка продолжить
    public Button buttonPause;// Кнопка паузы
    public Button buttonRestartLevel;// Кнопка перезагрузить уровень

    // Метод InstallBindings вызывается для установки привязок в контейнере Zenject.
    public override void InstallBindings()
    {
        // Привязка объектов к их идентификаторам в контейнере Zenject.
        Container.Bind<GameObject>().WithId("PanelPause").FromInstance(panelPause).AsTransient();

        Container.Bind<TMP_Text>().WithId("TextCurrentAccount").FromInstance(textCurrentAccount).AsTransient();
        Container.Bind<TMP_Text>().WithId("TextMaxAccount").FromInstance(textMaxAccount).AsTransient();
        Container.Bind<TMP_Text>().WithId("TextRoundCounter").FromInstance(textRoundCounter).AsTransient();

        Container.Bind<Button>().WithId("ButtonContinue").FromInstance(buttonContinue).AsTransient();
        Container.Bind<Button>().WithId("ButtonPause").FromInstance(buttonPause).AsTransient();
        Container.Bind<Button>().WithId("ButtonRestartLevel").FromInstance(buttonRestartLevel).AsTransient();
    }
}