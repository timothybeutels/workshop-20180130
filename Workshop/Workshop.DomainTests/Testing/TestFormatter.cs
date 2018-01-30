using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Workshop.DomainTests.Testing
{
    public class TestFormatter
    {
        private readonly TestRecorder _recorder;
        private readonly string _scenario;

        public TestFormatter(TestRecorder recorder, string scenario)
        {
            _recorder = recorder;
            _scenario = scenario;
        }

        public string Format()
        {
            var output = new StringBuilder();

            output.AppendLine($"-------");
            output.AppendLine($"Scenario: {_scenario} ");
            foreach (var r in _recorder.Records)
            {
                output.AppendLine($"({r.Type}) {r.Name} ");
                output.AppendLine(JsonConvert.SerializeObject(r.Payload));
            }

            return output.ToString();
        }
    }
}
