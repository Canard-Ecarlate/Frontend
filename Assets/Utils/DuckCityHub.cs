using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class DuckCityHub
    {
        private static HubConnection _hubConnection;
        public delegate void OnPlayersUpdateDelegate();
        public static event OnPlayersUpdateDelegate OnPlayersPush;
        public delegate void OnRoomUpdateDelegate();
        public static event OnRoomUpdateDelegate OnRoomPush;

        public static void StartHub()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7143/",
                    options =>
                    {
                        options.Headers.Add("Authorization",
                            new AuthenticationHeaderValue("Bearer", DataSave.LoadDataString("token")).ToString());
                    })
                .WithAutomaticReconnect()
                .Build();
            _hubConnection.Reconnecting += error =>
            {
                Debug.Assert(_hubConnection.State == HubConnectionState.Reconnecting);
                // Notify users the connection was lost and the client is reconnecting.
                // Start queuing or dropping messages.
                return Task.CompletedTask;
            };
            _hubConnection.Closed += async _ =>
            {
                await Task.Delay(Random.Range(0, 5) * 1000).ConfigureAwait(false);
                await _hubConnection.StartAsync();
            };

            _hubConnection.On("PushRoom", (RoomDto roomDto) =>
            {
                GlobalVariable.Room.SetRoom(roomDto);
                OnRoomPush?.Invoke();
            });

            _hubConnection.On("PushPlayers", (List<PlayerInWaitingRoomDto> players) =>
            {
                GlobalVariable.Players.RemoveAll(_ => true);
                GlobalVariable.Players.AddRange(players);
                OnPlayersPush?.Invoke();
            });

            _hubConnection.On("PushGame", (GameDto game) =>
            {
                GlobalVariable.Game.SetGame(game);
            });

            try
            {
                _hubConnection.StartAsync();
                Debug.Log("Hub connection started");
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        public static async Task CreateRoom(RoomCreationDto roomCreationDto)
        {
            await _hubConnection.InvokeAsync("CreateRoom", roomCreationDto);
        }

        public static async Task JoinRoom(string roomCode)
        {
            await _hubConnection.InvokeAsync("JoinRoom", roomCode);
        }

        public static async Task LeaveRoom()
        {
            await _hubConnection.InvokeAsync("LeaveRoom", GlobalVariable.Room.Code);
        }

        public static async Task PlayerReady()
        {
            await _hubConnection.InvokeAsync("PlayerReady", GlobalVariable.Room.Code);
        }

        public static async Task StartGame()
        {
            await _hubConnection.InvokeAsync("StartGame", GlobalVariable.Room.Code);
        }

        public static async Task DrawCard(string playerWhereCardIsDrawingId)
        {
            await _hubConnection.InvokeAsync("DrawCard", GlobalVariable.Room.Code, playerWhereCardIsDrawingId);
        }

        public static async Task QuitMidGame()
        {
            await _hubConnection.InvokeAsync("QuitMidGame", GlobalVariable.Room.Code);
        }
    }
}