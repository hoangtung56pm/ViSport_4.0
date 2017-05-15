using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppboxApi.Library;

namespace AppboxApi
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Test_Click(object sender, EventArgs e)
        {
            ApiController.MtApi("84902201287", "Mat khau", "Moi Quy khach truy cap [link tải game] de tai cac game hap dan cua dich vu Appbox. Mat khau de su dung dich vu cua Quy khach la: [Mật khẩu]. Chi tiet: 19001255", "0", 0, 0);
        }
    }
}