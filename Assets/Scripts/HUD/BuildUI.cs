using System.Collections;
using System.Collections.Generic;
using TD;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using static TD.MapEventsBus;

public class BuildUI : MonoBehaviour
{
    [SerializeField] private InputBus _InputBus;
    [SerializeField] private MapEventsBus _MapEvents;

    [SerializeField] private List<TowerProperties> _Towers;

    [SerializeField] private TowerPanel _TowerPanelPrefab;

    [SerializeField] private GameObject _PanelsAnchor;

    private RectTransform _PanelsAnchorTransform;

    public void Awake()
    {
        gameObject.SetActive(false);

        _MapEvents.HideBuilder.AddListener(SetInactive);
        _MapEvents.BuilderGridClick += ShowBuilderUI;

        _PanelsAnchorTransform = _PanelsAnchor.GetComponent<RectTransform>();

        foreach (var props in _Towers)
        {
            TowerPanel panel = Instantiate(_TowerPanelPrefab, _PanelsAnchor.transform);
            panel.ApplySetup(props);
        }
    }

    private void OnDestroy()
    {
        _MapEvents.HideBuilder.RemoveListener(SetInactive);
        _MapEvents.BuilderGridClick -= ShowBuilderUI;
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }

    public void ShowBuilderUI(object Sender, OnBuilderGridClickEventArgs args)
    {
        gameObject.SetActive(true);
        _PanelsAnchorTransform.position = Utilities.WorldToScreenPosition(args.position);
    }
}
