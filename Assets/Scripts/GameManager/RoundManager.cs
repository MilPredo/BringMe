using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
public class RoundManager : MonoBehaviour {
    [SerializeField] private int maxRound = 3;
    [SerializeField] private float maxRoundTime = 60f;
    [SerializeField] private float maxPrepareTime = 15f;

    private int currentRound = 0;
    private float currentRoundTime = 0f;
    private float currentPrepareTime = 0f;
    private bool isPlaying = false;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject prepareRoundMenu;
    [SerializeField] private GameObject roundEndMenu;
    [SerializeField] private GameObject endGameResultMenu;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI prepareCountdownText;
    [SerializeField] private TextMeshProUGUI prepareMessageText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private ArbiterManager arbiterManager;

    // private void Start() {
    //     // wait for 15 seconds
    //     arbiterManager = GameObject.Find("Arbiter").GetComponent<ArbiterManager>();
    //     StartCoroutine(PrepareRound(maxPrepareTime));
    // }

    private void Update() {
        if (player == null) {
            player = ClientScene.localPlayer.gameObject;
            return;
        }
        this.gameManager.currentRoundTime -= Time.deltaTime;
        SetTimerTime(this.timerText, this.gameManager.currentRoundTime);
        if (this.gameManager.currentRoundTime < 0 && this.gameManager.isPlaying) {
            EndRound();

            this.gameManager.currentPrepareTime -= Time.deltaTime;

            SetTimerTime(this.prepareCountdownText, this.gameManager.currentPrepareTime);

            if (this.gameManager.currentPrepareTime < 0 && this.gameManager.isPlaying) {
                StartRound();
            }
        }
    }


    private void StartRound() {
        this.ui.SetActive(true);
        this.prepareRoundMenu.SetActive(false);
        this.gameManager.currentRoundTime = this.gameManager.MaxRoundTime;
        this.gameManager.currentRound += 1;
        this.arbiterManager.ItemsLeftToBring = this.gameManager.MaxRound - (this.gameManager.currentRound - 1);
    }


    private void EndRound() {
        // deactivate game
        // player.SetActive(false);
        this.ui.SetActive(false);
        if (this.gameManager.currentRound >= this.gameManager.MaxRound) {
            Debug.Log("RoundManager.EndRound(): Showing Game Result");
            this.endGameResultMenu.SetActive(true);

            this.gameManager.isPlaying = false;
        } else {
            Debug.Log($"RoundManager.EndRound(): End of Round { this.gameManager.currentRound }");

            if (this.gameManager.currentPrepareTime <= 0) {
                this.gameManager.currentPrepareTime = this.gameManager.MaxPrepareTime;
            }

            this.prepareMessageText.text = $"RoundManager.EndRound(): End of Round { this.gameManager.currentRound }";
            this.prepareRoundMenu.SetActive(true);
        }
    }

    public void StopRound() {
        Debug.Log("RoundManager.StopRound()");
        this.gameManager.currentRoundTime = 0f;
    }

    private IEnumerator PrepareRound() {
        this.prepareMessageText.text = "STARTING IN";
        this.prepareRoundMenu.SetActive(true);
        // player.SetActive(false);
        this.ui.SetActive(false);
        Debug.Log("RoundManager.PrepareRound()");
        while (this.gameManager.currentPrepareTime > 0) {
            // update countdown timer text
            SetTimerTime(this.prepareCountdownText, this.gameManager.currentPrepareTime);
            yield return new WaitForSeconds(1f);
            this.gameManager.currentPrepareTime -= 1f;
        }
        this.prepareRoundMenu.SetActive(false);
        this.gameManager.isPlaying = true;
        StartRound();
    }

    private void SetTimerTime(TMPro.TextMeshProUGUI timer, float time) {
        float roundedTime = Mathf.Round(time);
        timer.text = roundedTime.ToString();
    }
}
