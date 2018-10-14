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
    class LoanApplication
    {
        #region "CONSTRUCTORS"
        public LoanApplication()
        {
     
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string LoanApplicationId { get; set; }
        public DateTime Date { get; set; }
        public string ApplicationStatus { get; set; }
        public int LoanCycle { get; set; }
        public string ClientId { get; set; }
        public string BranchId { get; set; }
        public string ZoneId { get; set; }
        public string CollectorId { get; set; }
        public string ProductId { get; set; }
        public string PaymentFrequency { get; set; }
        public int Terms { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal InterestRate { get; set; }
        public decimal ServiceFeeRate { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalAmountDue { get; set; }
        public decimal InstallmentAmountDue { get; set; }
        public decimal ServiceFeeAmount { get; set; }
        public decimal LoanReleaseAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal Penalty { get; set; }
        public decimal RunningBalance { get; set; }
        public decimal PastDueAmount { get; set; }
        public decimal AdvancePayment { get; set; }
        public decimal TotalDaysPastDue { get; set; }
        public string PreparedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime DateApproved { get; set; }
        public string DisapprovedBy { get; set; }
        public DateTime DateDisapproved { get; set; }
        public string DisapprovedReason { get; set; }
        public string DisbursedBy { get; set; }
        public DateTime DateDisbursed { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAllData(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getLoanApplications?pDisplayType=" + pDisplayType + "&pPrimaryKey=" + pPrimaryKey + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getLoanApplicationPastDueAccounts()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getLoanApplicationPastDueAccounts").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getLoanApplicationByClient(string pClientId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getLoanApplicationByClient?pClientId=" + pClientId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getLoanApplicationByClientLedger(string pClientId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getLoanApplicationByClientLedger?pClientId=" + pClientId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getLoanApplicationStatus(string pLoanApplicationId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getLoanApplicationStatus?pLoanApplicationId=" + pLoanApplicationId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getForReleaseSheet(DateTime pReleaseDate, string pCollectorId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getForReleaseSheet?pReleaseDate=" + pReleaseDate + "&pCollectorId=" + pCollectorId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getMonthlyProjectionByBranch(string pBranchId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getMonthlyProjectionByBranch?pBranchId=" + pBranchId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public string save(GlobalVariables.Operation pOperation)
        {
            string _Id = "";
            try
            {
                switch (pOperation)
                {
                    case GlobalVariables.Operation.Add:
                        HttpClient clientAdd = new HttpClient();
                        clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertLoanApplication/", this).Result;
                        _Id = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateLoanApplication/", this).Result;
                        _Id = responseEdit.Content.ReadAsStringAsync().Result;
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return _Id.Replace("\"", "");
        }

        public bool remove(string pId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeLoanApplication?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool approve(string pId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/approveLoanApplication?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool cancel(string pId, string pReason)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/cancelLoanApplication?pId=" + pId + "&pReason=" + pReason + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool post(string pId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/postLoanApplication?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
