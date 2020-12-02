using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bruce;

public class UIJobsList : UI
{
    public Dictionary<Job, LimitList<Pop>> JobDictionary;
    public JobListItem JobListItemPrefab;
    public RectTransform JobHolder;
    public Text MainText;
    public UIJobPops UIJobPops;

    public override void Initialize(Action setterCallback)
    {
        base.Initialize(setterCallback);
        if(JobDictionary == null || JobDictionary.Keys.Count == 0)
        {
            JobDictionary = new Dictionary<Job, LimitList<Pop>>();
            Job placeHolderJob = new Job("No Jobs", null);
            JobDictionary.Add(placeHolderJob, new LimitList<Pop>(999));

            JobListItem listItem = Instantiate(JobListItemPrefab, JobHolder);

            listItem.JobNameText.text = placeHolderJob.name;
            listItem.JobNumberText.text = string.Empty;
            Prefabs.Add(listItem.gameObject);
        }
        else
        {
            foreach (Job job in JobDictionary.Keys)
            {
                JobListItem listItem = Instantiate(JobListItemPrefab, JobHolder);

                listItem.JobNameText.text = job.name;
                listItem.JobNumberText.text = string.Format("{0}/{1}", JobDictionary[job].Count, JobDictionary[job].Length);
                listItem.OpenPopMenuButton.onClick.AddListener(() => { PopsButtonOnClick(job); });

                Prefabs.Add(listItem.gameObject);
            }
        }
    }

    void PopsButtonOnClick( Job job )
    {
        if(JobDictionary.ContainsKey(job) == false)
        {
            Debug.Log("Job not in Dictionary");
            return;
        }
        LimitList<Pop> pops = JobDictionary[job];
        Action setterCallback = () => { UIJobPops.Pops = pops.GetList(); };
        UIController.Instance.DisplayUI(UIJobPops, setterCallback);
    }
}
