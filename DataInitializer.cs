using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Control_22_04.Models;

namespace Control_22_04
{
    public class DataInitializer : CreateDatabaseIfNotExists<AppContext> 
    {
        private List<Journal> journals;
        public DataInitializer()
        {
            journals = new List<Journal>()
            {
                new Journal
                {
                    Name = "Nomad",
                    PageCount = 125,
                    PublicationDate = new DateTime(2019,02,03)
                },
                new Journal
                {
                    Name = "Nomad Life",
                    PageCount = 140,
                    PublicationDate = new DateTime(2019,02,20)
                },
                new Journal
                {
                    Name = "Nomad",
                    PageCount = 125,
                    PublicationDate = new DateTime(2019,03,03)
                },
                new Journal
                {
                    Name = "Nomad",
                    PageCount = 125,
                    PublicationDate = new DateTime(2019,04,03)
                },

            };
        }
        protected override void Seed(AppContext context)
        {
            context.Journals.AddRange(journals);
            context.SaveChanges();
        }
    }
}
