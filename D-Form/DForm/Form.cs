using System;
using System.Collections.Generic;
using System.Linq;

namespace DForm
{
    public class Form
    {
        private string _title;

        private Dictionary<string, FormAnswer> existingFormAnswer = new Dictionary<string, FormAnswer>();
        public Form()
        {

        }

        public FormAnswer FindOrCreateAnswer( string user )
        {
            FormAnswer fa = new FormAnswer( user );
            FormAnswer existingAnswer;


            existingFormAnswer.TryGetValue( user, out existingAnswer );

            if( existingAnswer == null )
            {
                existingFormAnswer.Add( user, fa );
                return fa;
            }
            else
            {
                return existingAnswer;
            }
        }

        public FormAnswer Create( string userName )
        {

            FormAnswer fa = new FormAnswer( userName );
            existingFormAnswer.Add( userName, fa );

            return fa;
        }

        public FormAnswer Find( string userName )
        {
            FormAnswer existingAnswer;
            existingFormAnswer.TryGetValue( userName, out existingAnswer );

            return existingAnswer;

        }
        public string Title { get => _title; set => _title = value; }
    }
}
