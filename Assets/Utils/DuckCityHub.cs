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

        public static void Start()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7143/", options => 
                { 
                    options.Headers.Add("Authorization", new AuthenticationHeaderValue("Bearer", DataSave.LoadDataString("token")).ToString());                    
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
                GlobalVariable.CurrentRoom.SetRoom(roomDto);
                Debug.Log($"Salle : {roomDto.Name}, Code : {roomDto.Code}");
            });
            
            _hubConnection.On("PushPlayers", (IEnumerable<PlayerInWaitingRoomDto> players) =>
            {
                foreach (PlayerInWaitingRoomDto player in players)
                {
                    Debug.Log($"Player : {player.Name}");
                }
            });
            
            _hubConnection.On("PushGame", (GameDto game) =>
            {
                Debug.Log($"{game}");
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

        public static bool CreateRoom(RoomCreationDto roomCreationDto)
        {
            try
            {
                _hubConnection.InvokeAsync("CreateRoom", roomCreationDto);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return false;
            }
        }

        public static bool JoinRoom(string roomCode)
        {
            try
            {
                _hubConnection.InvokeAsync("JoinRoom", roomCode);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return false;
            }
        }
        
        public static bool LeaveRoom(string roomCode)
        {
            try
            {
                _hubConnection.InvokeAsync("LeaveRoom", roomCode);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return false;
            }
        }
        
        public static bool PlayerReady(string roomCode)
        {
            try
            {
                _hubConnection.InvokeAsync("PlayerReady", roomCode);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return false;
            }
        }
    }
}