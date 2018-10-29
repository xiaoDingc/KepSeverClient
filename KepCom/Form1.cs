using OPCAutomation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KepCom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region 私有变量

        private OPCServer kepServer = new OPCServer();
        private OPCBrowser opcBrowser;
        private Dictionary<string, OPCGroup> groupMap = new Dictionary<string, OPCGroup>();
        private Dictionary<int, OPCItem> itemMap = new Dictionary<int, OPCItem>();
        #endregion
        //此方法连接OPCServer上的一个Program.
        public bool Connect(string host, string prog)
        {
            try
            {
                if (kepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    if (this.kepServer != null)
                    {
                        this.kepServer.Disconnect();
                        //this.connected = false;
                    }
                }
                this.tvwGroupList.Nodes.Clear();
                this.groupMap.Clear();
                this.itemMap.Clear();
                this.kepServer.Connect(prog, host);
                this.tsslblServerState.Text = "OPC服务[" + prog + "]已连接";
                TreeNode node = this.tvwGroupList.Nodes.Add(prog);
                node.Tag = this.kepServer;
                this.opcBrowser = this.kepServer.CreateBrowser();
                LoadDataToTree(node, "");
                this.SetItemsClientHandle();
                //this.LoadConfig(prog);
                this.tsslblItemCount.Text = "Item Count " + this.itemMap.Count.ToString();
                this.tsslblGroupCount.Text = "Group Count " + this.groupMap.Count.ToString();
                //this.connected = true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return true;
        }

        //用来标识每一个OPCItem数据的唯一标识
        private void SetItemsClientHandle()
        {
            foreach (OPCItem item in this.itemMap.Values)
            {
                item.ClientHandle = item.GetHashCode();
            }
        }

        //此方法通过OPCBrowser将数据结节加载到TreeView中.
        private bool LoadDataToTree(TreeNode node, string nodeName)
        {
            TreeNode childNode = null;
            OPCGroup group = null;
            try
            {
                this.opcBrowser.ShowBranches();
                int count = this.opcBrowser.Count;
                if (count == 0)
                {
                    this.opcBrowser.ShowLeafs(true);
                    group = this.kepServer.OPCGroups.Add(nodeName);
                    group.UpdateRate = 1000;
                    group.IsSubscribed = true;
                    group.IsActive = true;
                    node.Tag = group;
                    this.groupMap.Add(nodeName, group);
                }
                HashSet<string> itemList = new HashSet<string>();
                foreach (object turn in this.opcBrowser)
                {
                    string path = nodeName;
                    string name = turn.ToString();

                    if (count != 0)
                    {
                        childNode = node.Nodes.Add(name);
                        if (path != "")
                        {
                            path += ".";
                        }
                        path += name;
                        this.opcBrowser.MoveDown(name);
                        LoadDataToTree(childNode, path);
                        this.opcBrowser.MoveUp();
                    }
                    else
                    {
                        OPCItem opcItem = group.OPCItems.AddItem(name, 0);
                        itemList.Add(name);
                        if (!this.itemMap.ContainsKey(opcItem.GetHashCode()))
                        {
                            this.itemMap.Add(opcItem.GetHashCode(), opcItem);
                        }
                    }
                }

                this.opcBrowser.ShowLeafs(false);
                List<string> leafItems = new List<string>();
                foreach (object t in this.opcBrowser)
                {
                    string itemId = this.opcBrowser.GetItemID(t.ToString());
                    if (itemList.Contains(itemId))
                    {
                        continue;
                    }
                    leafItems.Add(itemId);
                }

                if (leafItems.Count != 0)
                {
                    group = this.kepServer.OPCGroups.Add(nodeName);
                    group.UpdateRate = 1000;
                    group.IsSubscribed = true;
                    group.IsActive = true;
                    this.groupMap.Add(nodeName, group);
                    node.Tag = group;
                    foreach (string t in leafItems)
                    {
                        OPCItem opcItem = group.OPCItems.AddItem(t, 0);
                        //childNode = node.Nodes.Add(t.ToString());
                        if (!this.itemMap.ContainsKey(opcItem.GetHashCode()))
                        {
                            this.itemMap.Add(opcItem.GetHashCode(), opcItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return true;
        }

        private ListViewItem listitem;
        //此方法将选中的叶子节点(OPCGroup)下的所有OPCItem显示在ListView中.
        private void tvwGroupList_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                TreeNode crtNode = this.tvwGroupList.GetNodeAt(e.X, e.Y);
                TreeNode oldNode = this.tvwGroupList.SelectedNode;
                if (oldNode != null)
                {
                    this.lstviewItems.Items.Clear();
                    if (oldNode.Tag is OPCGroup)
                    {
                        OPCGroup old_group = oldNode.Tag as OPCGroup;
                    }
                }
                this.tvwGroupList.SelectedNode = crtNode;

                if (e.Button == MouseButtons.Right)
                {
                    if (crtNode.Tag is OPCServer)
                    {
                        this.opcServercontextMenuStrip.Show(this.tvwGroupList, new Point(e.X, e.Y));
                    }
                    else if (crtNode.Tag is OPCGroup)
                    {
                        this.opcGroupcontextMenuStrip.Show(this.tvwGroupList, new Point(e.X, e.Y));
                    }
                }
                if (crtNode.Tag is OPCGroup)
                {
                    OPCGroup crt_group = crtNode.Tag as OPCGroup;
                    OPCItems items = crt_group.OPCItems;
                    if (items.Count != 0)
                    {
                        foreach (OPCItem item in items)
                        {
                            string itemId = item.ItemID;
                            string itemValue = Convert.ToString(item.Value);
                            
                            if (item.CanonicalDataType == 11)
                            {
                                itemValue = Convert.ToInt32(Convert.ToBoolean(itemValue)).ToString();
                            }
                            listitem = new ListViewItem(new string[] {
                    itemId,
                    //item.CanonicalDataType.ToString(),
                    itemValue,
                    item.TimeStamp.ToLocalTime().ToString(),
                    //OPCEnumHelper.GetQuality((OPCQuality)item.Quality),
                    //"0"
                            });
                            
                            listitem.Tag = item.ClientHandle;
                            this.lstviewItems.Items.Add(listitem);
                        }
                      
                    }
                    else
                    {
                        Array branches = crt_group.Name.Split('.');
                        this.opcBrowser.MoveTo(ref branches);
                        this.opcBrowser.ShowLeafs(true);
                        foreach (object turn in this.opcBrowser)
                        {
                            string name = turn.ToString();
                            //crt_group.OPCItems.AddItem(name, this.itemHandleClient);

                            ListViewItem listitem = new ListViewItem(new string[] {
                            name,
                            "",
                            "",
                            "",
                            "",
                            ""});
                            this.lstviewItems.Items.Add(listitem);
                        }

                        this.opcBrowser.ShowLeafs(true);
                        this.opcBrowser.MoveToRoot();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Connect("127.0.0.1", "KEPware.KEPServerEx.V4");
          
        }

        internal class LogHelper
        {
            public static void Error(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void lstviewItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbShow.Text = "";
            if (lstviewItems.SelectedIndices!=null && lstviewItems.SelectedIndices.Count>0)
            {
                var x = lstviewItems.SelectedIndices;
                foreach (var item in lstviewItems.Items[x[0]].SubItems)
                {                  
                    lbShow.Text += item.ToString() + "\r\n";
                }
               
            } 
        }
    }
}
