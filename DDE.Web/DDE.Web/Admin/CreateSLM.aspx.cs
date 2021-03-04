using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class CreateSLM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 91) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 92))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateSySession(ddlistSS);
                    PopulateDDList.populateCourses(ddlistCourse);
                   

                    if (Request.QueryString["SLMID"] != null)
                    {
                        if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 92))
                        {
                            populateSLMRecord(Request.QueryString["SLMID"]);
                            populateCourseDetails();                          
                            btnSubmit.Text = "Update";
                        }
                    }
                    else
                    {
                        if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 91))
                        {
                            btnSubmit.Text = "Create";
                        }
                    }
                  
                    pnlData.Visible = true;
                    pnlMSG.Visible = false;

                }

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

        private void populateCourseDetails()
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMLRID");
            DataColumn dtcol3 = new DataColumn("CID");
            DataColumn dtcol4 = new DataColumn("CName");
            DataColumn dtcol5 = new DataColumn("Year");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);



            int i = 1;


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESLMLinking where SLMID='" + Request.QueryString["SLMID"] + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                DataRow drow = dt.NewRow();

                drow["SNo"] = i;
                drow["SLMLRID"] = Convert.ToInt32(dr["SLMLRID"]);
                drow["CID"] = Convert.ToString(dr["CID"]);
                drow["CName"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["CID"]));
                drow["Year"] = Convert.ToString(dr["Year"]);

                dt.Rows.Add(drow);

                i = i + 1;
            }

            con.Close();
         

            dtlistCourse.DataSource = dt;
            dtlistCourse.DataBind();
        }

        private void populateSLMRecord(string slmid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESLMMaster where SLMID='" + slmid+ "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            
            lblSLMCode.Text=tbSLMCode.Text = dr["SLMCode"].ToString();
            rblLang.SelectedItem.Selected = false;
            rblLang.Items.FindByValue(dr["Lang"].ToString()).Selected = true;

            ddlistSS.SelectedItem.Selected = false;
            ddlistSS.Items.FindByText(dr["SyllabusSession"].ToString()).Selected = true;

            tbTitle.Text = dr["Title"].ToString();
                
            tbCost.Text = dr["Cost"].ToString();
                
            con.Close();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlistSS.SelectedItem.Value != "0"&& ddlistCourse.SelectedItem.Value != "0" && ddlistCourse.SelectedItem.Value != "0")
            {
                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("SLMLRID");
                DataColumn dtcol3 = new DataColumn("CID");
                DataColumn dtcol4 = new DataColumn("CName");
                DataColumn dtcol5 = new DataColumn("Year");


                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);


                int i = 1;



                foreach (DataListItem dli in dtlistCourse.Items)
                {
                    Label cid = (Label)dli.FindControl("lblCID");
                    Label cn = (Label)dli.FindControl("lblCName");
                    Label yr = (Label)dli.FindControl("lblYear");



                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i;
                    drow["SLMLRID"] = 0;
                    drow["CID"] = Convert.ToString(cid.Text);
                    drow["CName"] = Convert.ToString(cn.Text);
                    drow["Year"] = Convert.ToString(yr.Text);

                    dt.Rows.Add(drow);

                    i = i + 1;

                }
                if (dt.Rows.Count > 0)
                {
                    if (!(dt.AsEnumerable().Where(c => c.Field<string>("CID").Equals(ddlistCourse.SelectedItem.Value)).Count() > 0))
                    {
                        DataRow drow1 = dt.NewRow();

                        drow1["SNo"] = i;
                        drow1["SLMLRID"] = 0;
                        drow1["CID"] = Convert.ToString(ddlistCourse.SelectedItem.Value);
                        drow1["CName"] = Convert.ToString(ddlistCourse.SelectedItem.Text);
                        drow1["Year"] = Convert.ToString(ddlistYear.SelectedItem.Value);
                        dt.Rows.Add(drow1);

                    }

                }
                else
                {
                    DataRow drow1 = dt.NewRow();

                    drow1["SNo"] = i;
                    drow1["SLMLRID"] = 0;
                    drow1["CID"] = Convert.ToString(ddlistCourse.SelectedItem.Value);
                    drow1["CName"] = Convert.ToString(ddlistCourse.SelectedItem.Text);
                    drow1["Year"] = Convert.ToString(ddlistYear.SelectedItem.Value);
                    dt.Rows.Add(drow1);

                }


                dtlistCourse.DataSource = dt;
                dtlistCourse.DataBind();


                if (!(dtlistCourse.Visible))
                {
                    dtlistCourse.Visible = true;
                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Please select Course and Year.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Create")
            {

                if (dtlistCourse.Items.Count > 0)
                {
                    if (!(FindInfo.isSLMExist(tbSLMCode.Text)))
                    {

                        object oslmid = 0;

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into DDESLMMaster OUTPUT INSERTED.SLMID values(@SLMCode,@Dual,@GroupID,@Lang,@Title,@Cost,@PresentStock,@SyllabusSession)", con);

                        cmd.Parameters.AddWithValue("@SLMCode", tbSLMCode.Text.ToUpper());
                        cmd.Parameters.AddWithValue("@Dual","False");
                        cmd.Parameters.AddWithValue("@GroupID", 0);
                        cmd.Parameters.AddWithValue("@Lang", rblLang.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@Title", tbTitle.Text);
                        cmd.Parameters.AddWithValue("@Cost", tbCost.Text);                   
                        cmd.Parameters.AddWithValue("@PresentStock", 0);
                        cmd.Parameters.AddWithValue("@SyllabusSession", ddlistSS.SelectedItem.Text);

                        con.Open();
                        oslmid=cmd.ExecuteScalar();
                        con.Close();

                        int slmid = Convert.ToInt32(oslmid);

                        if (slmid != 0)
                        {
                            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand command = new SqlCommand();
                            SqlDataAdapter adapter = new SqlDataAdapter();
                            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                            DataSet dataset = new DataSet();


                            command.Connection = connection;
                            command.CommandText = "SELECT * FROM DDESLMLinking";
                            adapter.SelectCommand = command;
                            adapter.Fill(dataset, "SLMLinkingRecord");

                            int counter = 0;

                            foreach (DataListItem dli in dtlistCourse.Items)
                            {
                                Label cid = (Label)dli.FindControl("lblCID");

                                Label year = (Label)dli.FindControl("lblYear");

                                DataRow row = dataset.Tables["SLMLinkingRecord"].NewRow();

                                row["SLMID"] = slmid.ToString();

                                row["CID"] = Convert.ToInt32(cid.Text);
                                row["Year"] = Convert.ToInt32(year.Text);



                                dataset.Tables["SLMLinkingRecord"].Rows.Add(row);

                                counter = counter + 1;

                            }

                            try
                            {
                                int result = adapter.Update(dataset, "SLMLinkingRecord");
                                if (result == counter)
                                {
                                    pnlData.Visible = false;
                                    lblMSG.Text = "SLM has been created successfully !!";
                                    pnlMSG.Visible = true;
                                }

                            }
                            catch (SqlException ex)
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = ex.Message;
                                pnlMSG.Visible = true;
                            }
                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! This SLM could not be created.";
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;
                        }
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! This SLM Code is already exist.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }

                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Please add courses on this SLM.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
                   
                
              
            }
            else if (btnSubmit.Text == "Update")
            {
                if (dtlistCourse.Items.Count > 0)
                {
                    if (tbSLMCode.Text != lblSLMCode.Text)
                    {
                        if (!(FindInfo.isSLMExist(tbSLMCode.Text)))
                        {

                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("update DDESLMMaster set SLMCode=@SLMCode,Lang=@Lang,Title=@Title,Cost=@Cost where SLMID='" + Request.QueryString["SLMID"] + "' ", con);


                            cmd.Parameters.AddWithValue("@SLMCode", tbSLMCode.Text);
                            if (rblLang.SelectedItem.Value == "E")
                            {
                                cmd.Parameters.AddWithValue("@Lang", rblLang.SelectedItem.Value);

                            }
                            else if (rblLang.SelectedItem.Value == "H")
                            {
                                cmd.Parameters.AddWithValue("@Lang", rblLang.SelectedItem.Value);

                            }
                            cmd.Parameters.AddWithValue("@Title", tbTitle.Text);
                            cmd.Parameters.AddWithValue("@Cost", tbCost.Text);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            string error;
                            updateLinkingRecord(Convert.ToInt32(Request.QueryString["SLMID"]), out error);

                            if (error == "")
                            {

                                Log.createLogNow("Update", "Updated Record of SLM Code '" + tbSLMCode.Text, Convert.ToInt32(Session["ERID"].ToString()));

                                pnlData.Visible = false;
                                lblMSG.Text = "Record has been updated successfully";
                                pnlMSG.Visible = true;
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! This SLM Record could not be updated.Please contact ERP Developer.";
                                pnlMSG.Visible = true;

                            }
                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! This SLM Code is already exist.";
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;
                        }
                    }
                    else
                    {
                        

                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("update DDESLMMaster set Lang=@Lang,Title=@Title,Cost=@Cost where SLMID='" + Request.QueryString["SLMID"] + "' ", con);
                     
                            if (rblLang.SelectedItem.Value == "E")
                            {
                                cmd.Parameters.AddWithValue("@Lang", rblLang.SelectedItem.Value);

                            }
                            else if (rblLang.SelectedItem.Value == "H")
                            {
                                cmd.Parameters.AddWithValue("@Lang", rblLang.SelectedItem.Value);

                            }
                            cmd.Parameters.AddWithValue("@Title", tbTitle.Text);
                            cmd.Parameters.AddWithValue("@Cost", tbCost.Text);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            string error;
                            updateLinkingRecord(Convert.ToInt32(Request.QueryString["SLMID"]), out error);

                            if (error == "")
                            {
                                Log.createLogNow("Update", "Updated Record of SLM Code '" + tbSLMCode.Text, Convert.ToInt32(Session["ERID"].ToString()));

                                pnlData.Visible = false;
                                lblMSG.Text = "Record has been updated successfully";
                                pnlMSG.Visible = true;
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! This SLM Record could not be updated.Please contact ERP Developer.";
                                pnlMSG.Visible = true;

                            }
                       
                    }
                  }                     

                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Please add courses on this SLM.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
           

        }

        private void updateLinkingRecord(int slmid, out string error)
        {
            error = "";
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet dataset = new DataSet();


            command.Connection = connection;
            command.CommandText = "SELECT * FROM DDESLMLinking";
            adapter.SelectCommand = command;
            adapter.Fill(dataset, "SLMLinkingRecord");

            int counter = 0;

            foreach (DataListItem dli in dtlistCourse.Items)
            {
                Label cid = (Label)dli.FindControl("lblCID");

                Label year = (Label)dli.FindControl("lblYear");

                if (!(SLM.isSLMLinkingRecordExist(slmid, Convert.ToInt32(cid.Text), Convert.ToInt32(year.Text))))
                {

                    DataRow row = dataset.Tables["SLMLinkingRecord"].NewRow();

                    row["SLMID"] = slmid.ToString();

                    row["CID"] = Convert.ToInt32(cid.Text);
                    row["Year"] = Convert.ToInt32(year.Text);



                    dataset.Tables["SLMLinkingRecord"].Rows.Add(row);

                    counter = counter + 1;
                }

            }

            try
            {
                int result = adapter.Update(dataset, "SLMLinkingRecord");
                if (result == counter)
                {
                    error = "";
                }
                else
                {
                    error = "Sorry!! SLM Record could not be updated accurately.Please contact ERP Developer.";

                }

            }
            catch (SqlException ex)
            {
                error = ex.Message;
                
            }
        }

       

       

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateSLM.aspx");
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }

        protected void dtlistCourse_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMLRID");
            DataColumn dtcol3 = new DataColumn("CID");
            DataColumn dtcol4 = new DataColumn("CName");
            DataColumn dtcol5 = new DataColumn("Year");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);

            int i = 1;



            foreach (DataListItem dli in dtlistCourse.Items)
            {
                Label slmlrid = (Label)dli.FindControl("lblSLMLRID");
                Label cid = (Label)dli.FindControl("lblCID");
                Label cn = (Label)dli.FindControl("lblCName");
                Label yr = (Label)dli.FindControl("lblYear");


                DataRow drow = dt.NewRow();

                drow["SNo"] = i;
                drow["SLMLRID"] = Convert.ToString(slmlrid.Text);
                drow["CID"] = Convert.ToString(cid.Text);
                drow["CName"] = Convert.ToString(cn.Text);
                drow["Year"] = Convert.ToString(yr.Text);
                dt.Rows.Add(drow);

                i = i + 1;

            }

            int index = Convert.ToInt32(e.Item.ItemIndex);


            dt.Rows[index].Delete();
            dt.AcceptChanges();


            dtlistCourse.EditItemIndex = -1;
            dtlistCourse.DataSource = dt;
            dtlistCourse.DataBind();

            if (Request.QueryString["SLMID"] != null && btnSubmit.Text=="Update")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDESLMLinking where SLMLRID ='" + Convert.ToString(e.CommandArgument) + "'", con);

                
                con.Open();
                cmd.ExecuteReader();
                con.Close();
                
            }

            if (dtlistCourse.Items.Count == 0)
            {
                dtlistCourse.Visible = false;
            }
        }

       
    }
}