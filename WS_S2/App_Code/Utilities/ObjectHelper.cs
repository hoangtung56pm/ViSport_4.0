using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace SMSManager_API.Library.Utilities
{
	public class ObjectHelper
	{
        public static T FillObject<T>(IDataReader _dr)
        {

            T objTarget = default(T);
            if (_dr == null) return objTarget;
            try
            {       
                if (_dr.Read())
                {
                    objTarget = Activator.CreateInstance<T>();
                    PropertyInfo[] objProperties = objTarget.GetType().GetProperties();
                    for (int i = 0; i < objProperties.Length; i++)
                    {
                        PropertyInfo property = objProperties[i];
                        if (property.CanWrite && !Convert.IsDBNull(_dr[property.Name]))
                        {
                            property.SetValue(objTarget, _dr[property.Name], null);
                        }
                    }
                }
            }
            finally
            {
                _dr.Close();
            }
            return objTarget;
        }
        public static List<T> FillCollection<T>(IDataReader _dr)
        {
            List<T> _list = new List<T>();
            if (_dr == null) return _list;
            try
            {
                while (_dr.Read())
                {
                    T objTarget = Activator.CreateInstance<T>();
                    PropertyInfo[] objProperties = objTarget.GetType().GetProperties();
                    for (int i = 0; i < objProperties.Length; i++)
                    {
                        PropertyInfo property = objProperties[i];
                        LazyInitAttribute lazy = new LazyInitAttribute(false);
                        object[] objs = property.GetCustomAttributes(true);
                        if (objs.Length > 0)
                            lazy = (LazyInitAttribute)objs[0];
                        if (lazy.IsLazyInit) continue;
                        if (property.CanWrite && !Convert.IsDBNull(_dr[property.Name]))
                        {
                            property.SetValue(objTarget, _dr[property.Name], null);
                        }

                    }
                    _list.Add(objTarget);
                }
            }
            finally
            {
                _dr.Close();
            }
            return _list;
        }
		public static object CreateObject(Type type, IDataReader dr)
		{

			object objTarget = null;
			try
			{
				if (dr.Read())
				{
					objTarget = Activator.CreateInstance(type);
					PropertyInfo[] objProperties = type.GetProperties();
					for (int i = 0; i < objProperties.Length; i++)
					{
						PropertyInfo property = objProperties[i];
						if (property.CanWrite)
						{
                            if (dr[property.Name] != null)
                            {
                                try
                                {
                                    property.SetValue(objTarget, dr[property.Name], null);
                                }
                                catch (Exception ex)
                                {
                                    property.SetValue(objTarget, "", null);
                                }
                               
                            }
                            else 
                            {
                                property.SetValue(objTarget, "", null);
                            }
						}
					}

				}
			}finally
			{
				dr.Close();
			}
			return objTarget;
		}
	}
}
