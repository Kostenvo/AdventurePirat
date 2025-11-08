using Definitions;
using GameData;
using Subscribe;
using UnityEngine;

namespace UI.Stats
{
    public class StatsWindow : AnimatedWindow
    {
        [SerializeField] private PriceItem _priceItem;
        [SerializeField] private GameObject _priceBox;
        [SerializeField] private UnityEngine.UI.Button _updateButton;
        [SerializeField] private Transform _statsContainer;
        [SerializeField] private StatItem _statItem;


        private ComposideDisposible _trash = new ComposideDisposible();
        private ItemController<StatItem, StatDef> _statController;
        private GameSession _session;

        private float _timeScale;

        protected override void Start()
        {
            _session = FindAnyObjectByType<GameSession>();
            _timeScale = Time.timeScale;
            Time.timeScale = 0;
            _statController = new ItemController<StatItem, StatDef>(_statItem, _statsContainer);
            _trash.Retain(_session.StatsModel.Subscribe(UpdateWindow));
            _trash.Retain(_session.StatsModel.SelectedStats.Subscribe((_, _) => UpdateWindow()));
            UpdateWindow();
            base.Start();
        }

        private void UpdateWindow()
        {
            _statController.Rebuild(DefsFacade.Instance.StatsRepository.Items);
            var nextLevel = _session.StatsModel.GetNextLevel(_session.StatsModel.SelectedStats.Value);
            if (nextLevel.Price == null)
            {
                _updateButton.gameObject.SetActive(false);
                _priceBox.SetActive(false);
            }
            else
            {
                _updateButton.gameObject.SetActive(true);
                _updateButton.interactable = _session.PlayerData.Inventory.IsEnoughItem(nextLevel.Price);
                _priceBox.SetActive(true);
                _priceItem.SetPrice(nextLevel.Price);
            }
        }

        public void OnClickUpdateButton()
        {
            _session.StatsModel.UpgradeStats(_session.StatsModel.SelectedStats.Value);
        }


        protected override void OnClosedAnimation()
        {
            Time.timeScale = _timeScale;
            base.OnClosedAnimation();
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}