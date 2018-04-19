using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker_Library.Models
{
    public class PersonModel
    {
        public int PersonId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAdress { get; set; }

        public string CellphoneNumber { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} { LastName }";
            }
        }
       /* 
        public PersonModel()
        {

        }

        public PersonModel(string firstName, string lastName, string email, string cellphone)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAdress = email;
            CellphoneNumber = cellphone;


        }
        */
    }
}
