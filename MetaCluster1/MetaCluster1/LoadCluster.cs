using System;
using System.IO;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;



namespace MetaCluster1
{
    public class LoadCluster : GH_Component
    {
        public LoadCluster() : base("Load Cluster", "LOADC", "Load Cluster", "Extra", "Simple")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("String", "ClusterPath", "Path To Cluster", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Activate", "Activate", "Activate to load clsuter", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
           
            // Declare a variable for the input String
            string filename = null;
            bool activate = false;
            // 1. Declare placeholder variables and assign initial invalid data.
            //    This way, if the input parameters fail to supply valid data, we know when to abort.
                        
            // 2. Retrieve input data.
            if (!DA.GetData(0, ref filename)) { return; }
            if (!DA.GetData(1, ref activate)) { return; }

            // If the retrieved data is Nothing, we need to abort.
            if (filename == null) { return; }
            if (!File.Exists(filename)) { return; }

            if (activate)
            {
                GH_Cluster cluster = new GH_Cluster();
                cluster.CreateFromFilePath(filename);

                GH_Document doc = OnPingDocument();
                doc.AddObject(cluster, false);
             }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("9859098a-9361-4d26-81d0-7143087cac55"); }
        }
    }
}
