﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenZWaveDotNet;

namespace OZWForm
{
    public partial class MainForm : Form
    {
        static private ZWOptions m_options = null;
        static public ZWOptions Options
        {
            get { return m_options; }
        }

        static private ZWManager m_manager = null;
        static public ZWManager Manager
        {
            get { return m_manager; }
        }

        private UInt32 m_homeId = 0;
        private ZWNotification m_notification = null;
        private BindingList<Node> m_nodeList = new BindingList<Node>();
        private Byte m_rightClickNode = 0xff;
        public MainForm()
        {
            // Initialize the form
            InitializeComponent();

            // Add the columns to the grid view
            // Data Grid
            NodeGridView.AutoGenerateColumns = false;
            NodeGridView.AllowUserToResizeColumns = true;
            NodeGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            DataGridViewTextBoxColumn column;
            //DataGridViewCheckBoxColumn check;

            // Id
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ID";
            column.Name = "Node";
            column.ReadOnly = true;
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ToolTipText = "The Z-Wave node ID of the device.\nThis value is not editable.";
            NodeGridView.Columns.Add(column);

            // Location
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Location";
            column.Name = "Location";
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ToolTipText = "The user-defined location of the Z-Wave device.";
            NodeGridView.Columns.Add(column);

            // Name
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ToolTipText = "The user-defined name for the Z-Wave device.";
            NodeGridView.Columns.Add(column);

            // Device Type
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Label";
            column.Name = "Type";
            column.ReadOnly = true;
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ToolTipText = "The Z-Wave device type.\nThis value is not editable.";
            NodeGridView.Columns.Add(column);

            // Manufacturer
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Manufacturer";
            column.Name = "Manufacturer";
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ToolTipText = "The manufacturer of the Z-Wave device.";
            NodeGridView.Columns.Add(column);

            // Product
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Product";
            column.Name = "Product";
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.ToolTipText = "The product name of the Z-Wave device.";
            NodeGridView.Columns.Add(column);

/*            // Poll Interval
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "PollInterval";
            column.Name = "Poll Interval";
            column.ReadOnly = false;
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ToolTipText = "Polling interval in seconds, or zero for no polling.\nNewer devices should not need to be polled for\nyour PC to know their current state.\nFor those that do requre polling, the interval should\nbe as long as possible to reduce network traffic.";
            NodeGridView.Columns.Add(column);
*/
/*            // Schema
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Schema";
            column.Name = "Schema";
            column.ReadOnly = true;
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ToolTipText = "The xPL message schema family that will be used\nif the 'Use zwave.basic' option is not checked.\nThe schema is chosen automatically according to\nthe Z-Wave device type, and cannot be changed.";
            NodeGridView.Columns.Add(column);

            // ZWaveBasic
            //check = new DataGridViewCheckBoxColumn();
            //check.DataPropertyName = "ZWaveBasic";
            //check.Name = "Use zwave.basic";
            //check.Frozen = false;
            //check.Resizable = DataGridViewTriState.True;
            //check.SortMode = DataGridViewColumnSortMode.NotSortable;
            //check.ToolTipText = "If the box is checked, the device will send and respond to\nnative zwave.basic messages rather than those of the\ngeneric schema family listed under the Schema column.";
            //NodeGridView.Columns.Add(check);
*/
            // Level
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Level";
            column.Name = "Level";
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ToolTipText = "Current level of the device";
            NodeGridView.Columns.Add(column);

            // On-Off button
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.DataPropertyName = "ButtonText";
            buttonColumn.Name = "Power";
            buttonColumn.Frozen = false;
            buttonColumn.Resizable = DataGridViewTriState.True;
            buttonColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            buttonColumn.ToolTipText = "Click a button to turn a light on or off";
            NodeGridView.Columns.Add(buttonColumn);

            BindingSource bs = new BindingSource();
            bs.DataSource = m_nodeList;
            NodeGridView.DataSource = bs;

            // Create the Options
            m_options = new ZWOptions();
            m_options.Create(@"..\..\..\..\..\..\config\", @"", @"");

            // Add any app specific options here...

            // Lock the options
            m_options.Lock();

            // Create the OpenZWave Manager
            m_manager = new ZWManager();
            m_manager.Create();
            m_manager.OnNotification += new ManagedNotificationsHandler(NotificationHandler);

            // Add a driver
            m_manager.AddDriver(@"\\.\COM4");
        }

        public void NotificationHandler(ZWNotification notification)
        {
            // Handle the notification on a thread that can safely
            // modify the form controls without throwing an exception.
            m_notification = notification;
            Invoke(new MethodInvoker(NotificationHandler));
            m_notification = null;
        }

        private void NotificationHandler()
        {
            switch (m_notification.GetType())
            {
                case ZWNotification.Type.ValueAdded:
                    {
                        Node node = GetNode(m_notification.GetHomeId(), m_notification.GetNodeId());
                        if (node != null)
                        {
                            node.AddValue(m_notification.GetValueID());
                        }
                        break;
                    }

                case ZWNotification.Type.ValueRemoved:
                    {
                        Node node = GetNode(m_notification.GetHomeId(), m_notification.GetNodeId());
                        if (node != null)
                        {
                            node.RemoveValue(m_notification.GetValueID());
                        }
                        break;
                    }

                case ZWNotification.Type.ValueChanged:
                    {
                        break;
                    }

                case ZWNotification.Type.Group:
                    {
                        break;
                    }

                case ZWNotification.Type.NodeAdded:
                    {
                        // Add the new node to our list
                        Node node = new Node();
                        node.ID = m_notification.GetNodeId();
                        node.HomeID = m_notification.GetHomeId();
                        m_nodeList.Add(node);

                        // Request refreshed config param values
                        m_manager.RequestAllConfigParams(node.HomeID, node.ID);
                        break;
                    }

                case ZWNotification.Type.NodeRemoved:
                    {
                        foreach (Node node in m_nodeList)
                        {
                            if (node.ID == m_notification.GetNodeId())
                            {
                                m_nodeList.Remove(node);
                                break;
                            }
                        }
                        break;
                    }

                case ZWNotification.Type.NodeProtocolInfo:
                    {
                        Node node = GetNode(m_notification.GetHomeId(), m_notification.GetNodeId());
                        if (node != null)
                        {
                            node.Label = m_manager.GetNodeType(m_homeId, node.ID);
                        }
                        break;
                    }

                case ZWNotification.Type.NodeNaming:
                    {
                        Node node = GetNode(m_notification.GetHomeId(), m_notification.GetNodeId());
                        if (node != null)
                        {
                            node.Manufacturer = m_manager.GetNodeManufacturerName(m_homeId, node.ID);
                            node.Product = m_manager.GetNodeProductName(m_homeId, node.ID);
                            node.Location = m_manager.GetNodeLocation(m_homeId, node.ID);
                            node.Name = m_manager.GetNodeName(m_homeId, node.ID);
                        }
                        break;
                    }

                case ZWNotification.Type.NodeEvent:
                    {
                        break;
                    }

                case ZWNotification.Type.PollingDisabled:
                    {
                        Console.WriteLine("Polling disabled notification");
                        break;
                    }

                case ZWNotification.Type.PollingEnabled:
                    {
                        Console.WriteLine("Polling disabled notification");
                        break;
                    }

                case ZWNotification.Type.DriverReady:
                    {
                        m_homeId = m_notification.GetHomeId();
                        break;
                    }
                case ZWNotification.Type.AllNodesQueried:
                    {
                        MessageBox.Show("All nodes queried");
 //                       m_manager.EnablePoll(m_homeId, 7);
                        break;
                    }
                case ZWNotification.Type.AwakeNodesQueried:
                    {
                        MessageBox.Show("Awake nodes queried (but some sleeping nodes have not been queried)");
                        break;
                    }
            }

            //NodeGridView.Refresh();
            NodeGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            NodeGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private Node GetNode(UInt32 homeId, Byte nodeId)
        {
            foreach (Node node in m_nodeList)
            {
                if ((node.ID == nodeId) && (node.HomeID == homeId))
                {
                    return node;
                }
            }

            return null;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_manager.WriteConfig(m_homeId);
        }

        private void NodeGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.Button == System.Windows.Forms.MouseButtons.Right))
            {
                // Highlight the clicked row
                NodeGridView.Rows[e.RowIndex].Selected = true;

                // Store the index of the selected node
                m_rightClickNode = Convert.ToByte(NodeGridView.Rows[e.RowIndex].Cells["Node"].Value);
            }
        }

