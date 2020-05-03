using ENet;
using System;

namespace NetworkedKeyboard {
	class Program {
		static void Main(string[] args) {
			using (Host server = new Host()) {
				server.InitializeServer(6789, 2);

				Event netEvent;

				while (!Console.KeyAvailable) {
					bool polled = false;

					while (!polled) {
						if (server.CheckEvents(out netEvent) == false) {
							if (server.Service(15, out netEvent) == false)
								break;

							polled = true;
						}

						switch (netEvent.Type) {
							case EventType.None:
								break;

							case EventType.Connect:
								Console.WriteLine("Client connected - ID: " + netEvent.ChannelID + ", IP: " + netEvent.Peer.Host);
								break;

							case EventType.Disconnect:
								Console.WriteLine("Client disconnected - ID: " + netEvent.ChannelID + ", IP: " + netEvent.Peer.Host);
								break;


							case EventType.Receive:
								Console.WriteLine("Packet received from - ID: " + netEvent.ChannelID + ", IP: " + netEvent.Peer.Host + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
								int t;
								Console.WriteLine(System.Text.Encoding.Default.GetString(netEvent.Packet.GetBytes()));
								if (Int32.TryParse(System.Text.Encoding.Default.GetString(netEvent.Packet.GetBytes()), out t)) {
									bool press = t < 20;
									switch (t % 20) {
										case 0:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.D4).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.D4).Invoke(); }
											break;
										case 1:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.D5).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.D5).Invoke(); }
											break;
										case 2:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.D6).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.D6).Invoke(); }
											break;
										case 3:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.D7).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.D7).Invoke(); }
											break;
										case 4:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.R).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.R).Invoke(); }
											break;
										case 5:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.T).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.T).Invoke(); }
											break;
										case 6:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.Y).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.Y).Invoke(); }
											break;
										case 7:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.U).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.U).Invoke(); }
											break;
										case 8:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.F).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.F).Invoke(); }
											break;
										case 9:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.G).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.G).Invoke(); }
											break;
										case 10:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.H).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.H).Invoke(); }
											break;
										case 11:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.J).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.J).Invoke(); }
											break;
										case 12:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.V).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.V).Invoke(); }
											break;
										case 13:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.B).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.B).Invoke(); }
											break;
										case 14:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.N).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.N).Invoke(); }
											break;
										case 15:
											if (press) { WindowsInput.Simulate.Events().Hold(WindowsInput.Events.KeyCode.M).Invoke(); } else { WindowsInput.Simulate.Events().Release(WindowsInput.Events.KeyCode.M).Invoke(); }
											break;
									}
								}
								netEvent.Packet.Dispose();
								break;
						}
					}
				}

				server.Flush();
			}
		}
	}
}
