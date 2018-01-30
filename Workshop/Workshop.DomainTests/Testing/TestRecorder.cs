using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.DomainTests.Testing
{
    public class TestRecorder
    {
        public enum RecordType
        {
            Event,
            Command,
            Hotspot
        }

        public struct RecordDto
        {
            public RecordDto(string name, RecordType type, object payload)
            {
                Name = name;
                Type = type;
                Payload = payload;
            }

            public RecordType Type { get; }
            public String Name { get; }

            public Object Payload { get; }
        }

        public List<RecordDto> Records { get; } = new List<RecordDto>();

        public void Event<TEvent>(TEvent payload)
        {
            Records.Add(new RecordDto(typeof(TEvent).Name, RecordType.Event, payload));
        }

        public void Command<TCommand>(TCommand payload)
        {
            Records.Add(new RecordDto(typeof(TCommand).Name, RecordType.Command, payload));
        }

        public void Nothing()
        {
            Records.Add(new RecordDto("Nothing", RecordType.Hotspot, null));
        }

        public void Close()
        {
            

        }
    }
}
