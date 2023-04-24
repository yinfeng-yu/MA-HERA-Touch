using System;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    


    [CreateAssetMenu(menuName = "HERA Touch/Locations List")]
    public class LocationsList : ScriptableObject
    {
        [Serializable]
        public struct SiteLocPair
        {
            [MyBox.ConditionalField(nameof(site), false, SiteEnum.AnnasWard,
                                                         SiteEnum.BobsWard,
                                                         SiteEnum.CharliesWard,
                                                         SiteEnum.DavidsWard,
                                                         SiteEnum.EmilysWard)]
            public int patientId;

            public Site site;
            public Vector3 location;
        }

        // public List<Site> locations;

        public List<SiteLocPair> patientsLocations;
        public List<SiteLocPair> itemsLocations;

        public bool IsPatientWard(Site _site)
        {
            if (_site.siteEnum == SiteEnum.AnnasWard    ||
                _site.siteEnum == SiteEnum.BobsWard     ||
                _site.siteEnum == SiteEnum.CharliesWard ||
                _site.siteEnum == SiteEnum.DavidsWard   ||
                _site.siteEnum == SiteEnum.EmilysWard)
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
            return null;
        }
    }

}
