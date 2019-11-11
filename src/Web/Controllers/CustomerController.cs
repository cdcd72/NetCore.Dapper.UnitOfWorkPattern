using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Domain;
using Web.Repositories.Interface;

namespace Web.Controllers
{
    [Route("Customers")]
    public class CustomerController : ControllerBase
    {
        #region Properties

        private readonly IUnitOfWork _uowRepo;

        #endregion

        #region Constructor

        public CustomerController(IUnitOfWork uowRepo)
        {
            _uowRepo = uowRepo;
        }

        #endregion

        /// <summary>
        /// 取得全部客戶資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<string> Customers()
        {
            IEnumerable<Customer> customers;

            using (var uow = _uowRepo)
            {
                customers = await uow.Customer.GetAllAsync();
            }

            return JsonConvert.SerializeObject(customers);
        }

        /// <summary>
        /// 依據ID取得客戶資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<string> Customer(string id)
        {
            Customer customer;

            using (var uow = _uowRepo)
            {
                customer = await uow.Customer.FindAsync(id);
            }

            return JsonConvert.SerializeObject(customer);
        }

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            using (var uow = _uowRepo)
            {
                await uow.Customer.DeleteAsync(id);

                // 儲存變更
                uow.SaveChanges();
            }

            return Ok();
        }

        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertCustomer([FromBody]Customer customer)
        {
            using (var uow = _uowRepo)
            {
                await uow.Customer.InsertAsync(customer);

                // 儲存變更
                uow.SaveChanges();
            }

            return Ok();
        }

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCustomer([FromBody]Customer customer)
        {
            using (var uow = _uowRepo)
            {
                await uow.Customer.UpdateAsync(customer);

                // 儲存變更
                uow.SaveChanges();
            }

            return Ok();
        }
    }
}
