using System.Collections;
using System.Collections.Generic;
using System;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using UnityEngine;

public class TwitchClient : MonoBehaviour
{
    public Client client;
    private string channel_name = "twitchplayab";


    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
        ConnectionCredentials credentails = new ConnectionCredentials("twitchplayoldmaid", Secrets.bot_access_token);

        client = new Client();
        client.Initialize(credentails, channel_name);


        client.Connect();
        client.OnChatCommandReceived += Client_OnChatCommandReceived;
    }

    private void Client_OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {
        if (CheckNumber(e.Command.CommandText))
            Debug.Log(e.Command.CommandText);
   
    }



    private bool CheckNumber(string commandText)
    {
        try
        {
            int num = Convert.ToInt32(commandText);
            if(num>=0 && num<=5)
                return true;
            return false;
        }
        catch
        {
            return false;
        }
    }

  


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            client.SendMessage(client.JoinedChannels[0], "Test start");
        }
    }
}
