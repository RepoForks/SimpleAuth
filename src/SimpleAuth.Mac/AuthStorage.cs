﻿using System;
using Foundation;
using Security;

namespace SimpleAuth
{
	public class AuthStorage : IAuthStorage
	{
		public void SetSecured(string key, string value, string clientId, string service, string sharedGroupId)
		{
			var s = new SecRecord(SecKind.GenericPassword)
			{
				Service = $"{clientId}-{key}-{service}",
			};

			SecStatusCode res;
			var match = SecKeyChain.QueryAsRecord(s, out res);
			if (res == SecStatusCode.Success)
			{
				var remStatus = SecKeyChain.Remove(s);
			}

			s.ValueData = NSData.FromString(value);

			if(!string.IsNullOrWhiteSpace(sharedGroupId))
				s.AccessGroup = sharedGroupId;

			var err = SecKeyChain.Add(s);
			Console.WriteLine (err);
		}
		public string GetSecured(string id, string clientId, string service, string sharedGroupId)
		{
			var rec = new SecRecord(SecKind.GenericPassword)
			{
				Service = $"{clientId}-{id}-{service}",
			};

			SecStatusCode res;
			var match = SecKeyChain.QueryAsRecord(rec, out res);
			if (res == SecStatusCode.Success)
				return match.ValueData.ToString();
			return "";
		}
	}
}

