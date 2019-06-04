using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;

namespace Mirror.LiteNetLib4Mirror
{
	[RequireComponent(typeof(NetworkManager))]
	[RequireComponent(typeof(NetworkManagerHUD))]
	[RequireComponent(typeof(LiteNetLib4MirrorTransport))]
	[RequireComponent(typeof(LiteNetLib4MirrorDiscovery))]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// ReSharper disable once InconsistentNaming
	public class NetworkDiscoveryHUD : MonoBehaviour
	{
		[SerializeField] public float discoveryInterval = 1f;
		//private NetworkManagerHUD _managerHud;
		public bool _noDiscovering = true;

		/*private void Awake()
		{
			_managerHud = GetComponent<NetworkManagerHUD>();
		}*/

		/*private void OnGUI()
		{
			if (!_managerHud.showGUI)
			{
				_noDiscovering = true;
				return;
			}

			GUILayout.BeginArea(new Rect(10 + _managerHud.offsetX + 215 + 10, 40 + _managerHud.offsetY, 215, 9999));
			if (!NetworkClient.isConnected && !NetworkServer.active)
			{
				if (_noDiscovering)
				{
					if (GUILayout.Button("Start Discovery"))
					{
						StartCoroutine(StartDiscovery());
					}
				}
				else
				{
					GUILayout.Label("Discovering..");
					GUILayout.Label($"LocalPort: {LiteNetLib4MirrorTransport.Singleton.port}");
					if (GUILayout.Button("Stop Discovery"))
					{
						_noDiscovering = true;
					}
				}
			}
			else
			{
				_noDiscovering = true;
			}

			GUILayout.EndArea();
            
		}*/

		private IEnumerator StartDiscovery()
		{
			_noDiscovering = false;

			LiteNetLib4MirrorDiscovery.InitializeFinder();
			LiteNetLib4MirrorDiscovery.Singleton.onDiscoveryResponse.AddListener(OnClientDiscoveryResponse);
			while (!_noDiscovering)
			{
                ResetList();
                for (int i = 0; i < 20; i++)
                {
                    LiteNetLib4MirrorDiscovery.SendDiscoveryRequest("NetworkManagerHUD");
                    yield return new WaitForSeconds(discoveryInterval / 100f);
                }
				yield return new WaitForSeconds(discoveryInterval);
			}

			LiteNetLib4MirrorDiscovery.Singleton.onDiscoveryResponse.RemoveListener(OnClientDiscoveryResponse);
			LiteNetLib4MirrorDiscovery.StopDiscovery();
		}

        public virtual void ResetList()
        {
            //Override
        }

		public virtual void OnClientDiscoveryResponse(IPEndPoint endpoint, string text)
		{
			//string ip = endpoint.Address.ToString();

            Debug.Log(text);

			/*NetworkManager.singleton.networkAddress = ip;
			NetworkManager.singleton.maxConnections = 2;
			LiteNetLib4MirrorTransport.Singleton.clientAddress = ip;
			LiteNetLib4MirrorTransport.Singleton.port = (ushort)endpoint.Port;
			LiteNetLib4MirrorTransport.Singleton.maxConnections = 2;
			NetworkManager.singleton.StartClient();*/
			//_noDiscovering = true;
		}

        public void StartSearch()
        {
            if (_noDiscovering)
            {
                StartCoroutine(StartDiscovery());
            }
        }

        public void EndSearch()
        {
            _noDiscovering = true;
        }
	}
}
