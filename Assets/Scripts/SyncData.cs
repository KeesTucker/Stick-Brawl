using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SyncData : object {

    public static Color color;

    public static int gameMode;

    public static bool[] gameModes;

    public static bool[] maps;

    public static bool failed;

    public static int numPlayers = 5;

    public static string name;
    public static string serverName;

    public static int worldSize = 13;

    public static int health = 400;

    public static float volume = 1f;
    public static float sfx = 1f;

    public static int kills;

    public static int targetFPS;

    public static int skinID;

    public static int chunkID;

    public static float gunScale;
    public static float moveScale;
    public static bool fixedFloating;

    public static KeyCode a = KeyCode.A;
    public static KeyCode d = KeyCode.D;
    public static KeyCode s = KeyCode.S;
    public static KeyCode space = KeyCode.Space;
    public static KeyCode i = KeyCode.I;
    public static KeyCode f = KeyCode.F;
    public static KeyCode r = KeyCode.R;
    
    public static List<ServerUIObject> servers = new List<ServerUIObject>();
}
