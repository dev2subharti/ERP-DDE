using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class ChalanForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateData();
            }

        }

        private void populateData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDECourse order by CourseShortName", con);
            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            DataColumn dtcol0 = new DataColumn("Heading");
            DataColumn dtcol1 = new DataColumn("SCName");
            DataColumn dtcol2 = new DataColumn("SCCode");
            DataColumn dtcol3 = new DataColumn("Place");
            DataColumn dtcol4 = new DataColumn("FH1");
            DataColumn dtcol5 = new DataColumn("FH2");
            DataColumn dtcol6 = new DataColumn("FH3");
            DataColumn dtcol7 = new DataColumn("FH4");
            DataColumn dtcol8 = new DataColumn("FH5");
            DataColumn dtcol9 = new DataColumn("FH6");
            DataColumn dtcol10 = new DataColumn("FH7");
            DataColumn dtcol11 = new DataColumn("FH8");
            DataColumn dtcol12 = new DataColumn("Amount1");
            DataColumn dtcol13 = new DataColumn("Amount2");
            DataColumn dtcol14 = new DataColumn("Amount3");
            DataColumn dtcol15 = new DataColumn("Amount4");
            DataColumn dtcol16 = new DataColumn("Amount5");
            DataColumn dtcol17 = new DataColumn("Amount6");
            DataColumn dtcol18 = new DataColumn("Amount7");
            DataColumn dtcol19 = new DataColumn("Amount8");
            DataColumn dtcol20 = new DataColumn("Total");
            DataColumn dtcol21 = new DataColumn("ChallanNo");
            DataColumn dtcol22 = new DataColumn("Date");

            dt.Columns.Add(dtcol0);
            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);
            dt.Columns.Add(dtcol11);
            dt.Columns.Add(dtcol12);
            dt.Columns.Add(dtcol13);
            dt.Columns.Add(dtcol14);
            dt.Columns.Add(dtcol15);
            dt.Columns.Add(dtcol16);
            dt.Columns.Add(dtcol17);
            dt.Columns.Add(dtcol18);
            dt.Columns.Add(dtcol19);
            dt.Columns.Add(dtcol20);
            dt.Columns.Add(dtcol21);
            dt.Columns.Add(dtcol22);

            string date=DateTime.Now.ToString("dd/MM/yyyy");

            Random rd = new Random();
            int cn = rd.Next();
            for (int i = 1; i <= 3; i++)
            {
                DataRow drow = dt.NewRow();

                if (i == 1)
                {
                    drow["Heading"] = "BANK COPY";
                }
                else  if (i == 2)
                {
                    drow["Heading"] = "UNIVERSITY COPY";
                }
                else if (i == 3)
                {
                    drow["Heading"] = "CENTRE COPY";
                }
                drow["Date"] = date;
                drow["ChallanNo"] = cn.ToString();
                drow["SCName"] = Session["SCName"].ToString();
                drow["SCCode"] = Session["SCCode"].ToString();
             
                drow["FH1"] = Session["FH1"].ToString();
                drow["FH2"] = Session["FH2"].ToString();
                drow["FH3"] = Session["FH3"].ToString();
                drow["FH4"] = Session["FH4"].ToString();
                drow["FH5"] = Session["FH5"].ToString();
                drow["FH6"] = Session["FH6"].ToString();
                drow["FH7"] = Session["FH7"].ToString();
                drow["FH8"] = Session["FH8"].ToString();

                drow["Amount1"] = Session["Amount1"].ToString();
                drow["Amount2"] = Session["Amount2"].ToString();
                drow["Amount3"] = Session["Amount3"].ToString();
                drow["Amount4"] = Session["Amount4"].ToString();
                drow["Amount5"] = Session["Amount5"].ToString();
                drow["Amount6"] = Session["Amount6"].ToString();
                drow["Amount7"] = Session["Amount7"].ToString();
                drow["Amount8"] = Session["Amount8"].ToString();

                drow["Total"] = getAmount(Session["Amount1"].ToString()) + getAmount(Session["Amount2"].ToString()) + getAmount(Session["Amount3"].ToString()) + getAmount(Session["Amount4"].ToString()) + getAmount(Session["Amount5"].ToString()) + getAmount(Session["Amount6"].ToString()) + getAmount(Session["Amount7"].ToString()) + getAmount(Session["Amount8"].ToString());
                dt.Rows.Add(drow);
              
            }
            

            dtlistChalanForm.DataSource = dt;
            dtlistChalanForm.DataBind();

            con.Close();
            
        }

        private int getAmount(string amount)
        {
            if (amount == "")
            {
                return 0;
            }

            else
            {
                return Convert.ToInt32(amount);
            }
        }
    }
}
