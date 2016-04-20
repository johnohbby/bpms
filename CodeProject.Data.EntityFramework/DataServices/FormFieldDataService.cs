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
    /// FormField Field Service
    /// </summary>
    public class FormFieldFieldDataService : EntityFrameworkService, IFormFieldDataService
    {

        /// <summary>
        /// Update Workflow Type
        /// </summary>
        /// <param name="product"></param>
        public void UpdateFormField(FormField formField)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("FormFieldUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = formField.Id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = formField.Name;
                    cmd.Parameters.Add("@formId", SqlDbType.BigInt).Value = formField.FormId;
                    cmd.Parameters.Add("@fieldType", SqlDbType.VarChar).Value = formField.FieldType;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            
        }

        /// <summary>
        /// Create From Field
        /// </summary>
        /// <param name="product"></param>
        public void CreateFormField(FormField formField)
        {
            dbConnection.FormFields.Add(formField);
        }

        /// <summary>
        /// Get Form Field
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public FormField GetFormField(long formFieldID)
        {
            FormField formField = dbConnection.FormFields.Where(c => c.Id == formFieldID).FirstOrDefault();
            return formField;
        }

   /// <summary>
        /// Get Form Fields
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<FormField> GetFormFields(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<FormField> formFields = dbConnection.FormFields.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return formFields;
        }

        
        /// <summary>
        /// Delete From Field
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteFormField(FormField formField)
        {
            dbConnection.FormFields.Attach(formField);
            dbConnection.FormFields.Remove(formField);
        }


        /// Get Form Fields For Form
        /// </summary>
        /// <param name="customer"></param>
        public List<FormField> GetFormFieldsForForm(long formId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetFormFieldsForForm", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@formId", SqlDbType.BigInt).Value = formId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<FormField> formFields = dt.ToList<FormField>().ToList<FormField>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.StatusTranslations.Count();

            return formFields;
        }
    }




}


