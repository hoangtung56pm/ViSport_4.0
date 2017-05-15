#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Utilities\MiscUtility.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "956869A50591265A19F607BD0517B731081D0110"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Utilities\MiscUtility.cs"
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;


namespace SMSManager_API.Library.Utilities
{
	public class MiscUtility
	{
        public const string MSG_DELETE_CONFIRM = "return confirm('Bạn đã chắc chắn xóa ?');";
        public const string MSG_CANCEL_CONFIRM = "return confirm('Bạn đã chắc chắn hủy ?');";
        public const string MSG_EXISTED_CONFIRM = "Nội dung này đã tồn tại! Bạn phải nhập lại";
		public const string MSG_UPDATE_SUCCESS = "<b>Cập nhật thành công !</b>";
        public const string MSG_REGISTER_SUCCESS = "<b>Quá trinh đăng ký thành công !</b>";
        public const string MSG_UPDATE_ERROR = "<b>Lỗi trong quá trình cập nhật !</b>";
		public const string MSG_CONTROKEY_EXISTS = "Control key đã tồn tại";

        public static string Alert(string strAlert)
        {
            return "return confirm('" + strAlert + "')";
        }
        public static string  GET_ALERT_MESSAGE(string input)
        {
            return "return confirm('" + input + "');";
        }
      
		public static string UpdateQueryStringItem(HttpRequest httpRequest, string queryStringKey, string newQueryStringValue)
		{
			StringBuilder NewURL = new StringBuilder();

			NewURL.Append(httpRequest.RawUrl);

			if (httpRequest.QueryString[queryStringKey] != null)
			{
				string OrignalSet = String.Format("{0}={1}", queryStringKey, httpRequest.QueryString[queryStringKey]);
				string NewSet = String.Format("{0}={1}", queryStringKey, newQueryStringValue);
				NewURL.Replace(OrignalSet, NewSet);
			}
			else if (httpRequest.QueryString.Count == 0)
			{
				NewURL.AppendFormat("?{0}={1}", queryStringKey, newQueryStringValue);
			}
			else
			{
				NewURL.AppendFormat("&{0}={1}", queryStringKey, newQueryStringValue);
			}

			return NewURL.ToString();
		}

		public static string UpdateQueryStringItem(HttpRequest httpRequest, string[] queryStringKeys, string[] newQueryStringValues)
		{
			StringBuilder NewURL = new StringBuilder();

			NewURL.Append(httpRequest.RawUrl.Replace("%20", " "));
			bool check = true;
			for (int i = 0; i < queryStringKeys.GetLength(0); i ++)
			{
				string queryStringKey = queryStringKeys[i];
				string newQueryStringValue = newQueryStringValues[i];
				if (httpRequest.QueryString[queryStringKey] != null)
				{
					string OrignalSet = String.Format("{0}={1}", queryStringKey, httpRequest.QueryString[queryStringKey]);
					string NewSet = String.Format("{0}={1}", queryStringKey, newQueryStringValue);
					NewURL.Replace(OrignalSet, NewSet);
				}
				else if (httpRequest.QueryString.Count == 0)
				{
					if (newQueryStringValue != "" && newQueryStringValue != null)
					{
						if (check)
						{
							NewURL.AppendFormat("?{0}={1}", queryStringKey, newQueryStringValue);
							check = false;
						}
						else NewURL.AppendFormat("&{0}={1}", queryStringKey, newQueryStringValue);
					}
				}
				else if (newQueryStringValue != "" && newQueryStringValue != null) NewURL.AppendFormat("&{0}={1}", queryStringKey, newQueryStringValue);
			}

			return NewURL.ToString();
		}

		public static void FillTreeData(ListItemCollection lst, DataTable dtCommands, string fieldKey, string fieldName, string fieldParentID, string sortBy)
		{
			lst.Clear();
			DataRow[] drRoots = dtCommands.Select(fieldParentID + "  = " + 0, sortBy);
			foreach (DataRow row in drRoots)
			{
				ListItem item = new ListItem();
				item.Value = row[fieldKey].ToString();
				item.Text = row[fieldName].ToString();
				item.Attributes.Add("Level", "0");
				lst.Add(item);
				LoadCmdItem(lst, item, dtCommands, fieldKey, fieldName, fieldParentID, sortBy);
			}
		}

