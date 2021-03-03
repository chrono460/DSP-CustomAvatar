using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace Chrono460
{
	[BepInPlugin("me.chrono460.dspplugin.CustomAvatarPlugin", "Custom Avatar", "1.0")]
	public class CustomAvatarPlugin : BaseUnityPlugin
	{
		void Start()
		{
			// Harmony patch
			var harmony = new Harmony("me.chrono460.dspplugin.CustomAvatar");
			harmony.PatchAll(typeof(CustomAvatar));
		}


		public static class CustomAvatar
		{

			[HarmonyPrefix, HarmonyPatch(typeof(Player), "Create")]
			public static bool Prefix(ref Player __result, GameData gameData, int protoId)
			{
				// rewrite original method
				try
				{
					AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/player");
					GameObject gameObject = assetBundle.LoadAsset<GameObject>("Player");
					assetBundle.Unload(false);
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.name = gameObject.name;
					Player player = new Player();

					//player.gameObject = gameObject2;
					Traverse.Create(player).Property("gameObject").SetValue(gameObject2);

					//player.transform = gameObject2.transform;
					Traverse.Create(player).Property("transform").SetValue(gameObject2.transform);

					GameObject gameObject3 = new GameObject("Camera Target");
					gameObject3.transform.SetParent(player.transform, false);

					//player.cameraTarget = gameObject3.transform;
					Traverse.Create(player).Property("cameraTarget").SetValue(gameObject3.transform);

					GameObject gameObject4 = new GameObject("Build Target");
					gameObject4.transform.SetParent(null, false);
					player.uRotation = Quaternion.identity;

					//player.mecha = new Mecha();
					Traverse.Create(player).Property("mecha").SetValue(new Mecha());
					player.mecha.Init(player);

					//player.buildTarget = gameObject4.transform;
					Traverse.Create(player).Property("buildTarget").SetValue(gameObject4.transform);
					//player.controller = gameObject2.GetComponent<PlayerController>();
					Traverse.Create(player).Property("controller").SetValue(gameObject2.GetComponent<PlayerController>());
					//player.animator = gameObject2.GetComponent<PlayerAnimator>();
					Traverse.Create(player).Property("animator").SetValue(gameObject2.GetComponent<PlayerAnimator>());
					player.animator.player = player;
					//player.audio = gameObject2.GetComponent<PlayerAudio>();
					Traverse.Create(player).Property("audio").SetValue(gameObject2.GetComponent<PlayerAudio>());
					player.audio.player = player;
					//player.effect = gameObject2.GetComponent<PlayerEffect>();
					Traverse.Create(player).Property("effect").SetValue(gameObject2.GetComponent<PlayerEffect>());
					player.effect.player = player;
					//player.orders = new PlayerOrder(player);
					Traverse.Create(player).Property("orders").SetValue(new PlayerOrder(player));

					player.controller.gameData = gameData;
					player.controller.player = player;
					player.controller.mecha = player.mecha;

					//player.gizmo = new PlayerControlGizmo(player);
					Traverse.Create(player).Property("gizmo").SetValue(new PlayerControlGizmo(player));
					//player.navigation = new PlayerNavigation(gameData, player);
					Traverse.Create(player).Property("navigation").SetValue(new PlayerNavigation(gameData, player));
					player.navigation.Init();

					//player.sandCount = 0;
					Traverse.Create(player).Property("sandCount").SetValue(0);

					player.controller.Init();
					__result = player;
				}
				catch (Exception e)
				{

					//HarmonyLib.Tools.Logger.ChannelFilter = HarmonyLib.Tools.Logger.LogChannel.Info | HarmonyLib.Tools.Logger.LogChannel.Warn;
					//HarmonyLib.Tools.HarmonyFileLog.Enabled = true;
					Console.WriteLine("Loading customized avatar failed.");
					// direct to original method
					return true;
				}

				{
					// bypass original method
					Console.WriteLine("Loading customized avatar succeeded.");
					return false;
				}
			}
		}

	}
}
