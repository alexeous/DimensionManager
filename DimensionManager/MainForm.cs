﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace DimensionManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            axisSelectCombo.SelectedIndex = 0;

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "XML File | *.xml";
                if (DialogResult.OK != dialog.ShowDialog(this))
                {
                    return;
                }
                Save(dialog.FileName);
            }
        }

        private void Save(string fileName)
        {
            CoordianteSystem cs = CalcCoordinateSystem();
            Vector3 master = cs.GetLocalPoint(masterAntBox.Vector);
            Vector3 remote = cs.GetLocalPoint(remoteAntBox.Vector);
            Vector3 drillingRig = cs.GetLocalPoint(drillingRigBox.Vector);
            Vector3 mast = cs.GetLocalPoint(mastBox.Vector);
            Vector3 frontLeftJack = cs.GetLocalPoint(frontLeftJackBox.Vector);
            Vector3 frontRightJack = cs.GetLocalPoint(frontRightJackBox.Vector);
            Vector3 rearJack = cs.GetLocalPoint(rearJackBox.Vector);

            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\"?>");
            
        }

        private CoordianteSystem CalcCoordinateSystem()
        {
            Vector3 axisX, axisY;
            var specifiedAxis = Vector3.Normalize(axisBBox.Vector - axisABox.Vector);
            if(axisSelectCombo.SelectedIndex == 0)
            {
                axisX = specifiedAxis;
                axisY = Vector3.Normalize(new Vector3(-axisX.Y, axisX.X, 0));
            }
            else
            {
                axisY = specifiedAxis;
                axisX = Vector3.Normalize(new Vector3(axisY.Y, -axisY.X, 0));
            }
            var axisZ = Vector3.Cross(axisX, axisY);
            return new CoordianteSystem(masterAntBox.Vector, axisX, axisY, axisZ);
        }
        /*
        private string VectorToXML(Vector3 vector)
        {

        }*/
    }
}
