using System.Collections;
using System.Collections.Generic;
using TD;
using UnityEngine;
using UnityEngine.UI;

namespace TD
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private PlayerEventsBus _PlayerEvents;
        [SerializeField] private Text _CoinsDisplay;
        [SerializeField] private List<GameObject> _Lives;


        void Awake()
        {
            _PlayerEvents.LivesChanged.AddListener(UpdateLives);
            _PlayerEvents.GoldChanged.AddListener(UpdateCoins);
        }

        private void UpdateLives()
        {
            // TODO Improve optimisation
            _Lives.ForEach((obj) => obj.SetActive(false));
            if (Player.NumLives < 0)
            {
                return;
            }
            for (int i = 0; i < Player.NumLives; i++)
            {
                _Lives[i].SetActive(true);
            }
        }

        private void UpdateCoins()
        {
            _CoinsDisplay.text = Player.Gold.ToString();
        }
    }
}