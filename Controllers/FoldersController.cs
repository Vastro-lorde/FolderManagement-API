using Microsoft.AspNetCore.Mvc;

namespace folderManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class FoldersController : ControllerBase
{
    private static string rootPath = Directory.GetCurrentDirectory() +"/wwwroot";
    public FoldersController()
    {
        
    }

    [HttpPost("CreatFolder")]
    public  IActionResult CreatFolder(string name, string? path)
    {
        try
        {
            if (path == null)
            {
                var createdFolder = Directory.CreateDirectory(Path.Combine(rootPath,name));
                return Ok( (HttpContext.Request.IsHttps? "https": "http")+ "://" + HttpContext.Request.Host + "/" + createdFolder.Name);
            }
            var folder = Directory.CreateDirectory(Path.Combine(rootPath,path,name));
            return Ok(folder.Name);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetFolders")]
    public  IActionResult GetFolder(string? FolderName)
    {
        try
        {
            if (FolderName == null) FolderName ="";
            var file = Directory.GetDirectories(rootPath, FolderName, SearchOption.AllDirectories);
            List<string> Folds = new();
            foreach (var i in file)
            {
                int startIndex = rootPath.Length;
                Folds.Add((HttpContext.Request.IsHttps? "https": "http")+ "://" + HttpContext.Request.Host + i.Substring(startIndex));
            }
            if (file.Length == 0) return NotFound("Folder Not found, Check Given Name");
            return Ok(Folds);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("RenameFolder")]
    public  IActionResult RenameFolder(string FolderName, string FolderPath, string NewFolderName)
    {
        try
        {
            string siteHost = (HttpContext.Request.IsHttps? "https": "http")+ "://" + HttpContext.Request.Host;
            if(FolderPath.Contains(siteHost))
            {
                int startIndex = siteHost.Length;
                string newPath = FolderPath.Substring(startIndex);
                FolderPath = rootPath + '/' + newPath;
            }
            Directory.Move(Path.Combine(FolderPath, FolderName), Path.Combine(FolderPath, NewFolderName));
            return Ok("Successfully Renamed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteFolder")]
    public  IActionResult DeleteFolder(string FolderName, string? FolderPath)
    {
        try
        {
            string siteHost = (HttpContext.Request.IsHttps? "https": "http")+ "://" + HttpContext.Request.Host;
            if (FolderPath == null) FolderPath = "";
            if(FolderPath.Contains(siteHost))
            {
                int startIndex = siteHost.Length;
                string newPath = FolderPath.Substring(startIndex);
                FolderPath = rootPath + '/' + newPath;
                Directory.Delete(Path.Combine(FolderPath, FolderName), true);
                return Ok(FolderName + " folder Successfully deleted.");
            }
            Directory.Delete(Path.Combine(rootPath,FolderPath, FolderName), true);
            return Ok(FolderName + " folder Successfully deleted.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}