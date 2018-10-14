using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

using NSites_V.Global;
using System.Net.Http;

namespace NSites_V.ApplicationObjects.Classes.Lendings
{
    class LoanEndOfDayDetail
    {
        #region "CONSTRUCTORS"
        public LoanEndOfDayDetail()
        {
            
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string DetailId { get; set; }
        public string LoanEndOfDayId { get; set; }
        public string CollectorId { get; set; }
        public decimal TotalAmountDue { get; set; }
        public decimal TotalRunningBalance { get; set; }
        public decimal TotalCollection { get; set; }
        public decimal TotalVariance { get; set; }
        public decimal TotalLoanRelease { get; set; }
        public decimal TotalServiceFee { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getLoanEndOfDayDetails(string pLoanEndOfDayId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getLoanEndOfDayDetails?pLoanEndOfDayId=" + pLoanEndOfDayId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public bool save(GlobalVariables.Operation pOperation)
        {
            bool _result = false;
            try
            {
                switch (pOperation)
                {
                    case GlobalVariables.Operation.Add:
                        HttpClient clientAdd = new HttpClient();
                        clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertLoanEndOfDayDetail/", this).Result;
                        _result = bool.Parse(responseAdd.Content.ReadAsStringAsync().Result);
                        //string _results = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateLoanEndOfDayDetail/", this).Result;
                        _result = bool.Parse(responseEdit.Content.ReadAsStringAsync().Result);
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return _result;
        }

        public bool remove(string pDetailId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeLoanEndOfDayDetail?pDetailId=" + pDetailId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
