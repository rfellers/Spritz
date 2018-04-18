﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WorkflowLayer
{
    public enum MyWorkflow
    {
        Fastaq2Proteins,
        LncRnaDiscovery
    }

    public abstract class SpritzFlow
    {
        protected SpritzFlow(MyWorkflow workflowType)
        {
            WorkflowType = workflowType;
        }

        public MyWorkflow WorkflowType { get; set; }

        public void RunTask(string output_folder, List<string> genomeFastaList, List<string> geneSetList, List<string> rnaSeqFastqList, string displayName)
        {
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                RunSpecific(output_folder, genomeFastaList, geneSetList, rnaSeqFastqList);
                stopWatch.Stop();
                var resultsFileName = Path.Combine(output_folder, "results.txt");
                using (StreamWriter file = new StreamWriter(resultsFileName))
                {
                    file.WriteLine("Spritz: version ");
                    file.Write(stopWatch.Elapsed.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected abstract void RunSpecific(string OutputFolder, List<string> genomeFastaList, List<string> geneSetList, List<string> rnaSeqFastqList);
    }
}