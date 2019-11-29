using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 Person（IdCard，Name，Type） 
Athlete（IdCard，AthNumber，Age，EduScore ,Gender，AccountNum（外键））
Leader（IdCard，Tel，AccountNum（外键））
Doctor（IdCard，Tel）
Coach（IdCard，Tel，Gender，AccountNum（外键））
Referee（IdCard，Tel, AccountNum（外键））
Matches（MatchId，MatchName，Gender,Age） 
Ath_Mat（IdCard，MatchId，Score1，Score2，Score3，Score4，Score5，D，P, FinalScore）
Account（AccountNum，Pwd，Type）
Team（AccountNum，TeamName，Attachment）
*/
namespace conToDatabase
{
    class Athlete
    {

    }
    class Account
    {
        public int accountNum;
        public string pwd;
        public string type;
    }
}
