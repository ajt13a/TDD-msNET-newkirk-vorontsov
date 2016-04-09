﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=1.1.4322.573.
// 
using System.Xml.Serialization;


/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://nunit.org/webservices")]
[System.Xml.Serialization.XmlRootAttribute("Recording", Namespace="http://nunit.org/webservices", IsNullable=false)]
public class RecordingDto {
    
    /// <remarks/>
    public long id;
    
    /// <remarks/>
    public string title;
    
    /// <remarks/>
    public string artistName;
    
    /// <remarks/>
    public string releaseDate;
    
    /// <remarks/>
    public string labelName;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("tracks")]
    public TrackDto[] tracks;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("reviews")]
    public ReviewDto[] reviews;
    
    /// <remarks/>
    public string totalRunTime;
    
    /// <remarks/>
    public int averageRating;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://nunit.org/webservices")]
public class TrackDto {
    
    /// <remarks/>
    public long id;
    
    /// <remarks/>
    public string title;
    
    /// <remarks/>
    public string artistName;
    
    /// <remarks/>
    public string duration;
    
    /// <remarks/>
    public string genreName;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://nunit.org/webservices")]
public class ReviewDto {
    
    /// <remarks/>
    public long id;
    
    /// <remarks/>
    public string reviewerName;
    
    /// <remarks/>
    public int rating;
    
    /// <remarks/>
    public string reviewContent;
}
