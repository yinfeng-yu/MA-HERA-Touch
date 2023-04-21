using System;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public enum Site
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

    [CreateAssetMenu(menuName = "HERA Touch/Locations List")]
    public class LocationsList : ScriptableObject
    {
        [Serializable]
        public struct SiteLocPair
        {
            [MyBox.ConditionalField(nameof(site), false, Site.AnnasWard,
                                                         Site.BobsWard,
                                                         Site.CharliesWard,
                                                         Site.DavidsWard,
                                                         Site.EmilysWard)]
            public int patientId;

            public Site site;
            public Vector3 location;
        }

        public List<SiteLocPair> patientsLocations;
        public List<SiteLocPair> itemsLocations;

        public bool IsPatientWard(Site _site)
        {
            if (_site == Site.AnnasWard    ||
                _site == Site.BobsWard     ||
                _site == Site.CharliesWard ||
                _site == Site.DavidsWard   ||
                _site == Site.EmilysWard)
            {
                return true;
            }
            return false;
        }

        public Vector3 GetLocation(Site _site)
        {
            foreach (var sl in patientsLocations)
            {
                if (sl.site == _site)
                {
                    return sl.location;
                }
            }
            foreach (var sl in itemsLocations)
            {
                if (sl.site == _site)
                {
                    return sl.location;
                }
            }
            return Vector3.zero;
        }

        public Vector3 GetPatientLocation(int patientId)
        {
            foreach (var sl in patientsLocations)
            {
                if (sl.patientId == patientId && IsPatientWard(sl.site))
                {
                    return sl.location;
                }
            }
            return Vector3.zero;
        }

        public Site GetPatientSite(int patientId)
        {
            foreach (var sl in patientsLocations)
            {
                if (sl.patientId == patientId && IsPatientWard(sl.site))
                {
                    return sl.site;
                }
            }
            return Site.AnnasWard;
        }
    }

}
