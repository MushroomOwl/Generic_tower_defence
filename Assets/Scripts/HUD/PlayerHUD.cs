using System.Collections;
using System.Collections.Generic;
using TD;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TD
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private Text _CoinsDisplay;
        [SerializeField] private List<GameObject> _Lives;
        [SerializeField] private Text _Timer;
        [SerializeField] private GameObject _HUD;

        private void Start()
        {
            _HUD.SetActive(true);
        }

        public void UpdateLives(Component caller, object data)
        {
            if (data is not int)
            {
                return;
            }

            int lives = (int)data;

            _Lives.ForEach((obj) => obj.SetActive(false));

            if (lives < 0)
            {
                return;
            }

            for (int i = 0; i < lives; i++)
            {
                _Lives[i].SetActive(true);
            }
        }

        public void UpdateCoins(Component caller, object data)
        {
            if (data is not int)
            {
                return;
            }

            int coins = (int)data;

            _CoinsDisplay.text = coins.ToString();
        }

        public void UpdateTimer(Component caller, object data)
        {
            if (data is not float)
            {
                return;
            }

            float time = (float)data;

            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);

            string secondsStr = seconds.ToString();
            if (secondsStr.Length < 2)
            {
                secondsStr = "0" + secondsStr;
            }

            _Timer.text = minutes + ":" + secondsStr;
        }
    }
}