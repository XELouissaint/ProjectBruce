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
            JobDictionary = new Dictionary<Job, LimitList<JobOpening>>();
        }

        public Dictionary<Job, LimitList<JobOpening>> JobDictionary;

        public Settlement Settlement;

       
        public void OnJobAdded(Job job)
        {
            if (JobDictionary.ContainsKey(job))
            {
                return;
            }
            JobDictionary[job] = new LimitList<JobOpening>(1);

            PopulateJobOpenings(job);
            
        }


        public void RemovePop(Pop pop)
        {
           foreach(Job job in JobDictionary.Keys)
            {
                foreach(JobOpening opening in JobDictionary[job])
                {
                    if(opening.pop == pop)
                    {
                        RemovePopFromJob(opening);
                    }
                }
            }
        }

        public void RemovePopFromJob(JobOpening opening)
        {
            opening.pop = null;
            opening.jobMode = JobFactory.NoMode;
        }

        public void ExecuteJobs()
        {
            foreach (Job job in JobDictionary.Keys)
            {
                foreach (JobOpening opening in JobDictionary[job])
                {
                    if (opening.pop != null && opening.jobMode != null && opening.jobMode != JobFactory.NoMode)
                    {
                        opening.jobMode.Execute(opening.pop, Settlement);
                    }
                }
            }
        }

        internal void SetJobListLength(Job job, int v)
        {
            if(JobDictionary.ContainsKey(job) == false)
            {
                return;
            }

            JobDictionary[job].SetLength(v);
            PopulateJobOpenings(job);
        }

        void PopulateJobOpenings(Job job)
        {
            for (int i = 0; i < JobDictionary[job].Length; i++)
            {

                JobDictionary[job].Add(new JobOpening());

            }
        }

        internal bool AssignPopToUnWorkedJob(Pop pop)
        {
            foreach (Job job in JobDictionary.Keys)
            {
                Dictionary<JobMode, int> scores = new Dictionary<JobMode, int>();

                foreach (JobMode mode in job.Modes.Skip(1))
                {
                    scores[mode] = 10;
                    if (mode.Requirements(Settlement))
                    {
                        scores[mode] += 0;
                    }
                    else
                    {
                        scores[mode] -= 1000;
                    }

                    
                }

                foreach (JobOpening opening in JobDictionary[job])
                {
                    if (opening.jobMode != JobFactory.NoMode && opening.pop != null)
                    {
                        scores[opening.jobMode] -= 2;
                    }
                }

                JobOpening selectedOpening = null;
                int count = JobDictionary[job].Where(o => o.pop == null).Count();
                if (count > 0)
                {
                    selectedOpening = JobDictionary[job].Where(o => o.pop == null).First();
                }
                if (selectedOpening == null)
                {
                    return false;
                }

                JobMode winningMode = null;
                int maxScore = 0;
                foreach (JobMode mode in scores.Keys)
                {
                    if(scores[mode] < 0)
                    {
                        continue;
                    }

                    if(scores[mode] > maxScore)
                    {
                        maxScore = scores[mode];
                        winningMode = mode;
                    }
                }
                selectedOpening.jobMode = winningMode;
                selectedOpening.pop = pop;

                return true;
            }

            return false;
        }
    }

    public class JobOpening
    {
        public JobOpening()
        {
            jobMode = JobFactory.NoMode;
        }

        public Pop pop;
        public JobMode jobMode;
    }
}
