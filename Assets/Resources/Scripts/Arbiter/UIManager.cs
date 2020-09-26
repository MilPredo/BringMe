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
            if (this.roundUI)
                this.roundUI.text = round.ToString();
        }

        public void SetRoundTimeUI(int roundTime) {
            if (this.roundTimeUI)
                this.roundTimeUI.text = this.calculateStringTime(roundTime);
        }

        public void SetTargetCountUI(int targetCount) {
            string strCount = targetCount < 10 ? $"0{ targetCount }" : targetCount.ToString();
            if (this.targetCountUI)
                this.targetCountUI.text = $"ITEMS LEFT: ${strCount}";
        }

        public void SetTargetItemUI(string targetItem) {
            if (this.targetItemUI)
                this.targetItemUI.text = $"TARGET ITEM: {targetItem}";
        }

        public void SetFreezeTimeUI(int freezeTime) {
            if (this.freezeTimeUI)
                this.freezeTimeUI.text = this.calculateStringTime(freezeTime);
        }

        public void SetFreezeRoundUI(int currentRound) {
            if (this.freezeRoundUI)
                this.freezeRoundUI.text = $"ROUND {currentRound}";
        }

        public void ShowRoundLayout() {
            if (this.roundLayout)
                this.roundLayout.SetActive(true);
        }

        public void ShowRoundUI() {
            if (this.roundUI && this.roundUI.text == "")
                this.SetRoundUI(1);
        }

        public void ShowRoundTimeUI() {
            if (this.roundTimeUI && this.roundTimeUI.text == "")
                this.SetRoundTimeUI(120);
        }

        public void ShowTargetCountUI() {
            if (this.targetCountUI && this.targetCountUI.text == "")
                this.SetTargetCountUI(3);
        }

        public void ShowTargetItemUI() {
            if (this.targetItemUI && this.targetItemUI.text == "")
                this.SetTargetItemUI("Excalibur");
        }

        public void ShowFreezeTimeLayout() {
            if (this.freezeTimeLayout)
                this.freezeTimeLayout.SetActive(true);
        }

        public void ShowFreezeTimeUI() {
            if (this.freezeTimeUI && this.freezeTimeUI.text == "")
                this.SetFreezeTimeUI(5);
        }

        public void HideRoundLayout() {
            if (this.roundLayout)
                this.roundLayout.SetActive(false);
        }

        public void HideRoundUI() {
            if (this.roundUI && this.roundUI.text != "")
                this.roundUI.text = "";
        }

        public void HideRoundTimeUI() {
            if (this.roundTimeUI && this.roundTimeUI.text != "")
                this.roundTimeUI.text = "";
        }

        public void HideTargetCountUI() {
            if (this.targetCountUI && this.targetCountUI.text != "")
                this.targetCountUI.text = "";
        }

        public void HideTargetItemUI() {
            if (this.targetItemUI && this.targetItemUI.text != "")
                this.targetItemUI.text = "";
        }

        public void HideFreezeTimeLayout() {
            if (this.freezeTimeLayout)
                this.freezeTimeLayout.SetActive(false);
        }

        public void HideFreezeTimeUI() {
            if (this.freezeTimeUI && this.freezeTimeUI.text != "")
                this.freezeTimeUI.text = "";
        }

        private string calculateStringTime(int time) {
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

