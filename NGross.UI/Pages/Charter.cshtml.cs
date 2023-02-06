using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NGross.UI.Pages;

public class ChartWatcher : PageModel
{
    private readonly ILogger<ChartWatcher> _logger;

    public ChartWatcher(ILogger<ChartWatcher> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}