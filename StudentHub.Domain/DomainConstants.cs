using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Domain
{
    public static class DomainConstants
    {

    }

    public enum UserRating
    {
        FRESHER,
        ///100
        ENTHUSIAST,
        ///300
        MID,
        ///700
        LEARNED,
        ///2000
        UNIQUE,
        ///4000
        SCHOLAR,
        ///5000
        MERIT_SCHOLAR
        ////10000

        ///For every 3 days they do not answer,comment the rating reduces by 10 
        ///For every 5 days they do not answer a correct question rating reduces by 40
        ///For every 7 days they do not reply or upvote  rating reduces by 100
        ////These begin to apply when they reach MID
           ///
           
           
        ///For a correct answer +20
        ///For every view their question asked gets.Divide by 5 then +
        /// For every reply to their comment.Divide by 2 then +
        /// These begin to apply when they sign up
    }
}
