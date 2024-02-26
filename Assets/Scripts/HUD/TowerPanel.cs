using TD;
using UnityEngine;
using UnityEngine.UI;

public class TowerPanel : MonoBehaviour
{
    [SerializeField] private PlayerEventsBus _PlayerEventBus;
    [SerializeField] private UIEventsBus _UIEventsBus;
    [SerializeField] private Button _btn;
    [SerializeField] private TowerProperties _TowerProps;

    [SerializeField] private Text _Cost;
    [SerializeField] private Image _TowerWeaponIcon;

    public void ApplySetup(TowerProperties props)
    {
        _TowerProps = props;
        _Cost.text = _TowerProps.Cost.ToString();
        _TowerWeaponIcon.sprite = _TowerProps.Weapon.VisualModel;

        _PlayerEventBus.GoldChanged.AddListener(UpdateButtonAvailability);
    }

    private void OnEnable()
    {
        UpdateButtonAvailability();
    }

    private void UpdateButtonAvailability()
    {
        _btn.interactable = Player.Gold >= _TowerProps.Cost;
    }

    public void OnBtnClick()
    {
        // TODO position should be saved in map. Here only tower properties should be transfered
        _UIEventsBus.OnRequestBuildTower(Utilities.GetMouse2DWorldPosition(), _TowerProps);
    }
}
