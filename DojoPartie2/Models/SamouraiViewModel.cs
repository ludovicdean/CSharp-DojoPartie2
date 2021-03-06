﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpModule6Bo;

namespace DojoPartie2.Models
{
    public class SamouraiViewModel
    {
        public Samourai Samourai { get; set; }
        public List<Arme> Armes { get; set; } = new List<Arme>();
        public List<ArtMartial> ArtMartials { get; set; } = new List<ArtMartial>();
        public List<int> IdArtMartials { get; set; } = new List<int>();
        public int? IdSelectedArme { get; set; }
    }
}