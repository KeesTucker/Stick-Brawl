using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;

public class ServerUIObject : IComparable<ServerUIObject>
{
    public string name;
    public int port;
    public IPEndPoint endpoint;

    public ServerUIObject(string newName, int newPort, IPEndPoint newEndPoint)
    {
        name = newName;
        port = newPort;
        endpoint = newEndPoint;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(ServerUIObject other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in port.
        return port - other.port;
    }
}
