using TD;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TowerProperties _TowerProps;

    [SerializeField] private GameEvent _OnTowerBuildRequest;
    [SerializeField] private GameEvent _OnTowerPanelHoverOn;
    [SerializeField] private GameEvent _OnTowerPanelHoverOff;

    [SerializeField] private Button _btn;

    [SerializeField] private Text _Cost;
    [SerializeField] private Image _TowerWeaponIcon;

    public void ApplySetup(TowerProperties props)
    {
        _TowerProps = props;
        _Cost.text = _TowerProps.Cost.ToString();
        _TowerWeaponIcon.sprite = _TowerProps.Weapon.VisualModel;
    }

    public void UpdateButtonAvailability(Component caller, object data)
    {
        if (data is not int)
        {
            return;
        }
        _btn.interactable = (int)data >= _TowerProps.Cost;
    }

    public void OnBtnClick()
    {
        _OnTowerBuildRequest?.Raise(this, (Utilities.GetMouse2DWorldPosition(), _TowerProps));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _OnTowerPanelHoverOn?.Raise(this, null);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _OnTowerPanelHoverOff?.Raise(this, null);
    }

    public TowerProperties GetAssignedTowerProperties()
    {
        return _TowerProps;
    }
}
