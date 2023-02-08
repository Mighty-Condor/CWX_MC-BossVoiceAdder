using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using HarmonyLib;
using Newtonsoft.Json;

namespace VoiceAdd
{

	public class BossVoicePatch : ModulePatch
	{

		public BossVoicePatch()
		{
			BossVoicePatch._targetType = PatchConstants.EftTypes.Single(new Func<Type, bool>(this.IsTargetType));
		}


		private bool IsTargetType(Type type)
		{
			return type.GetMethod("TakePhrasePath") != null;
		}


		protected override MethodBase GetTargetMethod()
		{
			return BossVoicePatch._targetType.GetMethod("TakePhrasePath");
		}


		[PatchPrefix]
		private static void PatchPrefix()
		{
			string text = new StreamReader("./user/mods/MC-VOICE/voices.json").ReadToEnd();
			Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
			Dictionary<string, string> value = Traverse.Create(BossVoicePatch._targetType).Field<Dictionary<string, string>>("dictionary_0").Value;
			foreach (string key in dictionary.Keys)
			{
				string value2;
				dictionary.TryGetValue(key, out value2);
				bool flag = !value.ContainsKey(key);
				if (flag)
				{
					value.Add(key, value2);
				}
			}
		}


		private static Type _targetType;
	}
}