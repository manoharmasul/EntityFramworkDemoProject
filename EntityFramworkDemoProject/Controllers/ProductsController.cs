using EntityFramworkDemoProject.Model;
using EntityFramworkDemoProject.Models;
using EntityFramworkDemoProject.Repository;
using EntityFramworkDemoProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EntityFramworkDemoProject.Model.BaseModel;

namespace EntityFramworkDemoProject.Controllers
{
    [Route("EntityFramworkDemoProject/User")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductAsyncRepository _productAsyncRepository;   
        private readonly ILogger<ProductsController> logger;
        IConfiguration configuration;
        public ProductsController(IProductAsyncRepository productAsyncRepository, ILogger<ProductsController> logger, IConfiguration configuration)
        {
            _productAsyncRepository = productAsyncRepository;
            this.logger = logger;
            this.configuration = configuration;
        }
        [HttpPost("AddProducts")]
        public async Task<IActionResult> AddProducts(ProductsInsert prod)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();
            try
            {
                var result = await _productAsyncRepository.AddNewProducts(prod);
                 if(result>0)
                 {
                    var rtnmsg = string.Format($"Product Added Successfully with Name {prod.ProductName}");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"ProductsController-AddNewProducts : Complete Adding Product"));
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseresponse.ResponseData = result;
                    return Ok(baseresponse);
                }
                 else
                {
                    var rtnmsg = string.Format($"Error While Adding Product");
                    logger.LogInformation(rtnmsg);
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.StatusCode= StatusCodes.Status409Conflict.ToString();
                    return Ok(baseresponse);
                }
            }
            catch(Exception ex)
            {
               logger.LogError(ex.Message);
                var rtnmsg = string.Format(ex.Message);
                logger.LogInformation(rtnmsg);
                baseresponse.StatusMessage= rtnmsg;
                baseresponse.StatusCode = StatusCodes.Status409Conflict.ToString();

                return Ok(baseresponse);

            }
        }
        [HttpPut("UpdateProducts")]
        public async Task<IActionResult> UpdateProducts(ProductsInsert products)
        {
            BaseResponseStatus baseresponse = new BaseResponseStatus();
            try
            {
                var result = await _productAsyncRepository.UpdateProducts(products);
                if (result > 0)
                {
                    var rtnmsg = string.Format($"Product Updated Successfully with Name {products.ProductName}");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format($"ProductsController-UpdateProducts : Complete Updating Product"));
                    baseresponse.StatusMessage = rtnmsg;
                    baseresponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseresponse.ResponseData = result;
                    return Ok(baseresponse);
                }
                else
                {
                    var rtnmsg = string.Format($"Error While Updating Product");
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
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {

            BaseResponseStatus responseDetails = new BaseResponseStatus();


            try
            {
                var result = await _productAsyncRepository.GetAllProducts();


                if (result.Count == 0)
                {
                    var returnmsg = string.Format("No record found");
                    logger.LogDebug(returnmsg);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnmsg;
                    return Ok(responseDetails);
                }
                else if (result.Count() > 0)
                {
                    var rtnmsg = string.Format("All Product records are fetched successfully.");
                    logger.LogDebug(rtnmsg);
                    logger.LogDebug(string.Format("UserController-GetAllProducts : All User Product are fetched successfully."));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = rtnmsg;
                    responseDetails.ResponseData = result;
                    return Ok(responseDetails);
                }
                else
                {
                    var returnmsg = string.Format("Error While Fetching Product records.");
                    logger.LogDebug(returnmsg);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnmsg;
                    return Ok(responseDetails);
                }
            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);

            }
        }
        [HttpGet("GetProductById")]
        public async Task<ActionResult> GetProductById(long id)
        {

            BaseResponseStatus responseDetails = new BaseResponseStatus();


            try
            {
                var result = await _productAsyncRepository.GetProductById(id);


                if (result == null)
                {
                    var returnmsg = string.Format("No record found");
                    logger.LogDebug(returnmsg);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnmsg;
                    return Ok(responseDetails);
                }
                else if (result != null)
                {
                    var rtnmsg = string.Format("All Product records are fetched successfully.");
                    logger.LogDebug(rtnmsg);
                    logger.LogDebug(string.Format("UserController-GetProductById :  Product By Id are fetched successfully."));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = rtnmsg;
                    responseDetails.ResponseData = result;
                    return Ok(responseDetails);
                }
                else
                {
                    var returnmsg = string.Format("Error While Fetching Product records.");
                    logger.LogDebug(returnmsg);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnmsg;
                    return Ok(responseDetails);
                }
            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);

            }
        }
        [HttpDelete("DeleteProducts")]
        public async Task<IActionResult> DeleteProducts(DeleteObj deleteObj)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                var result = await _productAsyncRepository.DeleteProducts(deleteObj);
                if (result > 0)
                {
                    var returnStr = string.Format("Product Deleted successfully.");
                    logger.LogInformation(returnStr);
                    logger.LogDebug(string.Format("UserController-DeleteProducts : Completed Delete User record "));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = returnStr;
                    responseDetails.ResponseData = result;
                    return Ok(responseDetails);
                }
                else
                {
                    var msgStr = string.Format("Error while Deleting  product record.");
                    logger.LogInformation(msgStr);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = msgStr;
                    return Ok(responseDetails);
                }
            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
        }

    }
}
