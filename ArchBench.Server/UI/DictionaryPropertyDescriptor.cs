using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchBench.Server.UI
{
    class DictionaryPropertyDescriptor : PropertyDescriptor
    {
        readonly IDictionary<string,string> mDictionary;
        readonly string mKey;

        internal DictionaryPropertyDescriptor( IDictionary<string, string> d, string key ) : base( key.ToString(), null )
        {
            mDictionary = d;
            mKey = key;
        }

        public override Type PropertyType
        {
            get { return mDictionary[mKey].GetType(); }
        }

        public override void SetValue( object component, object value )
        {
            mDictionary[mKey] = value.ToString();
        }

        public override object GetValue( object component )
        {
            return mDictionary[mKey];
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type ComponentType
        {
            get { return null; }
        }

        public override bool CanResetValue( object component )
        {
            return false;
        }

        public override void ResetValue( object component )
        {
        }

        public override bool ShouldSerializeValue( object component )
        {
            return false;
        }
    }
}
