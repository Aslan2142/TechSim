using Godot;
using System;
using System.Text.Json;

public class MenuUI : Node2D
{

    public LineEdit IpAddressLineEdit { get; private set; }
    public LineEdit PortLineEdit { get; private set; }
    public LineEdit UsernameLineEdit { get; private set; }
    public LineEdit PasswordLineEdit { get; private set; }

    public override void _Ready()
    {
        // Set up properties
        IpAddressLineEdit = GetNode("IpAddressLineEdit") as LineEdit;
        PortLineEdit = GetNode("PortLineEdit") as LineEdit;
        UsernameLineEdit = GetNode("UsernameLineEdit") as LineEdit;
        PasswordLineEdit = GetNode("PasswordLineEdit") as LineEdit;
    }

    // Connect to the server
    public void _on_ConnectButton_pressed()
    {
        Client client = GetNode("/root/Client") as Client;
        client.ConnectToServer(IpAddressLineEdit.Text, Convert.ToInt32(PortLineEdit.Text));
    }

    // Quit game
    public void _on_ExitButton_pressed()
    {
        GetTree().Quit();
    }

}