using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchBench.Server.UI
{
    internal class DictionaryPropertyGridAdapter : ICustomTypeDescriptor
    {
        readonly IDictionary<string,string> mDictionary;

        public DictionaryPropertyGridAdapter( IDictionary<string,string> d )
        {
            mDictionary = d;
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName( this, true );
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent( this, true );
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName( this, true );
        }

        public EventDescriptorCollection GetEvents( Attribute[] attributes )
        {
            return TypeDescriptor.GetEvents( this, attributes, true );
        }

        EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents( this, true );
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter( this, true );
        }

        public object GetPropertyOwner( PropertyDescriptor pd )
        {
            return mDictionary;
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes( this, true );
        }

        public object GetEditor( Type editorBaseType )
        {
            return TypeDescriptor.GetEditor( this, editorBaseType, true );
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ( (ICustomTypeDescriptor) this ).GetProperties( new Attribute[0] );
        }

        public PropertyDescriptorCollection GetProperties( Attribute[] attributes )
        {
            ArrayList properties = new ArrayList();
            foreach ( var key in mDictionary.Keys )
            {
                properties.Add( new DictionaryPropertyDescriptor( mDictionary, key ) );
            }

            PropertyDescriptor[] props = (PropertyDescriptor[]) properties.ToArray(typeof(PropertyDescriptor));

            return new PropertyDescriptorCollection( props );
        }
    }
}
