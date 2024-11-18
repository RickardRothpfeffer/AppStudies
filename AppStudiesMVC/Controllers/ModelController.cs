using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppStudiesMVC.Models;
using Services;

namespace AppStudiesMVC.Controllers;

public class ModelController : Controller
{
    private readonly ILogger<ModelController> _logger;
    IQuoteService _service = null;

    public ModelController(ILogger<ModelController> logger, IQuoteService service)
    {
        _service = service;
        _logger = logger;
    }

    //Will execute on a Get request
    [HttpGet]
    public IActionResult ModelList()
    {
        var vw = new ModelListViewModel();

        //Use the Service
        vw.Quotes = _service.ReadQuotes();
        //return View(vw);
        return View("ModelList", vw);
    }

    //Will execute on a Get request
    [HttpGet]
    [Route("/Model/Search")]
    [Route("quotes/{search?}")]
    public IActionResult Search(string search, int pagenr)
    {
        var vwm = new SearchViewModel(){ThisPageNr = pagenr, SearchFilter = search};
        
        //Pagination
        vwm.UpdatePagination(_service);

        //Use the Service
        vwm.Quotes = _service.ReadQuotes(vwm.ThisPageNr, vwm.PageSize, vwm.SearchFilter);

        return View(vwm);
    }

    [HttpPost]
    public IActionResult Find(SearchViewModel vwm)
    {
        //Pagination
        vwm.UpdatePagination(_service);

        //Use the Service
        vwm.Quotes = _service.ReadQuotes(vwm.ThisPageNr, vwm.PageSize, vwm.SearchFilter);

        return View("Search", vwm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

