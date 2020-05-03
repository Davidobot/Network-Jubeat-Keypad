using ENet;
using System;

namespace NetworkedKeyboard {
	class Program {
		static void Main(string[] args) {
			ENet.Library.Initialize();
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
								netEvent.Packet.Dispose();
								break;
						}
					}
				}

				server.Flush();
			}
			ENet.Library.Deinitialize();
		}
	}
}
