using BackendExam.Api.Helpers;
using BackendExam.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BackendExam.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BackendExamController : ControllerBase
{
    private readonly IConfiguration _config;

    public BackendExamController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] MyOfficeACPD data)
    {
        string connStr = _config.GetConnectionString("DefaultConnection")!;
        string json = JsonSerializer.Serialize(data);
        string result = await SqlHelper.ExecuteStoredProcedureAsync(connStr, "usp_MyOfficeACPD_Insert", json);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] MyOfficeACPD data)
    {
        string connStr = _config.GetConnectionString("DefaultConnection")!;
        string json = JsonSerializer.Serialize(data);
        string result = await SqlHelper.ExecuteStoredProcedureAsync(connStr, "usp_MyOfficeACPD_Update", json);
        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] JsonElement json)
    {
        string connStr = _config.GetConnectionString("DefaultConnection")!;
        string result = await SqlHelper.ExecuteStoredProcedureAsync(connStr, "usp_MyOfficeACPD_Delete", json.ToString());
        return Ok(result);
    }

    [HttpGet("list")]
    public async Task<IActionResult> List()
    {
        string connStr = _config.GetConnectionString("DefaultConnection")!;
        string result = await SqlHelper.ExecuteStoredProcedureAsync(connStr, "usp_MyOfficeACPD_GetAll", "{}");
        return Ok(JsonDocument.Parse(result));
    }
}
