using DotBudget.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotBudget.Data.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly ReceiptService receiptService;
        public ReceiptController(ReceiptService _receiptService) {
            receiptService = _receiptService;
        }

        [HttpGet("{id}")]
        public ActionResult GetReceiptById(string id) {
            try
            {
                var receipt = receiptService.GetReceipt(id);
                return Ok(receipt);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult CreateReceipt([FromBody] Receipt receipt) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            receiptService.AddReceipt(receipt);
            return CreatedAtAction(nameof(Receipt), new { id = receipt.Id },
            receipt);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteReceipt(string id) {
            try
            {
                receiptService.DeleteReceipt(id);
                return NoContent();
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
        }

    }
}
