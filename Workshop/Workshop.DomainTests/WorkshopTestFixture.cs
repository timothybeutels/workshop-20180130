using System;

namespace Workshop.DomainTests
{
    public class WorkshopTestFixture
    {
        public WorkshopTestFixture Given(object evt)
        {
            return this;
        }

        public WorkshopTestFixture When(object cmd)
        {
            return this;
        }

        public void Then(object evt)
        {
            throw new Exception("implement this");
        }

        public void ThenNothing()
        {
            throw new Exception("implement this");
        }
    }
}
