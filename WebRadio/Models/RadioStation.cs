using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRadio.Models
{
    public class RadioStation
    {
        public string id { get; set; }
        public string changeuuid { get; set; }
        public string stationuuid { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string homepage { get; set; }
        public string favicon { get; set; }
        public string tags { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string language { get; set; }
        public string votes { get; set; }
        public string negativevotes { get; set; }
        public string lastchangetime { get; set; }
        public string ip { get; set; }
        public string codec { get; set; }
        public string bitrate { get; set; }
        public string hls { get; set; }
        public string lastcheckok { get; set; }
        public string lastchecktime { get; set; }
        public string lastcheckoktime { get; set; }
        public string clicktimestamp { get; set; }
        public string clickcount { get; set; }
        public string clicktrend { get; set; }
    }
}