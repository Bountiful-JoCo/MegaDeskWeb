using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MegaDeskWeb.Pages.Desks
{
    public class IndexModel : PageModel
    {
        private readonly MegaDeskWeb.Models.MegaDeskWebContext _context;

        public IndexModel(MegaDeskWeb.Models.MegaDeskWebContext context)
        {
            _context = context;
        }
        
        public IList<DeskQuote> DeskQuote { get;set; }
        [BindProperty(SupportsGet=true)]
        public string SearchString { get; set; }
        public SelectList Customers { get; set; }
       
        public string DateSort { get; set; }
        public string CustomerSort { get; set; }
        public string CurrentSort { get; set; }
        public string SortOrder { get; set; }
        public async Task OnGetAsync(string searchString, string sortOrder)
        {
           IQueryable<DeskQuote> customers = from c in _context.DeskQuote
                                                select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.LastName.Contains(searchString));
            }
          
            //Sorting quotes by date and name
            CustomerSort = String.IsNullOrEmpty(sortOrder) ? "customer_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            switch (sortOrder)
            {
                case "customer_desc":
                    customers = customers.OrderByDescending(c => c.LastName);
                    break;
                case "Date":
                    customers = customers.OrderBy(c => c.QuoteDate);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(c => c.QuoteDate);
                    break;
                default:
                    customers = customers.OrderBy(c => c.LastName);
                    break;
            }


            DeskQuote = await customers.AsNoTracking().ToListAsync();
        }
    }
}
