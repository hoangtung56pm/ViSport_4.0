using System;
using System.Xml.Serialization;	 // For serialization of an object to an XML Document file.
using System.Runtime.Serialization.Formatters.Binary; // For serialization of an object to an XML Binary file.
using System.IO;				 // For reading/writing data to an XML file.
using System.IO.IsolatedStorage; // For accessing user isolated data.
using System.Collections.Generic;

namespace SMSManager_API.Library.Utilities
{
    

    /// <summary>
    /// Facade to XML serialization and deserialization of strongly typed objects to/from an XML file.
    /// 
    /// References: XML Serialization at http://samples.gotdotnet.com/:
    /// http://samples.gotdotnet.com/QuickStart/howto/default.aspx?url=/quickstart/howto/doc/xmlserialization/rwobjfromxml.aspx
    /// </summary>
    public static class ObjectCreateInstance<T> where T : class // Specify that T must be a class.
    {
        public static void savefile(T objT, string XML_FILE_NAME)
        {            
            ObjectXMLSerializer<T>.Save(objT, XML_FILE_NAME);
        }

    }

    public static class ObjectCreateInstance_Struct<T> where T : struct // Specify that T must be a struct.
    {
        public static void savefile(T objT, string XML_FILE_NAME)
        {
            ObjectXMLSerializer_Struct<T>.Save(objT, XML_FILE_NAME);
        }

    }
}