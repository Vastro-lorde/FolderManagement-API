using Microsoft.AspNetCore.Mvc;

namespace folderManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private static string rootPath = Directory.GetCurrentDirectory() + "/wwwroot";
    public FileController()
    {
        
    }

    [HttpPost("UploadFile")]
    public async Task<IActionResult> UploadFile(IFormFile file, string? FolderPath)
    {
        try
        {
            string siteHost = (HttpContext.Request.IsHttps? "https": "http")+ "://" + HttpContext.Request.Host;
            if (FolderPath == null)
            {
                await file.CopyToAsync(new FileStream(rootPath, FileMode.Create, FileAccess.ReadWrite));
                return Ok("File created at: " + Path.Combine(rootPath,file.Name) );
            }
            await file.CopyToAsync(new FileStream(Path.Combine(rootPath,FolderPath), FileMode.Create, FileAccess.ReadWrite));
            return Ok("File created at: " + Path.Combine(rootPath,FolderPath,file.Name) );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetFile")]
    public  IActionResult GetFile(string FileName, string FilePath)
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("RenameFile")]
    public  IActionResult RenameFolder(string FileName, string FolderPath, string NewFileName)
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteFile")]
    public  IActionResult DeleteFolder(string FileName, string FolderPath)
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}