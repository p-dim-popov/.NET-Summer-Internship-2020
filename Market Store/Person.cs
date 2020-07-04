using System;
using System.Linq;

namespace MarketStore
{
    class Person
    {
        private string _firstName;
        private string _lastName;
        private string _uniqueCitizenshipNumber;
        public Person(string firstName, string lastName, string uniqueCitizenshipNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UniqueCitizenshipNumber = uniqueCitizenshipNumber;
        }

        public string FirstName
        {
            get => this._firstName;
            private set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException("First name cannot be bellow 3 letters!");
                }

                this._firstName = value;
            }
        }

        public string LastName
        {
            get => this._lastName;
            private set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException("First name cannot be bellow 3 letters!");
                }

                this._lastName = value;
            }
        }

        private string UniqueCitizenshipNumber
        {
            get => this._uniqueCitizenshipNumber;
            set
            {
                if (value.Replace(" ", "").Length < 10)
                {
                    throw new ArgumentException("The UCN cannot be below 10 characters");
                }

                if (value.Any(char.IsLetter))
                {
                    throw new ArgumentException("The UCN cannot consist of letters");
                }

                this._uniqueCitizenshipNumber = value;
            }
        }
    }
}
