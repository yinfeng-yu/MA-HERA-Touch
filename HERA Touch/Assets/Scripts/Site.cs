using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public enum SiteEnum
    {
        AnnasWard,
        BobsWard,
        CharliesWard,
        DavidsWard,
        EmilysWard,
        WaterCollectSite,
        ThermoCollectSite,
        ThermoReturnSite,
    }

    [Serializable]
    public class Site
    {
        public SiteEnum siteEnum;
        public Vector3 location;

        public bool isPatient;
        [MyBox.ConditionalField(nameof(isPatient), false)]
        public int patientId;
    }

    
}