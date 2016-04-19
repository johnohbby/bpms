using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Interfaces;
using CodeProject.Business.Entities;
using CodeProject.Business.Common;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CodeProject.Data.EntityFramework
{
    /// <summary>
    /// Account Data Service
    /// </summary>
    public class FormDataService : EntityFrameworkService, IFormDataService
    {

        /// <summary>
        /// Update Workflow Type
        /// </summary>
        /// <param name="product"></param>
        public void UpdateForm(Form form)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("FormUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = form.Id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = form.Name;
                    cmd.Parameters.Add("@tableName", SqlDbType.VarChar).Value = form.TableName;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            
        }

        /// <summary>
        /// Create Workflow Type
        /// </summary>
        /// <param name="product"></param>
        public void CreateForm(Form form)
        {
            dbConnection.Forms.Add(form);
        }

        /// <summary>
        /// Get Workflow Type
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Form GetForm(long formID)
        {
            Form form = dbConnection.Forms.Where(c => c.Id == formID).FirstOrDefault();
            return form;
        }

   /// <summary>
        /// Get Workflow Types
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<Form> GetForms(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetForms", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<Form> workflows = dt.ToList<Form>().ToList<Form>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Workflows.Count();
            
            return workflows;
        }

        public List<Form> GetAllForms( int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetForms", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<Form> workflows = dt.ToList<Form>().ToList<Form>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Workflows.Count();

            return workflows;
        }

        /// <summary>
        /// Delete Workflow Type
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteForm(Form form)
        {
            dbConnection.Forms.Attach(form);
            dbConnection.Forms.Remove(form);
        }
        public List<CodeProject.Business.Entities.Form> GetFormByActionTypeId(long actionTypeId, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetFormByActionTypeId", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@actionTypeId", SqlDbType.BigInt, 255).Value = actionTypeId;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {


                        da.Fill(dt);
                    }
                }
            }
            List<Business.Entities.Form> forms = dt.ToList<Business.Entities.Form>().ToList<Business.Entities.Form>();
            totalRows = 0;


            totalRows = forms.Count();

            // = dbConnection.Workflows.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return forms;
        }


        public List<CodeProject.Business.Entities.FormField> GetFormFieldsByFormId(long formId, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetFormFieldsByFormId", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@formId", SqlDbType.BigInt, 255).Value = formId;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<Business.Entities.FormField> ff = dt.ToList<Business.Entities.FormField>().ToList<Business.Entities.FormField>();
            totalRows = 0;


            totalRows = ff.Count();

            // = dbConnection.Workflows.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return ff;
        }


        public void InsertData(long formId, string allValues, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertData", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@formId", SqlDbType.BigInt, 255).Value = formId;
                    cmd.Parameters.Add("@allValues", SqlDbType.NVarChar).Value = allValues;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<Business.Entities.FormField> ff = dt.ToList<Business.Entities.FormField>().ToList<Business.Entities.FormField>();
            totalRows = 0;


            totalRows = ff.Count();

            // = dbConnection.Workflows.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

          
        }
    }




}


