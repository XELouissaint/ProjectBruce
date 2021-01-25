using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bruce;

public class UIJobsList : UI
{
    public JobListItem JobListItemPrefab;
    public RectTransform JobHolder;
    public RectTransform JobModeSelecterHolder;
    public Text MainText;
    public UIJobPops UIJobPops;

    public override void Initialize(Action setterCallback)
    {
        base.Initialize(setterCallback);

        var jobDict = SelectedSettlement.JobManager.JobDictionary;
        foreach (Job job in jobDict.Keys)
        {
            List<JobMode> uniqueModes = new List<JobMode>();
            List<JobOpening> uniqueOpenings = new List<JobOpening>();
            foreach (JobOpening opening in jobDict[job])
            {
                if (opening.jobMode != null)
                {
                    if (uniqueModes.Contains(opening.jobMode))
                    {
                        continue;
                    }
                    uniqueModes.Add(opening.jobMode);
                    uniqueOpenings.Add(opening);
                }
            }

            foreach(JobOpening opening in uniqueOpenings)
            {
                JobListItem listItem = Instantiate(JobListItemPrefab, JobHolder.transform);

                listItem.JobNameText.text = string.Format("{0}: {1}", job.name, opening.jobMode.name);
                listItem.JobNumberText.text = jobDict[job].Where(o => o.jobMode == opening.jobMode && o.pop != null).Count().ToString();

                listItem.OpenModeMenuButton.onClick.AddListener(() => { });

                Prefabs.Add(listItem.gameObject);
            }
        }
    }

    void PopsButtonOnClick( JobMode mode )
    {
    }

    void ModeMenuButton(Job job)
    {
        foreach(JobMode mode in job.Modes)
        {
            JobListItem listItem = Instantiate(JobListItemPrefab, JobHolder.transform);

            listItem.JobNameText.text = mode.name;

            listItem.OpenModeMenuButton.GetComponentInChildren<Text>().text = "select";
            listItem.OpenModeMenuButton.onClick.AddListener(() => { SelectModeButton(mode); });
        }
    }

    void SelectModeButton(JobMode mode)
    {

    }
}
