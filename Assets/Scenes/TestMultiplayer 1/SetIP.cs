using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetIP : MonoBehaviour {
    [SerializeField] NetworkManagerExt netman;
    public void Set() {
        InputField IP = GameObject.Find("IPField").GetComponent<InputField>();
        string ObjectsText = IP.text.Length >= 0 ? IP.text : "localhost";
        netman.networkAddress = ObjectsText;
        netman.StartClient();
    }
}
