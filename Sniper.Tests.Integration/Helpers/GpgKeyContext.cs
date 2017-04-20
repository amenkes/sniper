﻿using System;

namespace Sniper.Tests.Integration.Helpers
{
    public class GpgKeyContext : IDisposable
    {
        internal GpgKeyContext(IConnection connection, GpgKey key)
        {
            _connection = connection;
            Key = key;
            GpgKeyId = key.Id;
            KeyId = key.KeyId;
            PublicKeyData = key.PublicKey;
        }

        private IConnection _connection;
        internal int GpgKeyId { get; set; }
        internal string KeyId { get; set; }
        internal string PublicKeyData { get; set; }

        internal GpgKey Key { get; set; }

        public void Dispose()
        {
            if (Key != null)
            {
                Helper.DeleteGpgKey(_connection, Key);
            }
        }
    }
}
