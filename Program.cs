using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MegaDeskWeb
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
        //Calculation Variables
        public static int surfaceArea, surfaceAreaPrice, drawerPrice, surfacePrice, rushPrice, priceQuote;
        public static string rushCode;
        static int PRICE_PER_SQUARE_INCH = 1;
        public static int BASE_PRICE= 200;
        public static int PRICE_PER_DRAWER = 50;

        public enum SurfaceMaterial
        {
            Laminate=100,
            Oak=200,
            Pine=50,
            Rosewood= 300,
            Veneer=125
        }
        
        public static int CalcTotalPrice(int width, int depth, int drawers, int rushDays, string material)
        {
            surfaceArea = width * depth;
            surfaceAreaPrice = GetSurfaceAreaPrice();
            drawerPrice = drawers * PRICE_PER_DRAWER;
            surfacePrice = (int)Enum.Parse(typeof(SurfaceMaterial), material);
            rushCode = rushDays.ToString();
            if(surfaceArea<1000)
            {
                rushCode += "S";
            }
            else if (surfaceArea <= 2000)
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

        public static int GetSurfaceAreaPrice()
        {
            if (surfaceArea > 1000)
            {
                surfaceAreaPrice = surfaceArea * PRICE_PER_SQUARE_INCH;
            }
            else
            {
                surfaceAreaPrice = BASE_PRICE;
            }
            return surfaceAreaPrice;
        }

        public static int GetRushPrice(string rushCode)
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
    }
}
