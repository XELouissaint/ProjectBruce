using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class SettlementJobManager
    {
        public SettlementJobManager(Settlement settlement)
        {
            Settlement = settlement;
            JobDictionary = new Dictionary<Job, LimitList<Pop>>();

            
        }

        public Dictionary<Job, LimitList<Pop>> JobDictionary;
        public Dictionary<Job, int> MaxWorkersPerJob;
        public Settlement Settlement;

        public bool JobsUnWorked()
        {
            bool value = false;

            foreach (Job job in JobDictionary.Keys)
            {
                if (JobDictionary[job].Count == 0)
                {
                    return true;
                }
            }

            return value;
        }

        public void OnJobAdded(Job job)
        {
            if (JobDictionary.ContainsKey(job))
            {
                return;
            }

            JobDictionary[job] = new LimitList<Pop>(5);
        }

        public void SetJobListLength(Job job, int length)
        {
            if(JobDictionary.ContainsKey(job) == false)
            {
                return;
            }

            JobDictionary[job].SetLength(length);
        }

        public bool AddPopToJob(Job job, Pop pop)
        {
            bool result = JobDictionary[job].Add(pop);
            foreach (Job key in JobDictionary.Keys)
            {
                if (JobDictionary[key].Contains(pop) && result == false)
                {
                    RemovePopFromJob(key, pop);
                }
            }
            return result;
        }

        public void RemovePop(Pop pop)
        {
            foreach (Job job in JobDictionary.Keys)
            {
                if (JobDictionary[job].Contains(pop))
                {
                    RemovePopFromJob(job, pop);
                }
            }
        }

        public bool RemovePopFromJob(Job job, Pop pop)
        {
            return JobDictionary[job].Remove(pop);
        }

        public void ExecuteJobs()
        {
            foreach (Job job in JobDictionary.Keys)
            {
                foreach (Pop pop in JobDictionary[job])
                {
                    job.Execute(pop, Settlement);
                }
            }
        }

        public bool AssignPopToUnWorkedJob(Pop pop)
        {
            foreach (Job job in JobDictionary.Keys)
            {

                if (JobDictionary[job].Count == 0)
                {
                    bool add = AddPopToJob(job, pop);

                    return add;
                }
            }

            return false;
        }
    }
}
