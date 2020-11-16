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
            JobToPopDictionary = new Dictionary<Job, LimitList<Pop>>();

            
        }

        public Dictionary<Job, LimitList<Pop>> JobToPopDictionary;
        public Settlement Settlement;

        public bool JobsUnWorked()
        {
            bool value = false;

            foreach (Job job in JobToPopDictionary.Keys)
            {
                if (JobToPopDictionary[job].Count == 0)
                {
                    return true;
                }
            }

            return value;
        }

        public void OnJobAdded(Job job)
        {
            if (JobToPopDictionary.ContainsKey(job))
            {
                return;
            }
            JobToPopDictionary[job] = new LimitList<Pop>(5);
        }

       

        public void SetJobListLength(Job job, int length)
        {
            if(JobToPopDictionary.ContainsKey(job) == false)
            {
                return;
            }

            JobToPopDictionary[job].SetLength(length);
        }

        public bool AddPopToVisibleJob(Job job, Pop pop)
        {
            bool result = false;
            if (VisibleJobs().Contains(job))
            {
                result = JobToPopDictionary[job].Add(pop);
            }
            foreach (Job key in JobToPopDictionary.Keys)
            {
                if (JobToPopDictionary[key].Contains(pop) && result == false)
                {
                    RemovePopFromJob(key, pop);
                }
            }
            return result;
        }

        public void RemovePop(Pop pop)
        {
            foreach (Job job in JobToPopDictionary.Keys)
            {
                if (JobToPopDictionary[job].Contains(pop))
                {
                    RemovePopFromJob(job, pop);
                }
            }
        }

        public bool RemovePopFromJob(Job job, Pop pop)
        {
            return JobToPopDictionary[job].Remove(pop);
        }

        public void ExecuteJobs()
        {
            foreach (Job job in JobToPopDictionary.Keys)
            {
                foreach (Pop pop in JobToPopDictionary[job])
                {
                    job.Execute(pop, Settlement);
                }
            }
        }

        public bool AssignPopToUnWorkedJob(Pop pop)
        {
            List<Job> visible = VisibleJobs();
            foreach (Job job in visible)
            {
                if (JobToPopDictionary[job].Count == 0)
                {
                    bool add = AddPopToVisibleJob(job, pop);

                    return add;
                }
            }

            return false;
        }
        public List<Job> VisibleJobs()
        {
            List<Job> visible = new List<Job>();

            foreach (Job job in JobToPopDictionary.Keys)
            {
                if (job.Requirements(Settlement))
                {

                    visible.Add(job);
                }
                else
                {

                }
            }

            return visible;
        }

        public void OnPopUnemployed(Pop pop)
        {
            
            for (int i = 0; i < 100; i++)
            {
                if (AssignPopToUnWorkedJob(pop))
                {
                    return;
                }
            }

            var jobDict = JobToPopDictionary;

            for (int i = 0; i < 100; i++)
            {
                int rand = World.RNG.Next(0, jobDict.Keys.Count);

                Job randJob = jobDict.ElementAt(rand).Key;


                if (AddPopToVisibleJob(randJob, pop))
                {
                    return;
                }

            }

        }
    }
}
