using EvalServiceLibrary.Model;

namespace EvalServiceLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EvalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EvalServiceLibrary.EvalContext context)
        {
            context.Evals.AddOrUpdate(
                s => s.Id,
                new EvalDTO
                {
                    Id = "00000000-0000-0000-0000-000000000001",
                    Comments = "Comments1",
                    Submitter = "Submitter1",
                    TimeSubmitted = DateTime.Now
                },
                new EvalDTO
                {
                    Id = "00000000-0000-0000-0000-000000000002",
                    Comments = "Comments2",
                    Submitter = "Submitter2",
                    TimeSubmitted = DateTime.Now
                },
                new EvalDTO
                {
                    Id = "00000000-0000-0000-0000-000000000003",
                    Comments = "Comments3",
                    Submitter = "Submitter3",
                    TimeSubmitted = DateTime.Now
                },
                new EvalDTO
                {
                    Id = "00000000-0000-0000-0000-000000000004",
                    Comments = "Comments4",
                    Submitter = "Submitter4",
                    TimeSubmitted = DateTime.Now
                },
                new EvalDTO
                {
                    Id = "00000000-0000-0000-0000-000000000005",
                    Comments = "Comments5",
                    Submitter = "Submitter5",
                    TimeSubmitted = DateTime.Now
                },
                new EvalDTO
                {
                    Id = "00000000-0000-0000-0000-000000000006",
                    Comments = "Comments6",
                    Submitter = "Submitter6",
                    TimeSubmitted = DateTime.Now
                },
                new EvalDTO
                {
                    Id = "00000000-0000-0000-0000-000000000007",
                    Comments = "Comments7",
                    Submitter = "Submitter7",
                    TimeSubmitted = DateTime.Now
                },
                new EvalDTO
                {
                    Id = "00000000-0000-0000-0000-000000000008",
                    Comments = "Comments8",
                    Submitter = "Submitter8",
                    TimeSubmitted = DateTime.Now
                },
                new EvalDTO
                {
                    Id = "00000000-0000-0000-0000-000000000009",
                    Comments = "Comments9",
                    Submitter = "Submitter9",
                    TimeSubmitted = DateTime.Now
                },
                new EvalDTO
                {
                    Id = "00000000-0000-0000-0000-000000000010",
                    Comments = "Comments10",
                    Submitter = "Submitter10",
                    TimeSubmitted = DateTime.Now
                }
                );
        }
    }
}
