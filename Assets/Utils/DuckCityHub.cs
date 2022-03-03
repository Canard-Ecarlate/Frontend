using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BestHTTP.SignalRCore;
using BestHTTP.SignalRCore.Authentication;
using BestHTTP.SignalRCore.Encoders;
//using Microsoft.AspNetCore.SignalR.Client;
using Models;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class DuckCityHub
    {
        public static HubConnection _hubConnection;

        public static bool OnRoomPushInBar { get; set; }
        public static bool OnRoomPushInGame { get; set; }
        public static bool OnPlayersPushInBar { get; set; }
        public static bool OnPlayersPushInGame { get; set; }
        public static bool OnGamePushInGame { get; set; }

        public static bool IsConnectedCreate { get; set; }

        public static void StartHub(string containerId, Func<Task> callback)
        {
            HubOptions options = new HubOptions();
            options.SkipNegotiation = true;
            _hubConnection = new HubConnection(new Uri("https://game.canardecarlate.fr"),
                new JsonProtocol(new LitJsonEncoder()),options);
            _hubConnection.AuthenticationProvider = new HubAuthProvider(_hubConnection, containerId);
            _hubConnection.OnConnected += obj =>
            {
                //IsConnectedCreate = true;
                callback();
                Debug.Log("connecté");
            };
            _hubConnection.OnError += (obj,error) =>
            {
                Debug.Log("Error");
                Debug.Log(error);
            };
            /*_hubConnection = new HubConnectionBuilder()
                .WithUrl("https://game.canardecarlate.fr/",
                    options =>
                    {
                        options.Headers.Add("Authorization",
                            new AuthenticationHeaderValue("Bearer", DataSave.LoadDataString("token")).ToString());
                        options.Headers.Add("ContainerId", containerId);
                    })
                .WithAutomaticReconnect()
                .Build();*/
            /*_hubConnection.OnReconnecting += error =>
            {
                //Debug.Assert(_hubConnection.State == HubConnectionState.Reconnecting);
                //return Task.CompletedTask;
            };*/
            /*_hubConnection.onClosed += async _ =>
            {
                await Task.Delay(Random.Range(0, 5) * 1000).ConfigureAwait(false);
                await _hubConnection.StartAsync(); 
            };*/

            _hubConnection.On<RoomDto>("PushRoom", (roomDto) =>
            {
                GlobalVariable.RoomDto.SetRoom(roomDto);
                OnRoomPushInBar = true;
                OnRoomPushInGame = true;
                if (!SceneManager.GetSceneByName("BarScene").isLoaded)
                {
                    SceneManager.LoadScene("BarScene");
                }
            });

            _hubConnection.On<List<PlayerInWaitingRoomDto>>("PushPlayers", (players) =>
            {
                GlobalVariable.Players.RemoveAll(_ => true);
                GlobalVariable.Players.AddRange(players);
                OnPlayersPushInBar = true;
                OnPlayersPushInGame = true;
            }); 

            _hubConnection.On<GameDto>("PushGame", (game) =>
            {
                GlobalVariable.GameDto.SetGame(game);
                OnGamePushInGame = true;
                if (!SceneManager.GetSceneByName("GameScene").isLoaded)
                {
                    SceneManager.LoadScene("GameScene");
                }  
            });

            try
            {
                _hubConnection.StartConnect();
                //await _hubConnection.StartAsync();
                Debug.Log("Start hub connection in container (id : " + containerId + " fake)");
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        public static async Task CreateRoom(RoomCreationDto roomCreationDto)
        {
            await _hubConnection.InvokeAsync<RoomCreationDto>("CreateRoom", roomCreationDto);
        }

        public static async Task JoinRoom(string roomCode)
        {
            await _hubConnection.InvokeAsync<string>("JoinRoom", roomCode);
        }

        public static async Task LeaveRoom()
        {
            await _hubConnection.InvokeAsync<string>("LeaveRoom", GlobalVariable.RoomDto.Code);
        }

        public static async Task PlayerReady()
        {
            await _hubConnection.InvokeAsync<string>("PlayerReady", GlobalVariable.RoomDto.Code);
        }

        public static async Task StartGame()
        {
            await _hubConnection.InvokeAsync<string>("StartGame", GlobalVariable.RoomDto.Code);
        }

        public static async Task DrawCard(string playerWhereCardIsDrawingId)
        {
            await _hubConnection.InvokeAsync<string>("DrawCard", GlobalVariable.RoomDto.Code, playerWhereCardIsDrawingId);
        }

        public static async Task QuitMidGame()
        {
            await _hubConnection.InvokeAsync<string>("QuitMidGame", GlobalVariable.RoomDto.Code);
        }
    }
}