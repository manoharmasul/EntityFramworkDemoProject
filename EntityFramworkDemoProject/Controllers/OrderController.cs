using EntityFramworkDemoProject.Model;
using EntityFramworkDemoProject.Models;
using EntityFramworkDemoProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using static EntityFramworkDemoProject.Model.BaseModel;

namespace EntityFramworkDemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderController> logger;
        IConfiguration configuration;
        public OrderController(IOrderRepository orderRepository, ILogger<OrderController> logger, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            this.logger = logger;
            this.configuration = configuration;
        }
        [HttpPost("PlaceNewOrder")]
        public async Task<IActionResult> PlaceNewOrder(OrderInsert order)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();
            try
            {
                var result = await _orderRepository.PlaceNewOrder(order);
                if (result > 0)
                {
                    var rtnmsg = string.Format($"Order Added Successfully with Name {order.CustomerId}");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"OrderController-PlaceNewOrder : Complete Adding Order"));
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseresponse.ResponseData = result;
                    return Ok(baseresponse);
                }
                else if(result==-3)
                {
                    var rtnmsg = string.Format($"Product Out Of Stok");
                    logger.LogInformation(rtnmsg);
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    return Ok(baseresponse);
                }
                else
                {
                    var rtnmsg = string.Format($"Error While Adding Order");
                    logger.LogInformation(rtnmsg);
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    return Ok(baseresponse);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                logger.LogInformation(rtnmsg);
                baseresponse.StatusMessage = rtnmsg;
                baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                return Ok(baseresponse);

            }
        }
        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrders()
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();
            try
            {
                var result = await _orderRepository.GetAllOrder();
                if (result.Count() == 0 || result.Count() > 0)
                {
                    var rtnmsg = string.Format($"All Order Records Fetch Successfully...!");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"OrderController-GetAllOrder : All Records Fetch Successfully.....! "));
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponse.ResponseData = result;
                    return Ok(baseResponse);

                }
                else
                {
                    string rtnmsg = string.Format($"Error While Fetching Order Records.....!");
                    logger.LogInformation(rtnmsg);
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    return Ok(baseResponse);

                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                logger.LogInformation(rtnmsg);
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                return Ok(baseResponse);

            }
        }
        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetOrdersById(long id)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();
            try
            {
                var result = await _orderRepository.GetOrderById(id);
                if (result!=null)
                {
                    var rtnmsg = string.Format($"All Order Records Fetch Successfully...!");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"OrderController-GetAllOrder : All Records Fetch Successfully.....! "));
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponse.ResponseData = result;
                    return Ok(baseResponse);

                }
                else
                {
                    string rtnmsg = string.Format($"Error While Fetching Order Records.....!");
                    logger.LogInformation(rtnmsg);
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    return Ok(baseResponse);

                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                logger.LogInformation(rtnmsg);
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                return Ok(baseResponse);

            }
        }
        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(DeleteObj deleteObj)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();
            try
            {
                var result = await _orderRepository.DeleteOrder(deleteObj);
                if (result > 0)
                {
                    var rtnmsg = string.Format($" Order Records Deleted Successfully...!");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"OrderController-DeleteOrder :  Records Deleted Successfully.....! "));
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponse.ResponseData = result;
                    return Ok(baseResponse);

                }
                else
                {
                    string rtnmsg = string.Format($"Error While Deleting Order Records.....!");
                    logger.LogInformation(rtnmsg);
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    return Ok(baseResponse);

                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                logger.LogInformation(rtnmsg);
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                return Ok(baseResponse);

            }
        }
        [HttpGet("GetAllOrdersJoin")]
        public async Task<IActionResult> GetAllOrdersJoin(string OrderStatus)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();
            try
            {
                var result = await _orderRepository.GetAllOrderJoinUser(OrderStatus);
                if (result.Count() > 0)
                {
                    var rtnmsg = string.Format($"All Order Records Fetch Successfully...!");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"OrderController-GetAllOrder : All Records Fetch Successfully.....! "));
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponse.ResponseData = result;
                    return Ok(baseResponse);

                }
                if (result.Count() == 0)
                {
                    var rtnmsg = string.Format($"No Order Records Found ...!");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"OrderController-GetAllOrder : No Order Records Found ...!"));
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponse.ResponseData = result;
                    return Ok(baseResponse);

                }
                else
                {
                    string rtnmsg = string.Format($"Error While Fetching Order Records.....!");
                    logger.LogInformation(rtnmsg);
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    return Ok(baseResponse);

                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                logger.LogInformation(rtnmsg);
                baseResponse.StatusMessage = rtnmsg;
                baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                return Ok(baseResponse);

            }
        }
        [HttpPut("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatus updateOrderStatus)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();

            try
            {
                var result = await _orderRepository.UpdateOrderStatus(updateOrderStatus);

                if(result>0)
                {
                    var rtnmsg = string.Format("Order Successfully Updated Successfully...!");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format("OrderController-UpdateOrderStatus : Order Successfully Updated"));
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.ResponseData = result;
                    baseresponse.StatusCode= StatusCodes.Status200OK.ToString();

                    return Ok(baseresponse);
                }
                else
                {
                    var rtnmsg = string.Format("Error While Updating Order");
                    logger.LogInformation(rtnmsg);
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.ResponseData = result;
                    baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                    return Ok(baseresponse);

                }

            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                logger.LogInformation(rtnmsg);
                baseresponse.StatusMessage = rtnmsg;
                baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                return Ok(baseresponse);

            }
            
        }
        [HttpGet("GetOrderByCustIdAndStatusDate")]
        public async Task<IActionResult> GetOrderByCustIdAndStatusDate(long custId, string? orderstatus, DateTime? date)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();
            try
            {
                var order=await _orderRepository.GetOrderByCustIdAndStatusDate(custId, orderstatus, date);
                if(order!=null)
                {
                    var rtnmsg = string.Format("Order Records Fetch Successfully");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug("OrderController-GetOrderByCustIdAndStatusDate : Order Records Fetch SuccessFully....! ");
                    baseresponse.StatusMessage= rtnmsg; 
                    baseresponse.StatusCode= StatusCodes.Status200OK.ToString();
                    baseresponse.ResponseData = order;

                    return Ok(baseresponse);
                }
                else
                {
                    var rtnmsg = string.Format("Error While Fetching Order Records...!");
                    logger.LogInformation(rtnmsg);
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.StatusCode=StatusCodes.Status409Conflict.ToString();
                    return Ok(baseresponse);

                }


            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                logger.LogInformation(rtnmsg);
                baseresponse.StatusMessage = rtnmsg;
                baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                return Ok(baseresponse);
            }
        }
    }
}
