﻿using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class DuckCityHub
    {
        private static HubConnection _hubConnection;

        public static bool OnRoomPushInBar { get; set; }
        public static bool OnRoomPushInGame { get; set; }
        public static bool OnPlayersPushInBar { get; set; }
        public static bool OnPlayersPushInGame { get; set; }
        public static bool OnGamePushInGame { get; set; }

        public static void StartHub(string containerId)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://game.canardecarlate.fr/",
                    options =>
                    {
                        options.Headers.Add("Authorization",
                            new AuthenticationHeaderValue("Bearer", DataSave.LoadDataString("token")).ToString());
                        options.Headers.Add("ContainerId", GlobalVariable.User.ContainerId);
                    })
                .WithAutomaticReconnect()
                .Build();
            _hubConnection.Reconnecting += error =>
            {
                Debug.Assert(_hubConnection.State == HubConnectionState.Reconnecting);
                return Task.CompletedTask;
            };
            _hubConnection.Closed += async _ =>
            {
                await Task.Delay(Random.Range(0, 5) * 1000).ConfigureAwait(false);
                await _hubConnection.StartAsync(); 
            };

            _hubConnection.On("PushRoom", (RoomDto roomDto) =>
            {
                GlobalVariable.RoomDto.SetRoom(roomDto);
                OnRoomPushInBar = true;
                OnRoomPushInGame = true;
                if (!SceneManager.GetSceneByName("BarScene").isLoaded)
                {
                    SceneManager.LoadScene("BarScene");
                }
            });

            _hubConnection.On("PushPlayers", (List<PlayerInWaitingRoomDto> players) =>
            {
                GlobalVariable.Players.RemoveAll(_ => true);
                GlobalVariable.Players.AddRange(players);
                OnPlayersPushInBar = true;
                OnPlayersPushInGame = true;
            }); 

            _hubConnection.On("PushGame", (GameDto game) =>
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
                _hubConnection.StartAsync();
                Debug.Log("Start hub connection in container (id : " + containerId + " fake)");
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
            await _hubConnection.InvokeAsync("LeaveRoom", GlobalVariable.RoomDto.Code);
        }

        public static async Task PlayerReady()
        {
            await _hubConnection.InvokeAsync("PlayerReady", GlobalVariable.RoomDto.Code);
        }

        public static async Task StartGame()
        {
            await _hubConnection.InvokeAsync("StartGame", GlobalVariable.RoomDto.Code);
        }

        public static async Task DrawCard(string playerWhereCardIsDrawingId)
        {
            await _hubConnection.InvokeAsync("DrawCard", GlobalVariable.RoomDto.Code, playerWhereCardIsDrawingId);
        }

        public static async Task QuitMidGame()
        {
            await _hubConnection.InvokeAsync("QuitMidGame", GlobalVariable.RoomDto.Code);
        }
    }
}