		private static void LoadCmdItem(ListItemCollection lst, ListItem curItem, DataTable dtCommands, string fieldKey, string fieldName, string fieldParentID, string sortBy)
		{
			int level = Convert.ToInt32(curItem.Attributes["Level"]);
			level += 1;
			int curID = Convert.ToInt32(curItem.Value);
			DataRow[] drChilds = dtCommands.Select(fieldParentID + " = " + curID);
			foreach (DataRow row in drChilds)
			{
				ListItem childItem = new ListItem();
				childItem.Text = MiscUtility.StringIndent(level) + row[fieldName].ToString();
				childItem.Value = row[fieldKey].ToString();
				childItem.Attributes.Add("Level", level.ToString());
				lst.Add(childItem);
				LoadCmdItem(lst, childItem, dtCommands, fieldKey, fieldName, fieldParentID, sortBy);
			}
		}

		public static void FillIndex(DropDownList dropIndex, int min, int max, int selected)
		{
			dropIndex.Items.Clear();
			for (int i = min; i <= max; i++)
			{
				ListItem item = new ListItem(i.ToString(), i.ToString());
				if (i == selected) item.Selected = true;
				else item.Selected = false;
				dropIndex.Items.Add(item);
			}
		}

		public static void FillIndex(DropDownList dropIndex, int max, int selected)
		{
			dropIndex.Items.Clear();
			for (int i = 0; i <= max; i++)
			{
				ListItem item = new ListItem(i.ToString(), i.ToString());
				if (i == selected) item.Selected = true;
				else item.Selected = false;
				dropIndex.Items.Add(item);
			}
		}


		public static void SelectItemFromList(ListControl list, string selectedValue)
		{
			list.SelectedIndex = -1;
			ListItem item = list.Items.FindByValue(selectedValue);
			if (item != null)
				item.Selected = true;
		}
		public static void SelectItemFromList(ListControl list, string[] selectedValues)
		{
			list.SelectedIndex = -1;
			for(int i = 0; i < selectedValues.Length; i ++)
			{
				string selectedValue = selectedValues[i];
				ListItem item = list.Items.FindByValue(selectedValue);
				if (item != null)
					item.Selected = true;
			}
		}
        /// <summary>
        /// Sets the selected.
        /// </summary>
        /// <param name="lstItems">The LST items.</param>
        /// <param name="selectedValue">The selected value.</param>
        public static void SetSelected(ListItemCollection lstItems, string selectedValue)
        {
            ListItem item = lstItems.FindByValue(selectedValue);
            if (item != null) item.Selected = true;
        }

        /// <summary>
        /// Sets the selected.
        /// </summary>
        /// <param name="dropList">The drop list.</param>
        /// <param name="selectedValue">The selected value.</param>
        public static void SetSelected(DropDownList dropList, string selectedValue)
        {
            dropList.SelectedIndex = -1;
            SetSelected(dropList.Items, selectedValue);
        }

        /// <summary>
        /// Sets the selected.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="selectedValue">The selected value.</param>
        public static void SetSelected(ListBox list, string selectedValue)
        {
            list.SelectedIndex = -1;
            SetSelected(list.Items, selectedValue);
        }

        /// <summary>
        /// Sets the selected.
        /// </summary>
        /// <param name="rdoList">The rdo list.</param>
        /// <param name="selectedValue">The selected value.</param>
        public static void SetSelected(RadioButtonList rdoList, string selectedValue)
        {
            rdoList.SelectedIndex = -1;
            SetSelected(rdoList.Items, selectedValue);
        }

		public static string StringIndent(int level)
		{
			string retVal = string.Empty;
			for (int i = 0; i < level; i ++)
				retVal += ".....";
			return retVal;
		}

        public static string GetOriginalAvatar(string str)
        {
            str = str.ToLower();
            if (str.IndexOf("avatar") == -1) return str;
            string s = "";
            string name = "";
            try
            {
                int t = str.LastIndexOf("/avatar");
                s = str.Substring(t);
                name = str.Substring(str.LastIndexOf("/"));
            }
            catch (Exception ex)
            {
                return str;
            }
            return str.Replace(s, name);
        }
	}
}

#line default
#line hidden
