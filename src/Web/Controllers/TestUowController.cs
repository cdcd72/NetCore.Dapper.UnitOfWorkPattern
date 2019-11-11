using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Web.Domain;
using Web.Repositories.Interface;

namespace Web.Controllers
{
    [Route("Uow")]
    public class TestUowController : Controller
    {
        #region Properties

        private readonly IUnitOfWork _uowRepo;

        #endregion

        #region Constructor

        public TestUowController(IUnitOfWork uowRepo)
        {
            _uowRepo = uowRepo;
        }

        #endregion

        /// <summary>
        /// 寫入後刪除(情境1)
        /// 結果：最後沒有新增客戶資料(沒有系統例外狀況下)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("DeleteAfterInserted_ScenarioOne")]
        public async Task<string> DeleteAfterInserted_ScenarioOne([FromBody]Customer customer)
        {
            int insertResult = 0,
                deleteResult = 0;

            using (var uow = _uowRepo)
            {
                // 測試新增客戶
                insertResult = await uow.Customer.InsertAsync(customer);

                // 測試刪除客戶
                deleteResult = await uow.Customer.DeleteAsync(customer.CustomerID);

                // 儲存變更
                uow.SaveChanges();
            }

            return JsonConvert.SerializeObject(new { INSERT = insertResult, DELETE = deleteResult });
        }

        /// <summary>
        /// 寫入後刪除(情境2)
        /// 結果：最後沒有新增客戶資料(有系統例外狀況下)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("DeleteAfterInserted_ScenarioTwo")]
        public async Task<string> DeleteAfterInserted_ScenarioTwo([FromBody]Customer customer)
        {
            int insertResult = 0,
                deleteResult = 0;

            using (var uow = _uowRepo)
            {
                // 測試新增客戶
                insertResult = await uow.Customer.InsertAsync(customer);

                throw new Exception("Some Exception Happened...");

                // 測試刪除客戶
                deleteResult = await uow.Customer.DeleteAsync(customer.CustomerID);

                // 儲存變更
                uow.SaveChanges();
            }

            return JsonConvert.SerializeObject(new { INSERT = insertResult, DELETE = deleteResult });
        }

        /// <summary>
        /// 寫入後刪除(情境3)
        /// 結果：最後有新增客戶資料(有系統例外狀況下)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("DeleteAfterInserted_ScenarioThree")]
        public async Task<string> DeleteAfterInserted_ScenarioThree([FromBody]Customer customer)
        {
            int insertResult = 0,
                deleteResult = 0;

            using (var uow = _uowRepo)
            {
                // 測試新增客戶
                insertResult = await uow.Customer.InsertAsync(customer);

                // 儲存變更
                uow.SaveChanges();

                throw new Exception("Some Exception Happened...");

                // 測試刪除客戶
                deleteResult = await uow.Customer.DeleteAsync(customer.CustomerID);

                // 儲存變更
                uow.SaveChanges();
            }

            return JsonConvert.SerializeObject(new { INSERT = insertResult, DELETE = deleteResult });
        }

        /// <summary>
        /// 寫入後刪除(情境4)
        /// 結果：最後有新增客戶資料(沒有系統例外狀況下)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("DeleteAfterInserted_ScenarioFour")]
        public async Task<string> DeleteAfterInserted_ScenarioFour([FromBody]Customer customer)
        {
            int insertResult = 0,
                deleteResult = 0;

            using (var uow = _uowRepo)
            {
                // 測試新增客戶
                insertResult = await uow.Customer.InsertAsync(customer);

                // 儲存變更
                uow.SaveChanges();

                // 測試刪除客戶
                deleteResult = await uow.Customer.DeleteAsync(customer.CustomerID);
            }

            return JsonConvert.SerializeObject(new { INSERT = insertResult, DELETE = deleteResult });
        }

        /// <summary>
        /// 寫入後刪除(情境5)
        /// 結果：最後沒有新增客戶資料(沒有系統例外狀況下)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("DeleteAfterInserted_ScenarioFive")]
        public async Task<string> DeleteAfterInserted_ScenarioFive([FromBody]Customer customer)
        {
            int insertResult = 0,
                deleteResult = 0;

            using (var uow = _uowRepo)
            {
                // 測試新增客戶
                insertResult = await uow.Customer.InsertAsync(customer);

                // 儲存變更
                uow.SaveChanges();

                // 測試刪除客戶
                deleteResult = await uow.Customer.DeleteAsync(customer.CustomerID);

                // 儲存變更
                uow.SaveChanges();
            }

            return JsonConvert.SerializeObject(new { INSERT = insertResult, DELETE = deleteResult });
        }
    }
}
