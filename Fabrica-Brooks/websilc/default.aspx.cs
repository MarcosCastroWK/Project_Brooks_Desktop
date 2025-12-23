using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brooks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();

        while (enumerator.MoveNext())
        {
            HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
        }

    }
}