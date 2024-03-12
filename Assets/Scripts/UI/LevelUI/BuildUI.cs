using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TD
{
    public class BuildUI : MonoBehaviour
    {
        [SerializeField] private List<TowerProperties> _Towers;

        [SerializeField] private TowerPanel _TowerPanelPrefab;

        [SerializeField] private GameObject _PanelsAnchor;

        [SerializeField] private Image _TowerRadiusSprite;

        private RectTransform _PanelsAnchorTransform;

        private void Awake()
        {
            _PanelsAnchorTransform = _PanelsAnchor.GetComponent<RectTransform>();

            foreach (var props in _Towers)
            {
                TowerPanel panel = Instantiate(_TowerPanelPrefab, _PanelsAnchor.transform);
                panel.ApplySetup(props);
            }
        }

        private void Start()
        {
            _TowerRadiusSprite.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        public void ShowBuilderUI(Component Sender, object data)
        {
            if (data is not Vector2)
            {
                return;
            }

            gameObject.SetActive(true);
            _PanelsAnchorTransform.position = Utilities.WorldToScreenPosition((Vector2)data);
        }

        public void ShowTowerRadius(Component caller, object data)
        {
            if (caller?.gameObject == null || caller is not TowerPanel)
            {
                return;
            }

            TowerProperties props = ((TowerPanel)caller).GetAssignedTowerProperties();

            _TowerRadiusSprite.rectTransform.sizeDelta = new Vector2(props.Range * UISettings.PPU, props.Range * UISettings.PPU);
            _TowerRadiusSprite.rectTransform.position = _PanelsAnchor.GetComponent<RectTransform>().position;
            _TowerRadiusSprite.gameObject.SetActive(true);
        }
    }
}
