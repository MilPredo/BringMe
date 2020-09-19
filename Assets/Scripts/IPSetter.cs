using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IPSetter : MonoBehaviour {
    [SerializeField] NetworkManagerExt _network;

    public void SetIP() {
        InputField IP = GameObject.Find("IPField").GetComponent<InputField>();
        string ObjectsText = IP.text.Length >= 0 ? IP.text : "localhost";
        _network.networkAddress = ObjectsText;
        _network.StartClient();
    }

}
