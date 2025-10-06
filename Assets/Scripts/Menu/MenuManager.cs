using GameCore.Save;
using GameCore.ScenesLoader;
using Menu.Shop;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _difficultyButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private GameObject _shopWindow;
        [SerializeField] private TMP_Text _difficultyText;

        private SceneLoader _sceneLoader;
        private MenuUIUpdater _menuUIUpdater;
        private GameShop _gameShop;
        private GameDifficulty _gameDifficulty;

        [Inject]
        private void Construct(SceneLoader sceneLoader, MenuUIUpdater menuUIUpdater, GameShop gameShop,  GameDifficulty gameDifficulty)
        {
            _sceneLoader = sceneLoader;
            _menuUIUpdater = menuUIUpdater;
            _gameShop = gameShop;
            _gameDifficulty = gameDifficulty;
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
            _shopButton.onClick.AddListener(OpenShopWindow);
            _closeButton.onClick.AddListener(Application.Quit);
            _difficultyButton.onClick.AddListener(ChooseDifficulty);
            _difficultyText.text = _gameDifficulty.Difficulty.ToString();
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
            _shopButton.onClick.RemoveListener(OpenShopWindow);
            _closeButton.onClick.RemoveListener(Application.Quit);
            _difficultyButton.onClick.RemoveListener(ChooseDifficulty);

        }

        private void OpenShopWindow()
        {
            _shopWindow.SetActive(true);
            _menuUIUpdater.UpdateUI();
            _gameShop.ShowPrice();
            _gameShop.CheckButtons();
        }

        private void StartGame()
        {
            _sceneLoader.Game();
        }

        private void ChooseDifficulty()
        {
            if (_gameDifficulty.Difficulty == DifficultyEnum.Normal)
            {
                _gameDifficulty.SetDifficulty(DifficultyEnum.Hard);
                _difficultyText.text = "HARD";
            }
            else if (_gameDifficulty.Difficulty == DifficultyEnum.Hard)
            {
                _gameDifficulty.SetDifficulty(DifficultyEnum.Normal);
                _difficultyText.text = "NORMAL";
            }
        }
    }
}