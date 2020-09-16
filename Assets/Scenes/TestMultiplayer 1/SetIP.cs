using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIP : MonoBehaviour {
    public NetworkManagerExt netman;
    public void SetIPAddress(string ip) {
        netman.networkAddress = ip;
    }
}
