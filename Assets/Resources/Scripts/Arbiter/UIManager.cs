using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RummageBattle {
    public class UIManager : MonoBehaviour {

        [Header("Round UI")]
        [SerializeField] private GameObject roundLayout;
        [SerializeField] private TextMeshProUGUI roundUI;
        [SerializeField] private TextMeshProUGUI roundTimeUI;
        [SerializeField] private TextMeshProUGUI targetCountUI;
        [SerializeField] private TextMeshProUGUI targetItemUI;

        [Header("Preparation UI")]
        [SerializeField] private GameObject freezeTimeLayout;
        [SerializeField] private TextMeshProUGUI freezeRoundUI;
        [SerializeField] private TextMeshProUGUI freezeTimeUI;


        public void SetRoundUI(int round) {
            if (roundUI)
                roundUI.text = round.ToString();
        }

        public void SetRoundTimeUI(int roundTime) {
            if (roundTimeUI)
                roundTimeUI.text = CalculateStringTime(roundTime);
        }

        public void SetTargetCountUI(int targetCount) {
            string strCount = targetCount < 10 ? $"0{ targetCount }" : targetCount.ToString();
            if (targetCountUI)
                targetCountUI.text = $"ITEMS LEFT: {strCount}";
        }

        public void SetTargetItemUI(string targetItem) {
            if (targetItemUI)
                targetItemUI.text = $"TARGET ITEM: {targetItem}";
        }

        public void SetFreezeTimeUI(int freezeTime) {
            if (freezeTimeUI)
                freezeTimeUI.text = CalculateStringTime(freezeTime);
        }

        public void SetFreezeRoundUI(int currentRound) {
            if (freezeRoundUI)
                freezeRoundUI.text = $"ROUND {currentRound}";
        }

        public void ShowRoundLayout() {
            if (roundLayout)
                roundLayout.SetActive(true);
        }

        public void ShowRoundUI() {
            if (roundUI && roundUI.text == "")
                SetRoundUI(1);
        }

        public void ShowRoundTimeUI() {
            if (roundTimeUI && roundTimeUI.text == "")
                SetRoundTimeUI(120);
        }

        public void ShowTargetCountUI() {
            if (targetCountUI && targetCountUI.text == "")
                SetTargetCountUI(3);
        }

        public void ShowTargetItemUI() {
            if (targetItemUI && targetItemUI.text == "")
                SetTargetItemUI("Excalibur");
        }

        public void ShowFreezeTimeLayout() {
            if (freezeTimeLayout)
                freezeTimeLayout.SetActive(true);
        }

        public void ShowFreezeTimeUI() {
            if (freezeTimeUI && freezeTimeUI.text == "")
                SetFreezeTimeUI(5);
        }

        public void HideRoundLayout() {
            if (roundLayout)
                roundLayout.SetActive(false);
        }

        public void HideRoundUI() {
            if (roundUI && roundUI.text != "")
                roundUI.text = "";
        }

        public void HideRoundTimeUI() {
            if (roundTimeUI && roundTimeUI.text != "")
                roundTimeUI.text = "";
        }

        public void HideTargetCountUI() {
            if (targetCountUI && targetCountUI.text != "")
                targetCountUI.text = "";
        }

        public void HideTargetItemUI() {
            if (targetItemUI && targetItemUI.text != "")
                targetItemUI.text = "";
        }

        public void HideFreezeTimeLayout() {
            if (freezeTimeLayout)
                freezeTimeLayout.SetActive(false);
        }

        public void HideFreezeTimeUI() {
            if (freezeTimeUI && freezeTimeUI.text != "")
                freezeTimeUI.text = "";
        }

        private string CalculateStringTime(int time) {
            int mins = 0;
            int secs = time;

            if (secs > 60) {
                mins = (int)secs / 60;
                secs = (int)secs % 60;
            }

            // makes sure the numbers are always two digits
            string strSecs = secs < 10 ? $"0{ secs }" : secs.ToString();
            string strMins = mins < 10 ? $"0{ mins }" : mins.ToString();

            return $"{strMins}:{strSecs}";
        }
    }
}

