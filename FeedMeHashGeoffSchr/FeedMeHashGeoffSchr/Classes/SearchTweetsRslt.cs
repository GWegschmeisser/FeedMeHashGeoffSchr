using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FeedMeHashGeoffSchr.Classes
{
    [DataContract]
    public class SearchTweetsRslt
    {
        [DataMember]
        TweetsRsltDisplay[] status;
    }
    [DataContract]
    public class TweetsRsltDisplay
    {
        [DataMember]
        string created_at;      // This is actually a date and can be translated to one.
        [DataMember]
        string id_str;
        [DataMember]
        string text;
        [DataMember]
        string source;
        RsltIdentifier user;
    }
    [DataContract]
    public class RsltIdentifier
    {
        [DataMember]
        string id_str;
        [DataMember]
        string name;
        [DataMember]
        string screen_name;
        [DataMember]
        string location;
        [DataMember]
        string description;
    }
}