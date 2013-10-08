using System;
using System.IO;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;



namespace HairWorm
{
    public class EmulateCluster : GH_Component
    {
        public EmulateCluster() : base("EmulateCluster", "EMULC", "Emulate Cluster", "Extra", "Simple")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("String", "ClusterPath", "Path To Cluster", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Activate", "Activate", "Activate to emulate clsuter", GH_ParamAccess.item);
            pManager.AddGeometryParameter("Input Geometry", "InputGeo", "InputGeometry", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGeometryParameter("Output Geometry", "OutputGeo", "OutputGeometry", GH_ParamAccess.tree);
            pManager.AddGenericParameter("Generic Output", "GenericOutput", "GenericOutput", GH_ParamAccess.tree);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            // Declare a variable for the input String
            string filename = null;
            bool activate = false;
            Rhino.Geometry.Point3d point = Rhino.Geometry.Point3d.Unset;
            // 1. Declare placeholder variables and assign initial invalid data.
            //    This way, if the input parameters fail to supply valid data, we know when to abort.

            // 2. Retrieve input data.
            if (!DA.GetData(0, ref filename)) { return; }
            if (!DA.GetData(1, ref activate)) { return; }
            if (!DA.GetData(2, ref point)) { return; }

            // If the retrieved data is Nothing, we need to abort.
            if (filename == null) { return; }
            if (!File.Exists(filename)) { return; }

            if (activate)
            {
                GH_Cluster cluster = new GH_Cluster();
                cluster.CreateFromFilePath(filename);

                GH_Document doc = OnPingDocument();
                doc.AddObject(cluster, false);

                Grasshopper.Kernel.Parameters.Param_Point paramIn = new Grasshopper.Kernel.Parameters.Param_Point();
                Grasshopper.Kernel.Parameters.Param_Geometry paramOut = new Grasshopper.Kernel.Parameters.Param_Geometry();

                paramIn.SetPersistentData(point);

                cluster.Params.RegisterInputParam(paramIn);
                cluster.Params.RegisterOutputParam(paramOut);

                cluster.CollectData();

                cluster.ComputeData();

                                
                //Grasshopper.DataTree<object> test = new DataTree<object>();
                //test.Add(paramIn, 0);


                
                DA.SetData(1, paramOut);

                



                

            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("6ec94cbc-89ba-4be2-9e26-46ea747f662c"); }
        }
    }
}
