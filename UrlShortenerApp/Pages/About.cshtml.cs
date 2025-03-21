using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AboutModel : PageModel
{
    [BindProperty]
    public string Description { get; set; } = "This page describes how my URL shortening algorithm works.";

    [Authorize(Roles = "Admin")]
    public IActionResult OnPost()
    {
        Description = Request.Form["Description"];
        return Page();
    }
}