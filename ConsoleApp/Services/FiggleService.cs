using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class FiggleWriteService : IFiggleWriteService
    {
        private IWriteService service;

        private ILogger<FiggleWriteService> logger;

        public FiggleWriteService(IWriteService service, ILogger<FiggleWriteService> logger)
        {
            this.logger = logger;
            this.service = service;
        }

        public void WriteLine(string @string)
        {
            logger.LogInformation("Base string: " + @string);
            @string = Figgle.FiggleFonts.Standard.Render(@string);


            service.WriteLine(@string);
        }
    }
}
