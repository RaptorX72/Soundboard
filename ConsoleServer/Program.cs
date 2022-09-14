using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleServer {
    class Program {
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Socket> clientSockets = new List<Socket>();
        private static List<string> clientUsernames = new List<string>();
        private const int BUFFER_SIZE = 2048;
        private const string XMLFileName = "template.xml";
        private static int PORT = 100;
        private static byte[] buffer = new byte[BUFFER_SIZE];
        private static bool isQueueRunning = false;
        private static Thread queuePlayingThread;
        private static List<AudioQueue> audioQueues = new List<AudioQueue>();

        static void Main() {
            Console.Title = "Server";
			try {
                audioQueues = QueueLoader.LoadAudioQueuesFromXmlFile(XMLFileName);
			} catch (Exception ex) {
                Console.WriteLine("Failed to load queue file. Reason:");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
			}
            string command = "";
            Greeting();
			while (true) {
                Console.WriteLine($"Specify port (enter nothing to use default, {PORT})");
                command = Console.ReadLine();
                if (command == "") {
                    Console.WriteLine($"Using default port ({PORT})");
                    break;
                }
                int portnum;
                if (!int.TryParse(command, out portnum)) {
                    Console.WriteLine($"Input was not a number!");
                    continue;
                }
                if (portnum < 0 || portnum > UInt16.MaxValue) Console.WriteLine($"Port number incorrect! Please try again! (Must be between 0 and {UInt16.MaxValue})");
                else {
                    PORT = portnum;
                    Console.WriteLine($"Port set to {PORT}");
                    break;
                }
            }
            SetupServer();
            while (true) {
                command = Console.ReadLine().ToLower(); // When we press enter close everything
                if (command == "exit") break;
                else if (command == "help") {
                    Console.WriteLine(@"clear -- Clears the console
                        listc -- Lists all connected clients
                        reloadq -- Reloads the queue file
                        listq  -- Lists all the queues (ID, name)
                        playq ID -- Plays the specified queue
                        stopq -- Stops the current queue
                        exit -- Shuts down the server");
                } else if (command == "clear") Console.Clear();
                else if (command == "listc") {
                    Console.WriteLine("Connected clients:");
                    for (int i = 0; i < clientUsernames.Count; i++) Console.WriteLine($"{i}:{clientUsernames[i]}");
                } else if (command == "reloadq") {
                    AudioQueue[] backup = new AudioQueue[audioQueues.Count];
                    audioQueues.CopyTo(backup);
                    audioQueues.Clear();
                    try {
                        audioQueues = QueueLoader.LoadAudioQueuesFromXmlFile(XMLFileName);
                    } catch (Exception ex) {
                        audioQueues.Clear();
                        audioQueues.AddRange(backup);
                        Console.WriteLine("Error while reloading queue! Reason:");
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                    Console.WriteLine("Reloaded queues!");
                } else if (command == "stopq") {
                    isQueueRunning = false;
                    foreach (Socket client in clientSockets) SendStopAudioCommandToSocket(client);
                    Console.WriteLine("Stopped current queue!");
                } else if (command == "listq") {
                    Console.WriteLine("Audio queues:");
                    for (int i = 0; i < audioQueues.Count; i++) Console.WriteLine($"{i}:{audioQueues[i].QueueName} ({audioQueues[i].Voicelines.Count})");
                } else if (command.Contains("playq")) {
                    string[] substrings = command.Split(' ');
                    if (substrings.Length != 2) {
                        Console.WriteLine("Unexpected number of arguments for playq! Expected 2");
                        continue;
                    }
                    int index = -1;
                    if (!int.TryParse(substrings[1], out index)) {
                        Console.WriteLine("Paramter was not a number!");
                        continue;
                    }
                    if (index < 0 || index >= audioQueues.Count) {
                        Console.WriteLine("Audio queue in specified index does not exist!");
                        continue;
                    }
                    StartThread(audioQueues[index]);
                }
                /*if (command.Contains("play")) {
                    string[] subtext = command.Split(' ');
                    int clientID = Convert.ToInt32(subtext[1]);
                    byte[] data = Encoding.ASCII.GetBytes($"PLAY AUDIO:{subtext[2]}");
                    clientSockets[clientID].Send(data);
				}*/
            }
            CloseAllSockets();
        }

        private static void StartAudioQueue(AudioQueue aq) {
            Console.WriteLine($"Started playing {aq.QueueName} queue!");
            isQueueRunning = true;
			foreach (VoiceLine item in aq.Voicelines) {
                if (!isQueueRunning) break;
                int id = GetSocketIDFromUsername(item.SpeakerName);
                if (id != -1) {
                    int sleepDuration = (item.Duration + item.AfterPause) * 1000;
                    Console.WriteLine($"Sending {item.SpeakerName} voice line {item.AudioFile} with a total sleep duration of {sleepDuration}");
                    SendAudioCommandToSocket(clientSockets[id], item.AudioFile);
                    Thread.Sleep(sleepDuration);
                }
			}
            Console.WriteLine($"Finished playing {aq.QueueName} queue!");
            isQueueRunning = false;
            DestroyThread();
		} 

        private static int GetSocketIDFromUsername(string username) {
			for (int i = 0; i < clientUsernames.Count; i++) if (username == clientUsernames[i]) return i;
            return -1;
		}

        private static void SendAudioCommandToSocket(Socket socket, string audioFile) {
            SendCommandToSocket(socket, $"PLAY AUDIO:{audioFile}");
        }

        private static void SendStopAudioCommandToSocket(Socket socket) {
            SendCommandToSocket(socket, "STOP PLAYING");
        }

        private static void SendCommandToSocket(Socket socket, string command) {
            byte[] data = Encoding.ASCII.GetBytes(command);
            socket.Send(data);
        }

        private static void StartThread(AudioQueue aq) {
            queuePlayingThread = new Thread(delegate () {
                StartAudioQueue(aq);
            });
            queuePlayingThread.Start();
		}

        private static void DestroyThread() {
            if (queuePlayingThread.ThreadState == ThreadState.Running) queuePlayingThread.Abort();
		}

        private static void SetupServer() {
            Console.WriteLine("Setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete\nType 'help' to see available commands!");
        }

        private static void CloseAllSockets() {
            foreach (Socket socket in clientSockets) {
                byte[] data = Encoding.ASCII.GetBytes("SERVER STOP");
                socket.Send(data);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            serverSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult AR) {
            Socket socket;
            try {
                socket = serverSocket.EndAccept(AR);
            } catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
              {
                return;
            }

            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected, waiting for request...");
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult AR) {
            Socket current = (Socket)AR.AsyncState;
            int received;
            try {
                received = current.EndReceive(AR);
            } catch (SocketException ex) {
                current.Close();
                int socketID = GetSocketIndex(current);
                clientSockets.Remove(current);
                if (socketID == -1) Console.WriteLine("Unauthenticated client forcefully disconnected");
                else {
                    string username = clientUsernames[socketID];
                    clientUsernames.RemoveAt(socketID);
                    Console.WriteLine($"Client {username} forcefully disconnected");
					Console.WriteLine(ex.Message);
                }
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Received Text: " + text);

            if (text.Contains("AUTH REQ USERNAME")) {
                string username = text.Split(':')[1];
                bool found = false;
				foreach (string clientUsername in clientUsernames) {
                    if (clientUsername == username || username == "EZNEMJO") {
                        found = true;
                        break;
					}
				}
                if (found) {
                    byte[] data = Encoding.ASCII.GetBytes("AUTH RESP USERNAME WRONG");
                    current.Send(data);
                    current.Shutdown(SocketShutdown.Both);
                    current.Close();
                    clientSockets.Remove(current);
                    Console.WriteLine($"Authentication failed, disconnecting client.");
                    return;
                } else {
                    byte[] data = Encoding.ASCII.GetBytes("AUTH RESP USERNAME OK");
                    current.Send(data);
                    clientUsernames.Add(username);
                    Console.WriteLine($"Authentication success, added {username} to client list.");
                }
			} else if (text.ToLower() == "exit") {
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                int socketID = GetSocketIndex(current);
                string username = clientUsernames[socketID];
                clientSockets.Remove(current);
                clientUsernames.RemoveAt(socketID);
                Console.WriteLine($"Client {username} disconnected!");
                return;
            }
            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }

        private static int GetSocketIndex(Socket socket) {
            int socketID = -1;
            for (int i = 0; i < clientSockets.Count; i++) {
                if (clientSockets[i] == socket) {
                    socketID = i;
                    break;
                }
            }
            return socketID;
        }

        private static void Greeting() {
            Console.WriteLine(@"-----------------------------------
	                Soundboard server
	                by RaptorX72, 2021
                -----------------------------------");
		}
    }
}