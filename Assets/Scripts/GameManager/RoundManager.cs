using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private TMPro.TextMeshProUGUI prepareCountdownText;
    private TMPro.TextMeshProUGUI prepareMessageText;
    private TMPro.TextMeshProUGUI timerText;

    private void Start() {
        // wait for 15 seconds
        prepareCountdownText = FindTextFromFrame(prepareRoundMenu, "Countdown");
        prepareMessageText = FindTextFromFrame(prepareRoundMenu, "Message");
        timerText = FindTextFromFrame(ui, "Timer");
        StartCoroutine(PrepareRound(maxPrepareTime));
    }

    private void Update() {
        currentRoundTime -= Time.deltaTime;
        SetTimerTime(timerText, currentRoundTime);
        if ( currentRoundTime < 0 && (isPlaying) ) {
            EndRound();
            currentPrepareTime -= Time.deltaTime;
            SetTimerTime(prepareCountdownText, currentPrepareTime);
            if ( currentPrepareTime < 0 && isPlaying ) {
                StartRound();
                Debug.Log($"Start Round { currentRound }");
            }
        }
    }

    private void StartRound() {
        player.SetActive(true);
        ui.SetActive(true);
        prepareRoundMenu.SetActive(false);
        player.transform.position = new Vector3(0f, 0.5f, 0f);
        currentRoundTime = maxRoundTime;
        currentRound += 1;
    }

    private void EndRound() {
        // deactivate game
        player.SetActive(false);
        ui.SetActive(false);
        if ( currentRound >= maxRound ) {
            Debug.Log("Showing Game Result");
            endGameResultMenu.SetActive(true);
            isPlaying = false;
        } else {
            Debug.Log($"End of Round { currentRound }");
            if ( currentPrepareTime <= 0 ) {
                currentPrepareTime = maxPrepareTime;
            }
            prepareMessageText.text = $"End of Round { currentRound }";
            prepareRoundMenu.SetActive(true);
        }
    }

    public void StopRound() {
        this.currentRoundTime = 0f;
    }

    private IEnumerator PrepareRound(float time) {
        // countdown from `time`
        prepareMessageText.text = "STARTING IN";
        prepareRoundMenu.SetActive(true);
        player.SetActive(false);
        ui.SetActive(false);
        while (time > 0) {
            // update countdown timer text
            SetTimerTime(prepareCountdownText, time);
            yield return new WaitForSeconds(1f);
            time -= 1f;
        }
        prepareRoundMenu.SetActive(false);
        StartRound();
        isPlaying = true;
    }

    private void SetTimerTime(TMPro.TextMeshProUGUI timer, float time) {
        float roundedTime = Mathf.Round(time);
        timer.text = roundedTime.ToString();
    }

    private TMPro.TextMeshProUGUI FindTextFromFrame(GameObject source, string target) {
        foreach ( Transform child in source.transform ) {
            if (child.gameObject.name == "Frame") {
                foreach ( Transform frameChild in child.gameObject.transform ) {
                    if ( frameChild.gameObject.name == target ) {
                        return frameChild.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                    }
                }
            }
        }
        return null;
    }



}
