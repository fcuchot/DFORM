using System;
using System.Collections.Generic;
using System.Text;

namespace DForm
{
    public class FormAnswer /*: IFormAnswer*/
    {
        private string _userName;

        public FormAnswer( string userName )
        {
            _userName = userName;
        }


        public string UserName { get => _userName; set => _userName = value; }
    }
}
