using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public class SitesManager : MonoBehaviour
    {
        #region Singleton
        public static SitesManager instance;
        private void Awake()
        {
            if (instance != this)
            {
                instance = this;
            }
        }

        #endregion

        public List<Site> sites;

        // Start is called before the first frame update
        void Start()
        {
            CheckSitesSanity();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void CheckSitesSanity()
        {
            // TODO
            // Debug.LogError("The sites input is not correct.");
        }

        public Site GetSite(SiteEnum a_siteEnum)
        {
            foreach (var s in sites)
            {
                if (s.siteEnum == a_siteEnum)
                {
                    return s;
                }
            }
            return null;
        }

        public Vector3? GetLocation(SiteEnum a_siteEnum)
        {
            foreach (var s in sites)
            {
                if (s.siteEnum == a_siteEnum)
                {
                    return s.location;
                }
            }
            return null;
        }

        public Vector3? GetPatientLocation(int a_patientId)
        {
            foreach (var s in sites)
            {
                if (s.isPatient && s.patientId == a_patientId)
                {
                    return s.location;
                }
            }
            return null;
        }

        public SiteEnum GetPatientSiteEnum(int a_patientId)
        {
            foreach (var s in sites)
            {
                if (s.isPatient && s.patientId == a_patientId)
                {
                    return s.siteEnum;
                }
            }
            return SiteEnum.AnnasWard;
        }

        public string GetSiteName(SiteEnum a_siteEnum)
        {
            return a_siteEnum.ToString(); // TODO
        }

    }
}

