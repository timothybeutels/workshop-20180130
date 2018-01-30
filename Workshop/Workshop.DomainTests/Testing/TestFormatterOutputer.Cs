using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.DomainTests.Testing
{
    public class TestFormatterOutputer
    {
        private readonly HtmlTestFormatter _formatter;

        public TestFormatterOutputer(HtmlTestFormatter formatter)
        {
            _formatter = formatter;
        }

        public void Output(string filename)
        {
            var path = @"C:\Users\timothyb\Documents\workshop-20180130\Workshop\Workshop.DomainTests\Docs\";
            using (var stream = new FileInfo(path + filename).OpenWrite())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(_formatter.Format());
                }
            }
        }
    }
}
