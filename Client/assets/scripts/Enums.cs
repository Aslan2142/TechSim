using System;

[Serializable]
public enum RequestType
{
    Disconnect, GetTime, GetTasks, CheckCompatibility, Authorization
}

[Serializable]
public enum ResponseType
{
    InvalidData, InvalidRequest, Time, Tasks, IsCompatible, NotCompatible, Authorized, NotAuthorized, PublicKey
}