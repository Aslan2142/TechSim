using Godot;
using System;

public class MenuUI : Node2D
{

    public LineEdit IpAddressLineEdit { get; private set; }
    public LineEdit PortLineEdit { get; private set; }
    public LineEdit UsernameLineEdit { get; private set; }
    public LineEdit PasswordLineEdit { get; private set; }
    public Label InfoMessageLabel { get; private set; }

    public override void _Ready()
    {
        // Set up properties
        IpAddressLineEdit = GetNode("IpAddressLineEdit") as LineEdit;
        PortLineEdit = GetNode("PortLineEdit") as LineEdit;
        UsernameLineEdit = GetNode("UsernameLineEdit") as LineEdit;
        PasswordLineEdit = GetNode("PasswordLineEdit") as LineEdit;
        InfoMessageLabel = GetNode("InfoMessageLabel") as Label;
    }

    // Connect to the server
    public void _on_ConnectButton_pressed()
    {
        // Check port
        int port = 0;
        try {
            port = Convert.ToInt32(PortLineEdit.Text);
            if (port < 1 || port > 65535)
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            InfoMessageLabel.Text = "nPort has to be a number between 1 and 65535";
            return;
        }

        // Check input
        if (IpAddressLineEdit.Text.Length == 0 || UsernameLineEdit.Text.Length == 0 || PasswordLineEdit.Text.Length == 0)
        {
            InfoMessageLabel.Text = "You must fill all the text fields in order to connect to the server";
            return;
        }

        Client client = GetNode("/root/Client") as Client;
        client.ConnectToServer(IpAddressLineEdit.Text, port, UsernameLineEdit.Text, PasswordLineEdit.Text);

        InfoMessageLabel.Text = client.serverMessage;
    }

    // Quit game
    public void _on_ExitButton_pressed()
    {
        GetTree().Quit();
    }

}