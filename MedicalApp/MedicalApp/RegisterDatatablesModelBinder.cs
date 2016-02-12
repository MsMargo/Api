using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Mvc.JQuery.Datatables;
using Mvc.JQuery.DataTables;

[assembly: PreApplicationStartMethod(typeof(MedicalApp.RegisterDatatablesModelBinder), "Start")]

namespace MedicalApp {
    public static class RegisterDatatablesModelBinder {
        public static void Start() {
            if (!ModelBinders.Binders.ContainsKey(typeof(DataTablesParam)))
                ModelBinders.Binders.Add(typeof(DataTablesParam), new DataTablesModelBinder());
        }
    }
}
