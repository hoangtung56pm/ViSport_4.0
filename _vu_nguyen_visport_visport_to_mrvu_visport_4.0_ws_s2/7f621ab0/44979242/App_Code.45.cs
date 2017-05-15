#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Utilities\MultimediaUtility.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5C35A720902AA3AEA8706C3BB06FC5252183A1E0"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Utilities\MultimediaUtility.cs"
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Web;

namespace SMSManager_API.Library.Utilities
{
	public class MultimediaUtility
	{
		private static bool ThumbnailCallback()
		{
			return false;
		}
        public static Image Crop(Image p, int width, int height)
        {
            Bitmap b = new Bitmap(p);
            Bitmap imgCropped = new Bitmap(width, height);
            Graphics objGraphic = Graphics.FromImage(imgCropped);
            int intStartTop = 0;
            int intStartLeft = 0;
            objGraphic.DrawImage(b, intStartLeft, intStartTop);
            b.Dispose();
            objGraphic.Dispose();
           
            return imgCropped;
        }

		public static bool SetThumbnail(string filePath, string newPath, int iThumbWidth, int iThumbHeight)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			if (!fileInfo.Exists) return false;
			try
			{
				if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

				Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
				Bitmap myBitmap = new Bitmap(fileInfo.FullName);

				if ((iThumbHeight == 0) && (iThumbWidth == 0)) return false;
				else if ((iThumbHeight != 0) && (iThumbWidth == 0))
					iThumbWidth = (int) (iThumbHeight*myBitmap.Width)/myBitmap.Height;
				else if ((iThumbHeight == 0) && (iThumbWidth != 0))
					iThumbHeight = (int) (iThumbWidth*myBitmap.Height)/myBitmap.Width;

				Image myThumbnail = myBitmap.GetThumbnailImage(iThumbWidth, iThumbHeight, myCallback, IntPtr.Zero);
			    myThumbnail.Save(newPath + fileInfo.Name, ImageFormat.Jpeg);
			    myBitmap.Dispose();
                myThumbnail.Dispose();
                GC.Collect();
			}
			catch
			{
				return false;
			}
			return true;
		}
        public static bool SetThumbnailWallpaper(string filePath, string newPath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return false;
            try
            {
                if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap = new Bitmap(fileInfo.FullName);

                if ((iThumbHeight == 0) && (iThumbWidth == 0)) return false;
                else if ((iThumbHeight != 0) && (iThumbWidth == 0))
                    iThumbWidth = (int)(iThumbHeight * myBitmap.Width) / myBitmap.Height;
                else if ((iThumbHeight == 0) && (iThumbWidth != 0))
                    iThumbHeight = (int)(iThumbWidth * myBitmap.Height) / myBitmap.Width;

                Image myThumbnail = myBitmap.GetThumbnailImage(iThumbWidth, iThumbHeight, myCallback, IntPtr.Zero);
                myThumbnail.Save(newPath + "Thumb_" + fileInfo.Name, ImageFormat.Jpeg);
                myBitmap.Dispose();
                myThumbnail.Dispose();
                GC.Collect();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool SetThumbnailGame110(string filePath, string newPath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return false;
            try
            {
                if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap = new Bitmap(fileInfo.FullName);

                if ((iThumbHeight == 0) && (iThumbWidth == 0)) return false;
                else if ((iThumbHeight != 0) && (iThumbWidth == 0))
                    iThumbWidth = (int)(iThumbHeight * myBitmap.Width) / myBitmap.Height;
                else if ((iThumbHeight == 0) && (iThumbWidth != 0))
                    iThumbHeight = (int)(iThumbWidth * myBitmap.Height) / myBitmap.Width;

                Image myThumbnail = myBitmap.GetThumbnailImage(iThumbWidth, iThumbHeight, myCallback, IntPtr.Zero);
                myThumbnail.Save(newPath + "Thumb_110_" + fileInfo.Name, ImageFormat.Jpeg);
                myBitmap.Dispose();
                myThumbnail.Dispose();
                GC.Collect();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool SetThumbnailGame(string filePath, string newPath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return false;
            try
            {
                if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap = new Bitmap(fileInfo.FullName);

                if ((iThumbHeight == 0) && (iThumbWidth == 0)) return false;
                else if ((iThumbHeight != 0) && (iThumbWidth == 0))
                    iThumbWidth = (int)(iThumbHeight * myBitmap.Width) / myBitmap.Height;
                else if ((iThumbHeight == 0) && (iThumbWidth != 0))
                    iThumbHeight = (int)(iThumbWidth * myBitmap.Height) / myBitmap.Width;

                Image myThumbnail = myBitmap.GetThumbnailImage(iThumbWidth, iThumbHeight, myCallback, IntPtr.Zero);
                myThumbnail.Save(newPath + "Thumb_" + fileInfo.Name, ImageFormat.Jpeg);
                myBitmap.Dispose();
                myThumbnail.Dispose();
                GC.Collect();
            }
            catch
            {
                return false;
            }
            return true;
        }
        #region get and set Thumbnail with Gallery
        public static bool SetAvatarGalleryThumbnail(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
            if (!fileInfo.Exists) return false;
            SetThumbnail(HttpContext.Current.Server.MapPath(filePath), fileInfo.Directory + "\\AvatarThumbnail\\", 75, 0);
            return SetThumbnailList(HttpContext.Current.Server.MapPath(filePath), fileInfo.Directory + "\\Avatar\\", fileInfo.Directory.ToString() + "\\AvatarThumbnail\\", iThumbWidth, iThumbWidth);
            //else
            //    return false;
        }

        public static string GetGalleryAvatar(string avatar)
        {
            int splitIndex = avatar.LastIndexOf("/");
            if (splitIndex != -1)
                return avatar.Substring(0, splitIndex) + "/Avatar" + avatar.Substring(splitIndex, avatar.Length - splitIndex);
            else return string.Empty;

        }
        #endregion

        #region Get and set thumbnail with Event
        public static bool SetAvatarEventThumbnail(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return false;
            return SetThumbnail(filePath, fileInfo.Directory + "\\AvatarTemp\\", iThumbWidth, iThumbHeight);
        }
        public static bool SetAvatarEventThumbnailList(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
                return false;
            string FileAvatar = fileInfo.Directory + "\\AvatarTemp\\" + fileInfo.Name;
            FileInfo fileInfoAvatar = new FileInfo(FileAvatar);
            Bitmap myBitmap = new Bitmap(fileInfoAvatar.FullName);

            if (iThumbWidth > myBitmap.Height)
                return SetThumbnailList(filePath, fileInfo.Directory + "\\ListAvatar\\", fileInfo.Directory + "\\AvatarTemp\\", iThumbWidth, myBitmap.Height);
            else
                return SetThumbnailList(filePath, fileInfo.Directory + "\\ListAvatar\\", fileInfo.Directory + "\\AvatarTemp\\", iThumbWidth, iThumbHeight);

        }
        
        public static string GetEventAvatar(string avatar)
        {
            int splitIndex = avatar.LastIndexOf("/");
            if (splitIndex != -1)
                return avatar.Substring(0, splitIndex) + "/Avatar" + avatar.Substring(splitIndex, avatar.Length - splitIndex);
            else return string.Empty;

        }
        #endregion

        #region get and set Thumbnail with Picture
        public static bool SetAvatarPictureThumbnail(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
            if (!fileInfo.Exists) return false;
            SetThumbnail(HttpContext.Current.Server.MapPath(filePath), fileInfo.Directory + "\\Avatar\\", 75, 0);
            return SetThumbnailList(HttpContext.Current.Server.MapPath(filePath), fileInfo.Directory + "\\ListAvatar\\", fileInfo.Directory.ToString() + "\\Avatar\\", iThumbWidth, iThumbWidth);
            //else
            //    return false;
        }

        public static string GetPictureAvatar(string avatar)
        {
            int splitIndex = avatar.LastIndexOf("/");
            if (splitIndex != -1)
                return avatar.Substring(0, splitIndex) + "/Avatar" + avatar.Substring(splitIndex, avatar.Length - splitIndex);
            else return string.Empty;

        }
        #endregion

        #region get and set Thumbnail Music Album
        public static bool SetAvatarAlbumThumbnail(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
            if (!fileInfo.Exists) return false;
            return SetThumbnail(HttpContext.Current.Server.MapPath(filePath), fileInfo.Directory + "\\Avatar\\", iThumbWidth, 0);
            
        }
        public static bool SetAlbumAvatarListThumbnail(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
            if (!fileInfo.Exists)
                return false;
            return SetThumbnail(HttpContext.Current.Server.MapPath(filePath), fileInfo.Directory + "\\ListAvatar\\", iThumbWidth, 0);
        }
        public static string GetAlbumAvatar(string avatar)
        {
            int splitIndex = avatar.LastIndexOf("/");
            if (splitIndex != -1)
                return avatar.Substring(0, splitIndex) + "/Avatar" + avatar.Substring(splitIndex, avatar.Length - splitIndex);
            else return string.Empty;

        }
        #endregion

        public static bool SetAvatarThumbnailWallpaper(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return false;
            return SetThumbnailWallpaper(filePath, fileInfo.Directory + "\\", iThumbWidth, iThumbHeight);
        }
        public static bool SetAvatarThumbnailGame(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return false;
            return SetThumbnailGame(filePath, fileInfo.Directory + "\\", iThumbWidth, iThumbHeight);
        }
        public static bool SetAvatarThumbnailGame110(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return false;
            return SetThumbnailGame110(filePath, fileInfo.Directory + "\\", iThumbWidth, iThumbHeight);
        }
		public static bool SetAvatarThumbnail(string filePath, int iThumbWidth, int iThumbHeight)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			if (!fileInfo.Exists) return false;
			return SetThumbnail(filePath, fileInfo.Directory + "\\Avatar\\", iThumbWidth, iThumbHeight);
		}
        public static bool SetThumbnailList(string filePath, string newPath, string avatarPath, int width, int height)
        {
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);
            FileInfo fileInfo = new FileInfo(filePath);
            Image orImgeCrop = Crop(Image.FromFile(avatarPath + fileInfo.Name), width, height);
            orImgeCrop.Save(newPath + fileInfo.Name);
            orImgeCrop.Dispose();
            GC.Collect();
            return true;
        }
        public static bool SetAvatarListThumbnail(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
                return false;
            return SetThumbnailList(filePath, fileInfo.Directory + "\\ListAvatar\\", fileInfo.Directory + "\\Avatar\\", iThumbWidth, iThumbHeight);
        }
        public static string GetAvatarWallpaper(string avatar)
        {
            int splitIndex = avatar.LastIndexOf("/");
            if (splitIndex != 0)
                return avatar.Substring(0, splitIndex) + "/" + "thumb_" + avatar.Substring(splitIndex, avatar.Length - splitIndex).Replace("/","");
            else return string.Empty;

        }
		public static string GetAvatar(string avatar)
		{
			int splitIndex = avatar.LastIndexOf("/");
			if (splitIndex != 0)
				return avatar.Substring(0, splitIndex) + "/Avatar/" + avatar.Substring(splitIndex, avatar.Length - splitIndex);
			else return string.Empty;

		}
		public static string strInitImage(string image, int width, int height)
		{
			string retVal = "<img src=\"" + image + "\"";
			if (width > 0) retVal += " width=\"" + width + "px\" ";
			if (height > 0) retVal += " height=\"" + height + "px\" ";
			retVal += ">";
			return retVal;
		}
        public static string strInitFlashWithActiveContent(string fileName, int width, int height, string _align)
        {
            string retVal = "<script type=\"text/javascript\">";
            retVal += "AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0','width','" + width + "','height','" + height + "','id','" + fileName + "','align','" + _align + "','src','" + fileName + "','quality','high','bgcolor','','name','" + fileName + "','allowscriptaccess','sameDomain','pluginspage','http://www.macromedia.com/go/getflashplayer','movie','" + fileName + "' );";
            retVal += "</script>";
            return retVal;
        }
        public static string strInitFlash(string flashURL, int width, int height)
        {
            string retVal = "<EMBED align=baseline src='" + flashURL + "'";
            if (width != 0) retVal += " width=" + width;
            if (height != 0) retVal += " height=" + height;

            retVal += " type=audio/x-pn-realaudio-plugin autostart=\"true\" controls=\"ControlPanel\" console=\"Clip1\" border=\"0\">";
            return retVal;
        }

		

		public static string strInitMultimedia(string mediaPath, int width, int height)
		{
			string retVal = "<EMBED pluginspage='http://www.microsoft.com/Windows/Downloads/Contents/Products/MediaPlayer/' ";
			if (width != 0) retVal += " width=" + width;
			if (height != 0) retVal += " height=" + height;

			retVal += " src='" + mediaPath + "' type='application/x-mplayer2' ShowStatusBar='1' AutoStart='true' ShowControls='1'></embed>";
			return retVal;
		}

        #region Get and set thumbnail with Clip
        public static bool SetAvatarClipThumbnail(string filePath, int iThumbWidth, int iThumbHeight)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return false;
            return SetThumbnail(filePath, fileInfo.Directory + "\\ListAvatar\\", iThumbWidth, iThumbHeight);
        }        

        public static string GetClipAvatar(string avatar)
        {
            int splitIndex = avatar.LastIndexOf("/");
            if (splitIndex != -1)
                return avatar.Substring(0, splitIndex) + "/Avatar" + avatar.Substring(splitIndex, avatar.Length - splitIndex);
            else return string.Empty;

        }
        #endregion
	}
}

#line default
#line hidden
