using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Domain;
using Web.Repositories.Interface;

namespace Web.Controllers
{
    [Route("Orders")]
    public class OrderController : ControllerBase
    {
        #region Properties

        private readonly IUnitOfWork _uowRepo;

        #endregion

        #region Constructor

        public OrderController(IUnitOfWork uowRepo)
        {
            _uowRepo = uowRepo;
        }

        #endregion

        /// <summary>
        /// 取得訂單全部資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<string> Orders()
        {
            IEnumerable<Order> orders;

            using (var uow = _uowRepo)
            {
                orders = await uow.Order.GetAllAsync();
            }

            return JsonConvert.SerializeObject(orders);
        }

        /// <summary>
        /// 依據ID取得訂單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<string> Order(string id)
        {
            Order order;

            using (var uow = _uowRepo)
            {
                order = await uow.Order.FindAsync(id);
            }

            return JsonConvert.SerializeObject(order);
        }

        /// <summary>
        /// 刪除訂單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            using (var uow = _uowRepo)
            {
                await uow.Order.DeleteAsync(id);
                
                // 儲存變更
                uow.SaveChanges();
            }

            return Ok();
        }

        /// <summary>
        /// 新增訂單資料
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertOrder([FromBody]Order order)
        {
            using (var uow = _uowRepo)
            {
                await uow.Order.InsertAsync(order);

                // 儲存變更
                uow.SaveChanges();
            }

            return Ok();
        }

        /// <summary>
        /// 更新訂單資料
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOrder([FromBody]Order order)
        {
            using (var uow = _uowRepo)
            {
                await uow.Order.UpdateAsync(order);

                // 儲存變更
                uow.SaveChanges();
            }

            return Ok();
        }
    }
}
