using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalInfo {
    static string _UserID;
    static string _UserID2;
    static string _Username;
    static int _Userscore;
    static int _WorldNum;
    static int _QuestionNum;
    static int _LevelID;

    public static string UserID { get { return _UserID; } set { _UserID = value; } }
    public static string UserID2 { get { return _UserID2; } set { _UserID2 = value; } }
    public static string Usernameglobal { get { return _Username; } set { _Username = value; } }
    public static int Userscoreglobal { get { return _Userscore; } set { _Userscore = value; } }
    public static int Worldglobal { get { return _WorldNum; } set { _WorldNum = value; } }
    public static int LevelID { get { return _LevelID; } set { _LevelID = value; } }
    public static int Questionglobal { get { return _QuestionNum; } set { _QuestionNum = value; } }


}
