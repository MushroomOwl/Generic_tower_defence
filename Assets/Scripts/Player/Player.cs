using UnityEngine;

namespace TD
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameEvent _OnPlayerGoldChanged;
        [SerializeField] private GameEvent _OnPlayerLivesChanged;

        [SerializeField] private int _Gold;
        public int Gold => _Gold;

        [SerializeField] private int _Lives;
        public int Lives => _Lives;

        private void Start()
        {
            _OnPlayerGoldChanged?.Raise(this, Gold);
            _OnPlayerLivesChanged?.Raise(this, Lives);
        }

        public void ChangeGold(int amount)
        {
            _Gold += amount;
            _OnPlayerGoldChanged?.Raise(this, Gold);
        }

        public void ChangeLives(int amount)
        {
            _Lives += amount;
            _OnPlayerLivesChanged?.Raise(this, Lives);
        }

        public void TakeHit(Component caller, object data)
        {
            ChangeLives(-1);
        }

        public void PayForTower(Component caller, object data)
        {
            if (data is not TowerProperties)
            {
                return;
            }
            TowerProperties props = (TowerProperties)data;
            ChangeGold(-props.Cost);
        }

        public void GetRewardForEnemy(Component caller, object data)
        {
            if (data is not EnemyProps)
            {
                return;
            }
            EnemyProps props = (EnemyProps)data;
            ChangeGold(props.Reward);
        }
    }
}

