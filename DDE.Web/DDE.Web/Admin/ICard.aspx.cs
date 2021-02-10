using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class ICard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 43))
            {
                if (!IsPostBack)
                {

                    populateICards();
                    populateStudentPhotos();

                }

            }

            
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

        private void populateStudentPhotos()
        {
            foreach (DataListItem dli in dtlistICards.Items)
            {
                Image stph = (Image)dli.FindControl("imgStudentPhoto");
                Label lblsrid = (Label)dli.FindControl("lblSRID");

                stph.ImageUrl = "StudentImgHandler.ashx?SRID=" + lblsrid.Text;
            }
        }

        private void populateICards()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEStudentRecord where SRID in ("+Session["Students"].ToString()+")",con);


            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SRID");
            DataColumn dtcol2 = new DataColumn("SCCode");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("ICardNo");
            //DataColumn dtcol5 = new DataColumn("StudentPhoto");
            DataColumn dtcol6 = new DataColumn("SName");
            DataColumn dtcol7 = new DataColumn("FName");
            DataColumn dtcol8 = new DataColumn("MName");
            DataColumn dtcol9 = new DataColumn("Batch");
            DataColumn dtcol10 = new DataColumn("Course");
            DataColumn dtcol11 = new DataColumn("Year");
            DataColumn dtcol12 = new DataColumn("Gender");
            DataColumn dtcol13 = new DataColumn("Category");
            DataColumn dtcol14 = new DataColumn("Address");
            DataColumn dtcol15 = new DataColumn("MNo");
            DataColumn dtcol16 = new DataColumn("Email");
          


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            //dt.Columns.Add(dtcol5);
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
         
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.SelectCommand.Connection = con;
            da.Fill(ds);
           
            if (ds.Tables[0].Rows.Count > 0)
            {
               for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
               {
                   DataRow drow = dt.NewRow();
                  
                   drow["SRID"] = ds.Tables[0].Rows[j]["SRID"].ToString();
                   drow["SCCode"] = FindInfo.findSCCodeForAdmitCardBySRID(Convert.ToInt32(ds.Tables[0].Rows[j]["SRID"]));
                   drow["EnrollmentNo"] = Convert.ToString(ds.Tables[0].Rows[j]["EnrollmentNo"]);
                   drow["ICardNo"] = Convert.ToString(ds.Tables[0].Rows[j]["ICardNo"]);
                   //drow["StudentPhoto"] = "StudentPhotos/" + ds.Tables[0].Rows[j]["EnrollmentNo"].ToString() + ".jpg";
                   drow["SName"] = Convert.ToString(ds.Tables[0].Rows[j]["StudentName"]);
                   drow["FName"] = Convert.ToString(ds.Tables[0].Rows[j]["FatherName"]);
                   drow["MName"] = Convert.ToString(ds.Tables[0].Rows[j]["MotherName"]);
                   drow["Batch"] = Convert.ToString(ds.Tables[0].Rows[j]["Session"]);
                   drow["Year"] = FindInfo.findAlphaYear(ds.Tables[0].Rows[j]["CYear"].ToString()).ToUpper();
                   drow["Gender"] = (ds.Tables[0].Rows[j]["Gender"].ToString()).ToUpper();
                   drow["Category"] = (ds.Tables[0].Rows[j]["Category"].ToString()).ToUpper();
                   drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[j]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[j]["CYear"]));
                   drow["Address"] = Convert.ToString(ds.Tables[0].Rows[j]["CAddress"]);
                   drow["MNo"] = Convert.ToString(ds.Tables[0].Rows[j]["MobileNo"]);
                   drow["Email"] = Convert.ToString(ds.Tables[0].Rows[j]["Email"]);                
                   dt.Rows.Add(drow);
                  
                 
               }


            }

            dtlistICards.DataSource = dt;
            dtlistICards.DataBind();




            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlData.Visible = true;
                dtlistICards.Visible = true;
                pnlMSG.Visible = false;
                             
            }
            else
            {
                pnlData.Visible = false;
                dtlistICards.Visible = false;             
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }


        }
    }
}
