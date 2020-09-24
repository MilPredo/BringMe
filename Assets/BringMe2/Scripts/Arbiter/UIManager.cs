using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RummageBattle {
    public class UIManager : MonoBehaviour {

        [SerializeField] private TMPro.TextMeshProUGUI roundUI;
        [SerializeField] private TMPro.TextMeshProUGUI roundTimeUI;
        [SerializeField] private TMPro.TextMeshProUGUI freezeTimeUI;
        [SerializeField] private TMPro.TextMeshProUGUI targetCountUI;
        [SerializeField] private TMPro.TextMeshProUGUI targetItemUI;

        public void setRoundUI(int round) {
            if (this.roundUI) 
                this.roundUI.text = round.ToString();
        }

        public void setRoundTimeUI(int roundTime) {
            if (this.roundTimeUI)
                this.roundTimeUI.text = this.calculateStringTime(roundTime);
        }

        public void setFreezeTimeUI(int freezeTime) {
            if (this.freezeTimeUI)
                this.freezeTimeUI.text = this.calculateStringTime(freezeTime);
        }

        public void setTargetCountUI(int targetCount) {
            string strCount = targetCount < 10 ? $"0{ targetCount }" : targetCount.ToString();
            if (this.targetCountUI)
                this.targetCountUI.text = $"ITEMS LEFT: ${strCount}";
        }

        public void setTargetItemUI(string targetItem) {
            if (this.targetItemUI)
                this.targetItemUI.text = $"TARGET ITEM: {targetItem}";
        }

        public void showRoundUI() {
            if (this.roundUI && this.roundUI.text == "")
                this.setRoundUI(1);
        }

        public void showRoundTimeUI() {
            if (this.roundTimeUI && this.roundTimeUI.text == "")
                this.setRoundTimeUI(120);
        }

        public void showFreezeTimeUI() {
            if (this.freezeTimeUI && this.freezeTimeUI.text == "")
                this.setFreezeTimeUI(5);
        }

        public void showTargetCountUI() {
            if (this.targetCountUI && this.targetCountUI.text == "")
                this.setTargetCountUI(3);
        }

        public void showTargetItemUI() {
            if (this.targetItemUI && this.targetItemUI.text == "")
                this.setTargetItemUI("Excalibur");
        }

        public void hideRoundUI() {
            if (this.roundUI && this.roundUI.text != "")
                this.roundUI.text = "";
        }

        public void hideRoundTimeUI() {
            if (this.roundTimeUI && this.roundTimeUI.text != "")
                this.roundTimeUI.text = "";
        }

        public void hideFreezeTimeUI() {
            if (this.freezeTimeUI && this.freezeTimeUI.text != "")
                this.freezeTimeUI.text = "";
        }

        public void hideTargetCountUI() {
            if (this.targetCountUI && this.targetCountUI.text != "")
                this.targetCountUI.text = "";
        }

        public void hideTargetItemUI() {
            if (this.targetItemUI && this.targetItemUI.text != "")
                this.targetItemUI.text = "";
        }

        private string calculateStringTime(int time) {
            int mins = 0;
            int secs = time;

            if (secs > 60) {
                mins = (int) secs / 60;
                secs = (int) secs % 60;
            }

            // makes sure the numbers are always two digits
            string strSecs = secs < 10 ? $"0{ secs }" : secs.ToString();
            string strMins = mins < 10 ? $"0{ mins }" : mins.ToString();

            return $"{strMins}:{strSecs}";
        }
    }
}

