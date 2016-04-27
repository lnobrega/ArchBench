// Type: System.Collections.Generic.KeyValuePair`2
// Assembly: mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v2.0.50727\mscorlib.dll

using System;

namespace System.Collections.Generic
{
    [ Serializable ]
    public struct KeyValuePair< TKey, TValue >
    {
        public KeyValuePair( TKey key, TValue value );
        public TKey Key { get; }
        public TValue Value { get; }
        public override string ToString();
    }
}
