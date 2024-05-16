using Inventarios.Server.AspNet.Dto_s;
using Inventarios.Server.AspNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolutionPal.RazorPages.Models;
using System.Threading.Tasks.Dataflow;

namespace Inventarios.Server.AspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _service;
        private readonly ResponseDto _response;

        public StoreController(StoreService service)
        {
            _service = service;
            _response = new();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Store store)
        {
            try
            {
                var result = await _service.CreateStore(store);
                if (result == "1")
                {
                    _response.DisplayMessage = "New Item created!";
                    return Ok(_response);
                }
                _response.DisplayMessage = "Internal Error";
                _response.ErrorMessage = result;
                return BadRequest(_response);
            }
            catch (Exception e)
            {
                _response.DisplayMessage = "Error ";
                _response.ErrorMessage = e.ToString();
                return BadRequest(_response);

            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(StoreUpdateDto store)
        {
            try
            {
                var result = await _service.UpdateStore(store);
                if (result=="1")
                {
                    _response.DisplayMessage = "Record is update!";
                    return Ok(_response);
                }
                _response.DisplayMessage = result;
                return BadRequest(_response);
            }
            catch (Exception e)
            {
                _response.DisplayMessage = "Error ";
                _response.ErrorMessage = e.ToString();
                return BadRequest(_response);

            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetStoreById(id);
                if (result !=null)
                {
                    _response.Result = result;
                    _response.DisplayMessage = "Product details ProductName : " + result.ProductName;
                    return Ok(_response);
                }
                _response.DisplayMessage = "Data not found!";
                return BadRequest(_response);
            }
            catch (Exception e)
            {
                _response.DisplayMessage = "Error ";
                _response.ErrorMessage = e.ToString();
                return BadRequest(_response);

            }
        }

        [HttpGet("OptionsList")]
        public async Task<IActionResult> GetOptionsList()
        {
            List<string> list = ["none", "add", "substract"];

            return Ok(list); 
        }
        [HttpGet]
        public async Task<IActionResult> Get(string? batch,int pageNumber, int pageSize)
        {
            try
            {
                if (batch!=null)
                {
                    var result = await _service.getStoresByBatchOrProductName(batch,pageNumber, pageSize);
                    if (result != null)
                    {
                        _response.DisplayMessage = "List of Stores";
                        _response.Result = result;
                        _response.nextPage = pageNumber + 1;
                        _response.previosPage = pageNumber - 0;
                        return Ok(_response);
                    }
                }
                else
                {
                    var result = await _service.getStores(pageNumber, pageSize);
                    if (result != null)
                    {
                        _response.DisplayMessage = "List of Stores";
                        _response.Result = result;
                        _response.nextPage = pageNumber+1;
                        _response.previosPage = pageNumber-0;
                        return Ok(_response);
                    }
                }
              //  var result = await _service.getStore();
              
                _response.DisplayMessage = "Internal Error";
                _response.ErrorMessage = "Data Not found!";
                _response.nextPage = 0;
               // _response.previosPage = pageNumber;
                return BadRequest(_response);
            }
            catch (Exception e)
            {
                _response.DisplayMessage = "Error ";
                _response.ErrorMessage = e.ToString();
                return BadRequest(_response);

            }
        }

    }
}
