using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web;

namespace LuckyFone.Helper
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
                            property.SetValue(objTarget, convertType(_dr[property.Name], property.PropertyType), null);
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

        public static IList<T> FillCollection<T>(IDataReader _dr)
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
        public static List<T> FillTable<T>(DataTable _Table)
        {
            List<T> _list = new List<T>();
            if (_Table == null) return _list;
            try
            {
                foreach (DataRow r in _Table.Rows)
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
                        if (property.CanWrite && _Table.Columns.Contains(property.Name) && !Convert.IsDBNull(r[property.Name]))
                        {
                            property.SetValue(objTarget, convertType(r[property.Name], property.PropertyType), null);
                        }

                    }
                    _list.Add(objTarget);
                }
            }
            finally
            {
                //_dr.Close();
            }
            return _list;
        }
        public static List<T> FillRow<T>(DataRow r)
        {
            List<T> _list = new List<T>();
            if (r == null) return _list;
            try
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
                    if (property.CanWrite && r.Table.Columns.Contains(property.Name) && !Convert.IsDBNull(r[property.Name]))
                    {
                        property.SetValue(objTarget, convertType(r[property.Name], property.PropertyType), null);
                    }
                    else if (property.CanWrite && (!r.Table.Columns.Contains(property.Name) || !Convert.IsDBNull(r[property.Name])))
                    {
                        property.SetValue(objTarget, GetDefaultType(property.PropertyType), null);
                    }

                }
                _list.Add(objTarget);

            }
            finally
            {
                //_dr.Close();
            }
            return _list;
        }
        static object GetDefaultType(Type t)
        {
            if (t == typeof(string)) return string.Empty;
            if (t == typeof(decimal)) return 0;
            if (t == typeof(int)) return 0;
            if (t == typeof(DateTime)) return DateTime.Now;
            if (t == typeof(bool)) return false;
            if (t == typeof(Decimal)) return 0;
            return string.Empty;
        }
        static object convertType(object value, Type t)
        {
            object ret = null;
            if (t == typeof(int))
            {
                return ConvertUtility.ToInt32(value);
            }
            else if (t == typeof(bool))
                ret = ConvertUtility.ToBoolean(value);
            else if (t == typeof(DateTime))
                ret = ConvertUtility.ToDateTime(value);
            else if (t == typeof(String))
                ret = ConvertUtility.ToString(value);
            else if (t == typeof(Decimal))
                ret = Convert.ToDecimal(value);
            else if (t == typeof(Double))
                ret = Convert.ToDouble(value);
            else
                ret = value;
            return ret;

        }
        public static object CreateObject(Type type, IDataReader dr)
        {

            object objTarget = null;
            try
            {
                if (dr.Read())
                {
                    //Activator.
                    objTarget = Activator.CreateInstance(type);
                    PropertyInfo[] objProperties = type.GetProperties();
                    for (int i = 0; i < objProperties.Length; i++)
                    {
                        PropertyInfo property = objProperties[i];
                        if (property.CanWrite)
                        {
                            if (dr.GetSchemaTable().Select("ColumnName='" + property.Name + "'").Length > 0)
                            {
                                if (dr[property.Name] != null)
                                {
                                    try
                                    {
                                        property.SetValue(objTarget, convertType(dr[property.Name], property.PropertyType), null);
                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            property.SetValue(objTarget, ConvertUtility.ToInt32(dr[property.Name]), null);
                                        }
                                        catch
                                        {
                                            if (ConvertUtility.ToInt32(dr[property.Name]) == -1)
                                            {
                                                try
                                                {
                                                    property.SetValue(objTarget, true, null);
                                                }
                                                catch
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
                                else
                                {
                                    property.SetValue(objTarget, "", null);
                                }
                            }
                        }
                    }

                }
            }
            finally
            {
                dr.Close();
            }
            return objTarget;
        }
    }

}