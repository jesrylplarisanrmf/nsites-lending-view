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
    class LoanApplicationDetail
    {
        #region "CONSTRUCTORS"
        public LoanApplicationDetail()
        {
            
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string DetailId { get; set; }
        public string LoanApplicationId { get; set; }
        public int SeqNo { get; set; }
        public DateTime Date { get; set; }
        public decimal AmountDue { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal RunningBalance { get; set; }
        public decimal NewBalance { get; set; }
        public decimal Variance { get; set; }
        public string PastDueReason { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getLoanApplicationDetails(string pLoanApplicationId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getLoanApplicationDetails?pLoanApplicationId=" + pLoanApplicationId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getLoanApplicationDetail(string pDetailId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getLoanApplicationDetail?pDetailId=" + pDetailId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getDailyCollectionSheet(DateTime pCollectionDate, string pCollectorId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getDailyCollectionSheet?pCollectionDate=" + pCollectionDate + "&pCollectorId=" + pCollectorId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getUploadCollectionList(DateTime pCollectionDate, string pCollectorId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getUploadCollectionList?pCollectionDate=" + pCollectionDate + "&pCollectorId=" + pCollectorId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getEODLoanApplicationDetail(DateTime pDate, string pBranchId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getEODLoanApplicationDetail?pDate=" + pDate + "&pBranchId=" + pBranchId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getEODLoanApplicationDetailList(DateTime pDate, string pBranchId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getEODLoanApplicationDetailList?pDate=" + pDate + "&pBranchId=" + pBranchId + "").Result;
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
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertLoanApplicationDetail/", this).Result;
                        _result = bool.Parse(responseAdd.Content.ReadAsStringAsync().Result);
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateLoanApplicationDetail/", this).Result;
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
                HttpResponseMessage response = client.GetAsync("api/main/removeLoanApplicationDetail?pDetailId=" + pDetailId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool updatePayment(string pDetailId, decimal pPayment, decimal pNewBalance, 
            decimal pVariance,string pPastDueReason,string pRemarks,string pCollectorId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/updatePayment?pDetailId=" + pDetailId + "&pPayment=" + pPayment +
                    "&pNewBalance=" + pNewBalance + "&pVariance=" + pVariance +
                    "&pPastDueReason=" + pPastDueReason + "&pRemarks=" + pRemarks + "&pCollectorId=" + pCollectorId + 
                    "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool updateEODLoanTransactionDetail(string pDetailId, string pEODId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/updateEODLoanTransactionDetail?pDetailId=" + pDetailId + "&pEODId=" + pEODId +
                    "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
