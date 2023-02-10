using EntityFramworkDemoProject.Model;
using EntityFramworkDemoProject.Models;
using EntityFramworkDemoProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EntityFramworkDemoProject.Model.BaseModel;

namespace EntityFramworkDemoProject.Controllers
{
    [Route("EntityFramworkDemoProject/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IUserRepository userrepository;
        IConfiguration configuration;
        public UserController(IConfiguration configuration, ILoggerFactory loggerFactory, IUserRepository userrepository)
        {

            this.userrepository = userrepository;
            this.configuration = configuration;
            this.logger = loggerFactory.CreateLogger<UserController>();
        }
        [HttpPost("RegisterNewUser")]
        public async Task<IActionResult> RegisterNewUser(UserInsertModel user)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                var result = await userrepository.RegisterNewUser(user);
                if (result > 0)
                {
                    var returnStr = string.Format($"User registered successfully ");
                    logger.LogInformation(returnStr);
                    logger.LogDebug(string.Format("UserController-RegisterNewUser : Completed User Registration record "));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = returnStr;
                    responseDetails.ResponseData = result;
                    return Ok(responseDetails);
                }

                else
                {
                    var msgStr = string.Format("Error while adding User record.");
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
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdatetModel user)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                var result = await userrepository.UpdateUser(user);
                if (result > 0)
                {
                    var returnStr = string.Format("User Updated successfully.");
                    logger.LogInformation(returnStr);
                    logger.LogDebug(string.Format("UserController-UpdateUser : Completed Updated User record "));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = returnStr;
                    responseDetails.ResponseData = result;
                    return Ok(responseDetails);
                }
                else
                {
                    var msgStr = string.Format("Error while updating User record.");
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
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                var user = await userrepository.GetUserById(Id);
                if (user == null)
                {
                    var returnMsg = string.Format("User with Id is not available.");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                else
                {
                    var rtrMsg = string.Format("Record Fetch Successfully.");
                    logger.LogDebug(rtrMsg);
                    logger.LogDebug(string.Format("UserController-GetUserById : Record Fetch Successfully."));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = rtrMsg;
                    responseDetails.ResponseData = user;
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

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {

            BaseResponseStatus responseDetails = new BaseResponseStatus();


            try
            {
                var result = await userrepository.GetAllUsers();

               
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
                    var rtnmsg = string.Format("All User records are fetched successfully.");
                    logger.LogDebug(rtnmsg);
                    logger.LogDebug(string.Format("UserController-GetAllUsers : All User records are fetched successfully."));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = rtnmsg;
                    responseDetails.ResponseData = result;                  
                    return Ok(responseDetails);
                }
                else
                {
                    var returnmsg = string.Format("Error While Fetching User records.");
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
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(DeleteObj deleteObj)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                var result = await userrepository.DeleteUser(deleteObj);
                if (result > 0)
                {
                    var returnStr = string.Format("User Deleted successfully.");
                    logger.LogInformation(returnStr);
                    logger.LogDebug(string.Format("UserController-DeleteUser : Completed Updated User record "));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = returnStr;
                    responseDetails.ResponseData = result;
                    return Ok(responseDetails);
                }
                else
                {
                    var msgStr = string.Format("Error while Deleting User User record.");
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

      /*  [HttpGet("GetAllUsersLinq")]
        public async Task<IActionResult> GetAllUsersLinq()
        {
            var result=await userrepository.GetAllUsersLinq();  
            return Ok(result);  
        }*/
    }
}