        private void PowerOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_manager.SetNodeOn(m_homeId, m_rightClickNode);
        }

        private void PowerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_manager.SetNodeOff(m_homeId, m_rightClickNode);
        }

        private void hasNodeFailedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.HasNodeFailed);
        }

        private void markNodeAsFailedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.RemoveFailedNode);
        }

        private void replaceFailedNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.ReplaceFailedNode);
        }

        private void createNewPrmaryControllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand( ZWControllerCommand.CreateNewPrimary);
        }

        private void addControllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.AddController);
        }

        private void addDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.AddDevice);
        }

        private void removeControllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.RemoveController);
        }

        private void removeDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.RemoveDevice);
        }

        private void transferPrimaryRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.TransferPrimaryRole);
        }

        private void receiveConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.ReceiveConfiguration);
        }

        private void requestNetworkUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.RequestNetworkUpdate);
        }

        private void requestNodeNeighborUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.RequestNodeNeighborUpdate);
        }

        private void assignReturnRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.AssignReturnRoute);
        }

        private void deleteReturnRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.DeleteAllReturnRoutes);
        }

        private void DoCommand(ZWControllerCommand command)
        {
            ControllerCommandDlg dlg = new ControllerCommandDlg(this, m_manager, m_homeId, command, m_rightClickNode);
            dlg.ShowDialog(this);
            dlg.Dispose();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Node node = GetNode(m_homeId, m_rightClickNode);
            if (node != null)
            {
                // Show the form
                NodeForm dlg = new NodeForm( node );
                dlg.ShowDialog(this);
                dlg.Dispose();
            }
        }

        private void NodeGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0))
            {
                // Invalid cell
                return;
            }

            if (e.ColumnIndex == 1)
            {
                // Location
                Byte nodeId = Convert.ToByte(NodeGridView.Rows[e.RowIndex].Cells["Node"].Value);
                Node node = GetNode(m_homeId, nodeId);
                if (node != null)
                {
                    String newLocation = e.Value.ToString();
                    if (newLocation != node.Location)
                    {
                        m_manager.SetNodeLocation(m_homeId, node.ID, newLocation);
                    }
                }
            }

            if (e.ColumnIndex == 2)
            {
                // Name
                Byte nodeId = Convert.ToByte(NodeGridView.Rows[e.RowIndex].Cells["Node"].Value);
                Node node = GetNode(m_homeId, nodeId);
                if (node != null)
                {
                    String newName = e.Value.ToString();
                    if (newName != node.Name)
                    {
                        m_manager.SetNodeName(m_homeId, node.ID, newName);
                    }
                }
            }
        }
    }
}
