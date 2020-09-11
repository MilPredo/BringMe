using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private float maxRoundTime = 60f;
    private float preparationTime = 2f;
    private int maxRounds = 3;
    private float currentRoundTime;
    private float roundEndDelay = 60f;
    private int currentRound = 0;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startGameMenu;
    [SerializeField] private GameObject roundEndMenu;
    [SerializeField] private GameObject ui;

    private TMPro.TextMeshProUGUI countdownText;
    private TMPro.TextMeshProUGUI timerText;

    private void Start() {
        // set `currentRoundTime` to `maxRoundTime`
        // currentRoundTime = maxRoundTime;
        // // find timer `GameObjects`
        // countdownText = FindFromFrame(startGameMenu, "Countdown");
        // timerText = FindFromFrame(ui, "Timer");
        // // start preparation
        // StartCoroutine(PreparationTime());
    }

    void Update() {
        // // reduce `currentRoundTime` by 1
        // currentRoundTime -= Time.deltaTime;
        // // if `currentRoundTime` less than zero
        // if ( currentRoundTime < 0 ) {
        //     // round ended
        //     // set `setCurrentRoundTime` to maxRoundTime
        //     currentRoundTime = maxRoundTime;
        //     // increase `currentRound` by 1
        //     currentRound += 1;
        //     // disable `player`
        //     player.SetActive(false);
        //     // disable `ui`
        //     ui.SetActive(false);
        //     // if `currentRound` greater than `maxRounds`
        //     if ( currentRound > maxRounds ) {
        //         // enable `gameEndResultMenu`
        //         Debug.Log("Game End Results");
        //         // disable `ui`
        //         ui.SetActive(false);
        //     // endif
        //     } else {
        //     // else
        //         // enable `roundEndMenu`
        //         roundEndMenu.SetActive(true);
        //         // wait for 5 seconds

        //     // endelse
        //     }
        // // endif
        // }
    }

    private IEnumerator PreparationTime() {
        // disable gameplay
        player.SetActive(false);

        // wait for `preparationTime` before starting the game
        while (preparationTime > 0) {
            yield return new WaitForSeconds(1f);
            SetTime(countdownText, preparationTime);
            preparationTime -= 1f;
        }
        // start game
        // enable ui and player
        player.SetActive(true);
        ui.SetActive(true);
        // disable `startGameMenu`
        startGameMenu.SetActive(false);
    }

    private TMPro.TextMeshProUGUI FindFromFrame(GameObject source, string target) {
        foreach (Transform child in source.transform) {
            if (child.gameObject.name == "Frame") {
                foreach (Transform frameChild in child.gameObject.transform) {
                    if (frameChild.gameObject.name == target) {
                        return frameChild.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                    }
                }
            }
        }
        return null;
    }

    private void SetTime(TMPro.TextMeshProUGUI timer, float time) {
        float roundedTime = Mathf.Round(time);
        timer.text = roundedTime.ToString();
    }

    private void StopGame() {
        // disabled players
        player.SetActive(false);
        // hide `ui`
        ui.SetActive(false);

        if (currentRoundTime < 0) {
            // round ended
            roundEndMenu.SetActive(true);
            float counter = 2f;
            while (counter > 0) {
                counter -= Time.deltaTime;
            }
            StartGame();
        } else {
            // show `startGameMenu`
            startGameMenu.SetActive(true);
        }
    }

    private void StartGame() {
        // enable player
        player.SetActive(true);
        // hide start menu
        startGameMenu.SetActive(false);
        // hide ui
        ui.SetActive(true);
        // hide round end menu
        roundEndMenu.SetActive(false);
        // increase `currentRound` by 1
        currentRound += 1;
        // set `currentRoundTime` to `maxRoundTime`
        currentRoundTime = maxRoundTime;
        if (currentRound <= maxRounds) {
            // show `gameResultMenu`
            Debug.Log("Showing End Result");
            StopGame();
        }
    }
}
