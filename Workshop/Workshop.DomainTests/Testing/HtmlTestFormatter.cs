using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Workshop.DomainTests.Testing
{
    public class HtmlTestFormatter
    {
        private readonly TestRecorder _recorder;
        private readonly string _scenario;

        public HtmlTestFormatter(TestRecorder recorder, string scenario)
        {
            _recorder = recorder;
            _scenario = scenario;
        }

        public string Format()
        {
            var output = new StringBuilder();

            output.AppendLine("<article class=\"scenario\">");
            output.AppendLine($"<div class=\"sticky like-paper\"><h2>{ _scenario}</h2></div>");
            foreach (var r in _recorder.Records)
            {
                output.AppendLine($"<div class=\"{r.Type.ToString().ToLowerInvariant()} sticky like-paper\">");
                output.AppendLine($"<h2>{r.Name}</h2>");

                if (r.Payload != null)
                {
                    output.Append(FormatObject(r.Payload));
                }

                output.AppendLine("</div>");
            }
            output.AppendLine("</article>");

            var template = new FileInfo(@"C:\Users\timothyb\Documents\workshop-20180130\Workshop\Workshop.DomainTests\Testing\template.html").OpenText().ReadToEnd();

            return template.Replace("@@@placeholder@@@", output.ToString());
        }

        private string FormatObject(object obj)
        {
            var output = new StringBuilder();

            output.AppendLine("<table>");

            foreach (var p in obj.GetType().GetProperties())
            {
                if (p.PropertyType.IsArray)
                {
                    var value = (object[])p.GetValue(obj);
                    if (value.Any())
                    {
                        output.AppendLine($"<tr><th>{p.Name}</th><td>");

                        foreach (var v in value)
                        {
                            output.Append(FormatObject(v));
                        }

                        output.AppendLine($"</td></tr>");
                    }
                    else
                    {
                        output.AppendLine($"<tr><th>{p.Name}</th><td>[]</td></tr>");
                    }
                }
                else
                {
                    output.AppendLine($"<tr><th>{p.Name}</th><td>{p.GetValue(obj)}</td></tr>");
                }
            }

            output.AppendLine("</table>");

            return output.ToString();
        }
    }
}
