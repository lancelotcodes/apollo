using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Common.Interfaces;

namespace apollo.Infrastructure.Services
{
    public class CSVService: ICSVService
    {
        public async Task<List<Dictionary<string, object>>> ReadCSV(IFormFile file)
        {
            var properties = new List<Dictionary<string, object>>();

            var propertyNames = new List<string>();

            if (file != null)
            {
                using (StreamReader reader = new StreamReader(file.OpenReadStream()))
                {
                    int firstLine = 0;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        int index = 0;
                        Dictionary<string, object> myDict = new Dictionary<string, object>();

                        foreach (var item in values)
                        {
                            if(firstLine == 0) {
                                propertyNames.Add(item);
                            }
                            else
                            {
                                myDict.Add(propertyNames[index], item);
                            }

                            index++;
                        }

                        if(firstLine > 0) { 
                            properties.Add(myDict);
                        }
                        firstLine++;
                    }
                }
            }

            return properties;
        }
    }
}
