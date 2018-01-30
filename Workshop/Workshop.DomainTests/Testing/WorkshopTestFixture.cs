using System;

namespace Workshop.DomainTests.Testing
{
    public class WorkshopTestFixture
    {
        

        private readonly TestRecorder _recorder;
        private readonly string _scenario;

        public WorkshopTestFixture(string scenario)
        {
            _scenario = scenario;
            _recorder = new TestRecorder();
        }

        public WorkshopTestFixture Given<TEvent>(TEvent evt)
        {
            _recorder.Event(evt);

            return this;
        }

        public WorkshopTestFixture When<TCommand>(TCommand cmd)
        {
            _recorder.Command(cmd);

            return this;
        }

        public void Then<TEvent>(TEvent evt)
        {
            _recorder.Event(evt);
            End();

//            throw new Exception("implement this");
        }

        public void ThenNothing()
        {
            _recorder.Nothing();
            End();

 //           throw new Exception("implement this");
        }

        private void End()
        {
            _recorder.Close();

            var formatter = new HtmlTestFormatter(_recorder, _scenario);
            var outputter = new TestFormatterOutputer(formatter);

            outputter.Output(_scenario + ".html");
        }
    }
}
