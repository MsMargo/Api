using MedicalApp.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace MedicalApp.Models
{
    public class DataTablesResult : JsonNetResult
    {
        public DataTablesResult(string sEcho, int iTotalRecords, int iTotalDisplayRecords, IEnumerable<object> aaData)
        {
            var dataTablesRespone = new DataTablesResponse();

            dataTablesRespone.sEcho = sEcho;
            dataTablesRespone.iTotalRecords = iTotalRecords;
            dataTablesRespone.iTotalDisplayRecords = iTotalDisplayRecords;
            dataTablesRespone.aaData = aaData;

            Data = dataTablesRespone;
        }

        public DataTablesResult(DataTablesRequest dataTable, int iTotalRecords, int iTotalDisplayRecords, IEnumerable<object> aaData)
            : this(dataTable.sEcho, iTotalRecords, iTotalDisplayRecords, aaData)
        {
        }

        public DataTablesResult(DataTablesRequest dataTable)
            : this(dataTable.sEcho, 0, 0, null)
        {
        }

        public DataTablesResult(DataTablesResponse response)
        {
            Data = response;
        }
    }

    public class DataTablesResponse
    {
        public DataTablesResponse()
        {
            sEcho = sEcho;
            iTotalRecords = iTotalRecords;
            iTotalDisplayRecords = iTotalDisplayRecords;
            aaData = aaData;
        }

        /// <summary>
        /// An unaltered copy of sEcho sent from the client side. 
        /// This parameter will change with each draw (it is basically a draw count) - 
        /// so it is important that this is implemented. Note that it strongly recommended 
        /// for security reasons that you 'cast' this parameter to an integer 
        /// in order to prevent Cross Site Scripting (XSS) attacks.
        /// </summary>        
        [DataMember]
        public string sEcho { get; set; }

        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        [DataMember]
        public int iTotalRecords { get; set; }

        /// <summary>
        /// Total records, after filtering (i.e. the total number of records after filtering has been applied - 
        /// not just the number of records being returned in this result set)
        /// </summary>
        [DataMember]
        public int iTotalDisplayRecords { get; set; }

        /// <summary>
        /// Optional - this is a string of column names, comma separated (used in combination with sName) 
        /// which will allow DataTables to reorder data on the client-side if required for display
        /// </summary>
        //[DataMember]
        //public string sColumns { get; set; }

        /// <summary>
        /// The data in a 2D array
        /// Fill this structure with the plain table data
        /// represented as string.
        /// </summary>
        [DataMember]
        public IEnumerable<object> aaData { get; set; }
        //public List<List<string>> aaData { get; set; }
    }

    public class JsonNetResult : JsonResult
    {
        public JsonNetResult(object data)
        {
            Data = data;
        }

        public JsonNetResult()
        {
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data == null)
            {
                return;
            }

            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new JsonNullStringContractResolver() // zamienia w stringach null na ""
            });
            response.Write(serializedObject);
        }
    }

}