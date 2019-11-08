using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskWeb.Models;

namespace MegaDeskWeb.Pages.Desks
{
    public class DetailsModel : PageModel
    {
        private readonly MegaDeskWeb.Models.MegaDeskWebContext _context;

        //Calculation Variables
        public int surfaceArea, surfaceAreaPrice, drawerPrice, surfacePrice, rushPrice, priceQuote;
        public string rushCode;
        int PRICE_PER_SQUARE_INCH = 1;
        public int BASE_PRICE= 200;
        public int PRICE_PER_DRAWER = 50;
        

        public DetailsModel(MegaDeskWeb.Models.MegaDeskWebContext context)
        {
            _context = context;
        }

        public DeskQuote DeskQuote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeskQuote = await _context.DeskQuote.FirstOrDefaultAsync(m => m.DeskQuoteID == id);

            if (DeskQuote == null)
            {
                return NotFound();
            }
            return Page();
        }

        public int SurfaceAreaPrice()
        {
           
            if (surfaceArea > 1000)
            {
                surfaceAreaPrice = surfaceArea * PRICE_PER_SQUARE_INCH;
            }
            else
            {
                surfaceAreaPrice = BASE_PRICE;
            }return surfaceAreaPrice;
        }
       
        public enum SurfaceMaterial
        {
            Laminate=100,
            Oak=200,
            Pine=50,
            Rosewood= 300,
            Veneer=125
        }
        public int GetRushPrice(string rushCode)
        {

            switch (rushCode)
            {
                case "3S":
                    return 60;
                case "3M":
                    return 70;
                case "3L":
                    return 80;
                case "5S":
                    return 40;
                case "5M":
                    return 50;
                case "5L":
                    return 60;
                case "7S":
                    return 30;
                case "7M":
                    return 35;
                case "7L":
                    return 40;
                default: 
                    return 0;
            }
        }
        public int CalcTotalPrice(DeskQuote desk)
        {
            surfaceArea = desk.Width * desk.Depth;
            surfaceAreaPrice = SurfaceAreaPrice();
            drawerPrice = desk.Drawers * PRICE_PER_DRAWER;
            surfacePrice = (int)Enum.Parse(typeof(SurfaceMaterial), desk.DeskMaterial);
            rushCode = desk.RushOrderDays.ToString();
            if(surfaceArea<1000)
            {
                rushCode += "S";
            }else if (surfaceArea <= 2000)
            {
                rushCode += "M";
            }
            else
            {
                rushCode += "L";
            }

            rushPrice = GetRushPrice(rushCode);

            priceQuote = surfaceAreaPrice + drawerPrice + surfacePrice + rushPrice;
            return priceQuote;
        }
    }
}
