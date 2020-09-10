using UnityEngine;

public class GameManager : MonoBehaviour {
    private float maxRountTime = 5f;
    private float preparationTime = 5f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startGameMenu;
    [SerializeField] private GameObject roundEndMenu;
    [SerializeField] private GameObject ui;

    private TMPro.TextMeshProUGUI countdownText;
    private TMPro.TextMeshProUGUI timerText;

    private void Start() {
        Debug.Log("Game start in 15 seconds");
        StopGame();
        // find countdown child of `startGameMenu`
        countdownText = FindFromFrame(startGameMenu, "Countdown");

        // find ui timer
        timerText = FindFromFrame(ui, "Timer");
    }

    void Update() {
        if ( preparationTime > 0 ) {
            preparationTime -= Time.deltaTime;
            SetTime(countdownText, preparationTime);

            if ( preparationTime < 0) {
                // after 15 seconds enabled players
                StartGame();
            }   
        } else {
            // after 1 minute disable players
            if ( maxRountTime > 0 ) {
                maxRountTime -= Time.deltaTime;
                SetTime(timerText, maxRountTime);
                
                if ( maxRountTime < 0 ) {
                    // show gameover menu
                    StopGame();
                    Debug.Log("Game Over!");
                }
            }
        }
    }

    private TMPro.TextMeshProUGUI FindFromFrame(GameObject source, string target) {
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

    private void SetTime(TMPro.TextMeshProUGUI timer, float time) {
        float roundedTime = Mathf.Round(time);
        timer.text = roundedTime.ToString();
    }

    private void StopGame() {
        // disabled players
        player.SetActive(false);
        // show start menu
        if ( maxRountTime < 0 ) {
            roundEndMenu.SetActive(true);
        } else {
            startGameMenu.SetActive(true);
        }
    
        ui.SetActive(false);
    }

    private void StartGame() {
        // enable player
        player.SetActive(true);
        // hide start menu
        startGameMenu.SetActive(false);
        ui.SetActive(true);
    }
}
