using System;

[Serializable]
public enum RequestType
{
    Disconnect, GetTime, GetTasks // TO-DO
}

[Serializable]
public enum ResponseType
{
    InvalidData, InvalidRequest, Time, Tasks // TO-DO
}