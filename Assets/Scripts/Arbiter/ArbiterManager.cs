using System.Collections.Generic;
using UnityEngine;

public class ArbiterManager : MonoBehaviour {

    [SerializeField] List<Item> targetSelections;
    [SerializeField] private Item targetItem;
    [SerializeField] private TMPro.TextMeshProUGUI targetItemText;
    [SerializeField] private TMPro.TextMeshProUGUI itemsLeftToBringText;

    private RoundManager roundManager;

    private int itemsLeftToBring = 3;

    private void Start() {
        ChangeTargetItem();
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
        itemsLeftToBringText.text = $"ITEMS LEFT: { itemsLeftToBring.ToString() }";
    }

    public void ChangeTargetItem() {
        targetItem = targetSelections[Random.Range(0, targetSelections.Count)];
        targetItemText.text = $"TARGET: { targetItem.prefab.name.ToUpper() }";
        Debug.Log($"Changing Target Item: { targetItem.prefab.name }");
    }

    public void PickUpItem(GameObject player) {
        player.GetComponent<ScoreManager>().AddScore();

        ChangeTargetItem();
        itemsLeftToBring -= 1;
        itemsLeftToBringText.text = $"ITEMS LEFT: { itemsLeftToBring.ToString() }";
        if ( itemsLeftToBring == 0 ) {
            roundManager.StopRound();
        }
    }

    public string TargetItemName {
        get { return $"{this.targetItem.prefab.name}(Clone)"; }
        private set {}
    }

    public int ItemsLeftToBring { 
        set {
            itemsLeftToBring = value;
            itemsLeftToBringText.text = $"ITEMS LEFT: { itemsLeftToBring.ToString() }";
        }
     }
}
