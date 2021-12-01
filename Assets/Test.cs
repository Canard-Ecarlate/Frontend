using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

public class Test : MonoBehaviour
{
    private int _x;
    private static HubConnection _connection;
    void Start()
    {
        SetPosition(5);
        Debug.Log("Hello World guys!");
        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:5001/canardecarlatehub")
            .Build();
        _connection.Closed += async (error) =>
        {
            await Task.Delay(Random.Range(0, 5) * 1000);
            await _connection.StartAsync();
        };

        Connect();

        Send("Evi");
        Send("Sebastian");
        Send("Theo");
        Send("Dylan");
        Send("Alex");
        Send("Bastien");
    }

    private async void Connect()
    {
        _connection.On<string>("AfterSendMessageAsync", (data) =>
        {
            Debug.Log($"{data}");
        });

        try
        {
            await _connection.StartAsync();
            Debug.Log("Connection started");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private async void Send(string msg)
    {
        try
        {
            await _connection.InvokeAsync("SendMessageAsync", msg);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void SetPosition(int x)
    {
        this._x = x;
    }

    private void Update()
    {
        transform.position = new Vector2(_x, 0);
    }
}