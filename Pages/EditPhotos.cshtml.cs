using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Enums;
using RMeets.Models;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace RMeets.Pages;

public class AddPhotos : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private IWebHostEnvironment _appEnvironment;
    public ApplicationContext ApplicationContext { get; set; }
    public AddPhotos(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext, IWebHostEnvironment environment)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
        _appEnvironment = environment;
    }
    [BindProperty]
    public int blankId { get; set; }
    public void OnGet(int blankId)
    {
        this.blankId = blankId;
        Debug.WriteLine($"Blank edit photo: {blankId}");
    }
    public IActionResult OnGetConsume(int blankId)
    {
        return RedirectToPage("ViewBlank", new
        {
            blankId = blankId
        });
    }
    [BindProperty]
    public IFormFile? Upload { get; set; }
    public IActionResult OnPostAdd()
    {
        var uploadedFile = Upload;
        if (uploadedFile == null) return Content("Invalid file");
        Debug.WriteLine($"Uploading file: {uploadedFile?.Name.ToString()} : {uploadedFile?.Length}");
        var pt = ApplicationContext.BlankPhotos.Count();
        var path = $"\\UploadedPhotos\\{pt}_" + uploadedFile?.FileName;
        var paths = _appEnvironment.WebRootPath + path;
        Debug.WriteLine($"Saving path: {paths}");
        using (var fileStream = new FileStream(paths, FileMode.Create))
            uploadedFile?.CopyTo(fileStream);
        var level = ApplicationContext.AccessibilityLevels.FirstOrDefault(x =>
            x.Level == AccessibilityLevels.Private);
        Debug.WriteLine($"Accessibility set to: {level.Level}");
        var ph = new BlankPhoto()
            { AccessibilityLevel = level, Url = path,BlankId = blankId};
        ApplicationContext.BlankPhotos.Add(ph);
        ApplicationContext.SaveChanges();
        Debug.WriteLine($"Adding db record: {ph.Id} {ph.AccessibilityLevel.Level} {ph.Url}");
        return RedirectToPage("EditPhotos",new
        {
            blankId = blankId
        });
    }
    public IActionResult OnPostDelete(int photoId)
    {
        var photos = from photo in ApplicationContext.BlankPhotos where photo.Id == photoId select photo;
        foreach (var blankPhoto in photos)
        {
            var pt = _appEnvironment.WebRootPath + blankPhoto.Url;
            if (System.IO.File.Exists(pt))
            {
                System.IO.File.Delete(pt);
            }
        }
        ApplicationContext.BlankPhotos.RemoveRange(photos);
        ApplicationContext.SaveChanges();
        return RedirectToPage("EditPhotos",new
        {
            blankId = blankId
        });
    }
}
