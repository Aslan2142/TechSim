using Godot;
using System;

public class MenuUI : Node2D
{
    
    // Connect to the server
    public void _on_ConnectButton_pressed()
    {

    }

    // Quit game
    public void _on_ExitButton_pressed()
    {
        GetTree().Quit();
    }

}