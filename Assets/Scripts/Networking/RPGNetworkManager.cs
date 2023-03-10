using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class RPGNetworkManager : NetworkManager
{
    public override void OnClientConnect()
    {
        base.OnClientConnect();
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        
    }
}
